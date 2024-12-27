# 从 OpenAI 1.11.0 迁移到 OpenAI 2.0.0-beta.1 或更高版本的指南

本指南旨在帮助将官方 OpenAI 库（2.0.0-beta.1 或更高版本）迁移自 [OpenAI 1.11.0][openai_1110] ，重点是库之间相似操作的并排比较。将使用版本 2.0.0-beta.1 与 1.11.0 进行比较，但在迁移到更高版本时，本指南仍然可以安全使用。

在 2.0.0-beta.1 之前，OpenAI 包是一个社区库，不受 OpenAI 官方支持。有关更多详细信息，请参见 [CHANGELOG][changelog]。

假定您熟悉 OpenAI 1.11.0 包。对于首次使用任何 OpenAI .NET 库的用户，请参阅 [README][readme] 而不是本指南。

## 目录
- [客户端使用](#client-usage)
- [身份验证](#authentication)
- [突出显示的场景](#highlighted-scenarios)
    - [聊天完成：文本生成](#chat-completions-text-generation)
    - [聊天完成：流式传输](#chat-completions-streaming)
    - [聊天完成：JSON 模式](#chat-completions-json-mode)
    - [聊天完成：视觉](#chat-completions-vision)
    - [音频：语音转文本](#audio-speech-to-text)
    - [音频：文本转语音](#audio-text-to-speech)
    - [图像：图像生成](#image-image-generation)
- [其他示例](#additional-examples)

## 客户端使用

不同库之间的客户端使用发生了显著变化。虽然 OpenAI 1.11.0 有一个单一的客户端 `OpenAIAPI`，可以访问多个 API，但 OpenAI 2.0.0-beta.1 为每个 API 保持了单独的客户端。以下代码片段说明了从图像 API 调用图像生成能力时的区别：

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
ImageResult result = await api.ImageGenerations.CreateImageAsync("Draw a quick brown fox jumping over a lazy dog.", Model.DALLE3);
```

OpenAI 2.0.0-beta.1:
```cs
ImageClient client = new ImageClient("dall-e-3", "<api-key>");
ClientResult<GeneratedImage> result = await client.GenerateImageAsync("Draw a quick brown fox jumping over a lazy dog.");
```

另一个在上述代码片段中突出的重大区别是，OpenAI 2.0.0-beta.1 需要在客户端实例化时显式设置模型，而 `OpenAIAPI` 客户端允许在每次调用时指定模型。

下表展示了 `OpenAIAPI` 的每个端点被移植到哪个客户端。请注意，已弃用的 Completions API 在 2.0.0-beta.1 中不再支持：

旧库的端点 | 新库的客户端
|-|-
|聊天 | ChatClient
|图像生成 | ImageClient
|文本转语音 | AudioClient
|转录 | AudioClient
|翻译 | AudioClient
|审核 | ModerationClient
|嵌入 | EmbeddingClient
|文件 | OpenAIFileClient
|模型 | OpenAIModelClient
|完成功能 | 不支持

## 身份验证

要进行 OpenAI 身份验证，您必须在创建客户端时设置 API 密钥。

OpenAI 1.11.0 支持通过三种不同方式设置 API 密钥：
- 直接从字符串
- 从环境变量
- 从配置文件

```cs
OpenAIAPI api;

// 直接从字符串设置 API 密钥。
api = new OpenAIAPI("<api-key>");

// 尝试从环境变量 OPENAI_KEY 和 OPENAI_API_KEY 加载 API 密钥。
api = new OpenAIAPI(APIAuthentication.LoadFromEnv());

// 尝试从配置文件加载 API 密钥。
api = new OpenAIAPI(APIAuthentication.LoadFromPath("<directory>", "<filename>"));
```

OpenAI 2.0.0-beta.1 仅支持从字符串或环境变量设置。以下代码片段说明了 `ChatClient` 的行为，但其他客户端的行为相同：

```cs
ChatClient client;

// 直接从字符串设置 API 密钥。
client = new ChatClient("gpt-3.5-turbo", "<api-key>");

// 如果未指定 API 密钥字符串，则尝试从环境变量 OPENAI_API_KEY 加载 API 密钥。
client = new ChatClient("gpt-3.5-turbo");
```

请注意，与 OpenAI 1.11.0 不同的是，OpenAI 2.0.0-beta.1 将永远不会尝试从 `OPENAI_KEY` 环境变量加载 API 密钥。仅支持 `OPENAI_API_KEY`。

## 突出显示的场景

以下部分展示了两个库之间相似操作的并排比较，突出常见场景。

### 聊天完成：文本生成

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
Conversation conversation = api.Chat.CreateConversation();

conversation.Model = Model.ChatGPTTurbo;
conversation.AppendSystemMessage("You are a helpful assistant.");
conversation.AppendUserInput("When was the Nobel Prize founded?");

await conversation.GetResponseFromChatbotAsync();

conversation.AppendUserInput("Who was the first person to be awarded one?");

await conversation.GetResponseFromChatbotAsync();

foreach (ChatMessage message in conversation.Messages)
{
    Console.WriteLine($"{message.Role}: {message.TextContent}");
}
```

OpenAI 2.0.0-beta.1:
```cs
ChatClient client = new ChatClient("gpt-3.5-turbo", "<api-key>");
List<ChatMessage> messages = new List<ChatMessage>()
{
    new SystemChatMessage("You are a helpful assistant."),
    new UserChatMessage("When was the Nobel Prize founded?")
};

ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages);

messages.Add(new AssistantChatMessage(result));
messages.Add(new UserChatMessage("Who was the first person to be awarded one?"));

result = await client.CompleteChatAsync(messages);

messages.Add(new AssistantChatMessage(result));

foreach (ChatMessage message in messages)
{
    string role = message.GetType().Name;
    string text = message.Content[0].Text;

    Console.WriteLine($"{role}: {text}");
}
```

### 聊天完成：流式传输

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
Conversation conversation = api.Chat.CreateConversation();

conversation.Model = Model.ChatGPTTurbo;
conversation.AppendUserInput("Give me a list of Nobel Prize winners of the last 5 years.");

await foreach (string response in conversation.StreamResponseEnumerableFromChatbotAsync())
{
    Console.Write(response);
}
```

OpenAI 2.0.0-beta.1:
```cs
ChatClient client = new ChatClient("gpt-3.5-turbo", "<api-key>");
List<ChatMessage> messages = new List<ChatMessage>()
{
    new UserChatMessage("Give me a list of Nobel Prize winners of the last 5 years.")
};

await foreach (StreamingChatCompletionUpdate chatUpdate in client.CompleteChatStreamingAsync(messages))
{
    if (chatUpdate.ContentUpdate.Count > 0)
    {
        Console.Write(chatUpdate.ContentUpdate[0].Text);
    }
}
```

### 聊天完成：JSON 模式

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
ChatRequest request = new ChatRequest()
{
    Model = Model.ChatGPTTurbo,
    ResponseFormat = request.ResponseFormats.JsonObject,
    Messages = new List<ChatMessage>()
    {
        new ChatMessage(ChatMessageRole.System, "You are a helpful assistant designed to output JSON."),
        new ChatMessage(ChatMessageRole.User, "Give me a JSON object listing Nobel Prize winners of the last 5 years.")
    }
};

ChatResult result = await api.Chat.CreateChatCompletionAsync(request);

Console.WriteLine(result);
```

OpenAI 2.0.0-beta.1:
```cs
ChatClient client = new ChatClient("gpt-3.5-turbo", "<api-key>");
List<ChatMessage> messages = new List<ChatMessage>()
{
    new SystemChatMessage("You are a helpful assistant designed to output JSON."),
    new UserChatMessage("Give me a JSON object listing Nobel Prize winners of the last 5 years.")
};
ChatCompletionOptions options = new ChatCompletionOptions()
{
    ResponseFormat = ChatResponseFormat.JsonObject
};

ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages, options);
string text = result.Value.Content[0].Text;

Console.WriteLine(text);
```

### 聊天完成：视觉

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
Conversation conversation = api.Chat.CreateConversation();
byte[] imageData = await File.ReadAllBytesAsync("<file-path>");

conversation.Model = Model.GPT4_Vision;
conversation.AppendUserInput("Describe this image.", ImageInput.FromImageBytes(imageData));

string response = await conversation.GetResponseFromChatbotAsync();

Console.WriteLine(response);
```

OpenAI 2.0.0-beta.1:
```cs
ChatClient client = new ChatClient("gpt-4-vision-preview", "<api-key>");
using FileStream file = File.OpenRead("<file-path>");
BinaryData imageData = await BinaryData.FromStreamAsync(file);
List<ChatMessage> messages = new List<ChatMessage>()
{
    new UserChatMessage(
        ChatMessageContentPart.CreateTextMessageContentPart("Describe this image."),
        ChatMessageContentPart.CreateImageMessageContentPart(imageData, "image/png"))
};

ClientResult<ChatCompletion> result = await client.CompleteChatAsync(messages);
string text = result.Value.Content[0].Text;

Console.WriteLine(text);
```

### 音频：语音转文本

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
string result = await api.Transcriptions.GetTextAsync("<file-path>", "fr");

Console.WriteLine(result);
```

OpenAI 2.0.0-beta.1:
```cs
AudioClient client = new AudioClient("whisper-1", "<api-key>");
AudioTranscriptionOptions options = new AudioTranscriptionOptions()
{
    Language = "fr"
};

ClientResult<AudioTranscription> result = await client.TranscribeAudioAsync("<file-path>", options);
string text = result.Value.Text;

Console.WriteLine(text);
```

### 音频：文本转语音

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
TextToSpeechRequest request = new TextToSpeechRequest()
{
    Input = "Hasta la vista, baby.",
    Model = Model.TTS_Speed,
    Voice = "alloy"
};

await api.TextToSpeech.SaveSpeechToFileAsync(request, "<file-path>");
```

OpenAI 2.0.0-beta.1:
```cs
AudioClient client = new AudioClient("tts-1", "<api-key>");

ClientResult<BinaryData> result = await client.GenerateSpeechFromTextAsync("Hasta la vista, baby.", GeneratedSpeechVoice.Alloy);
BinaryData data = result.Value;

await File.WriteAllBytesAsync("<file-path>", data.ToArray());
```

### 图像：图像生成

OpenAI 1.11.0:
```cs
OpenAIAPI api = new OpenAIAPI("<api-key>");
ImageGenerationRequest request = new ImageGenerationRequest()
{
    Prompt = "Draw a quick brown fox jumping over a lazy dog.",
    Model = Model.DALLE3,
    Quality = "standard",
    Size = ImageSize._1024
};

ImageResult result = await api.ImageGenerations.CreateImageAsync(request);

Console.WriteLine(result.Data[0].Url);
```

OpenAI 2.0.0-beta.1:
```cs
ImageClient client = new ImageClient("dall-e-3", "<api-key>");
ImageGenerationOptions options = new ImageGenerationOptions()
{
    Quality = GeneratedImageQuality.Standard,
    Size = GeneratedImageSize.W1024xH1024
};

ClientResult<GeneratedImage> result = await client.GenerateImageAsync("Draw a quick brown fox jumping over a lazy dog.", options);
Uri imageUri = result.Value.ImageUri;

Console.WriteLine(imageUri.AbsoluteUri);
```

## 其他示例

有关其他示例，请参见 [OpenAI 示例][examples]。

[readme]: https://github.com/openai/openai-dotnet/blob/main/README.md
[changelog]: https://github.com/openai/openai-dotnet/blob/main/CHANGELOG.md
[examples]: https://github.com/openai/openai-dotnet/tree/main/examples
[openai_1110]: https://aka.ms/openai1110
