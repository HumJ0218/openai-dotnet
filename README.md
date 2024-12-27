# OpenAI .NET API 库

[![NuGet 稳定版本](https://img.shields.io/nuget/v/openai.svg)](https://www.nuget.org/packages/OpenAI) [![NuGet 预览版本](https://img.shields.io/nuget/vpre/openai.svg)](https://www.nuget.org/packages/OpenAI/absoluteLatest)

OpenAI .NET 库方便地从 .NET 应用程序访问 OpenAI REST API。

该库是与微软合作生成的，基于我们的 [OpenAPI 规范](https://github.com/openai/openai-openapi)。

## 目录

- [开始使用](#getting-started)
  - [前提条件](#prerequisites)
  - [安装 NuGet 包](#install-the-nuget-package)
- [使用客户端库](#using-the-client-library)
  - [命名空间组织](#namespace-organization)
  - [使用异步 API](#using-the-async-api)
  - [使用 `OpenAIClient` 类](#using-the-openaiclient-class)
- [如何使用流式聊天补全](#how-to-use-chat-completions-with-streaming)
- [如何使用工具和函数调用的聊天补全](#how-to-use-chat-completions-with-tools-and-function-calling)
- [如何使用结构化输出的聊天补全](#how-to-use-chat-completions-with-structured-outputs)
- [如何生成文本嵌入](#how-to-generate-text-embeddings)
- [如何生成图像](#how-to-generate-images)
- [如何转录音频](#how-to-transcribe-audio)
- [如何使用带有检索增强生成（RAG）的助手](#how-to-use-assistants-with-retrieval-augmented-generation-rag)
- [如何使用带有流和视觉的助手](#how-to-use-assistants-with-streaming-and-vision)
- [如何使用 Azure OpenAI](#how-to-work-with-azure-openai)
- [高级场景](#advanced-scenarios)
  - [使用协议方法](#using-protocol-methods)
  - [为测试模拟客户端](#mock-a-client-for-testing)
  - [自动重试错误](#automatically-retrying-errors)
  - [可观察性](#observability)

## 开始使用

### 前提条件

要调用 OpenAI REST API，您需要一个 API 密钥。要获取一个，请首先 [创建一个新的 OpenAI 账户](https://platform.openai.com/signup) 或 [登录](https://platform.openai.com/login)。接下来，导航到 [API 密钥页面](https://platform.openai.com/account/api-keys)，选择“创建新密钥”，可以为密钥命名。确保将 API 密钥保存在安全的地方，并且不要与任何人共享。

### 安装 NuGet 包

通过在 IDE 中安装 [NuGet](https://www.nuget.org/) 包，或在 .NET CLI 中运行以下命令，将客户端库添加到您的 .NET 项目中：

```cli
dotnet add package OpenAI
```

如果您想尝试最新的预览版本，请记得追加 `--prerelease` 命令选项。

请注意，下面包含的代码示例是使用 [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) 编写的。OpenAI .NET 库与所有 .NET Standard 2.0 应用程序兼容，但此文档中某些代码示例中使用的语法可能依赖于更新的语言特性。

## 使用客户端库

该库的完整 API 可以在 [OpenAI.netstandard2.0.cs](https://github.com/openai/openai-dotnet/blob/main/api/OpenAI.netstandard2.0.cs) 文件中找到，并且有许多 [代码示例](https://github.com/openai/openai-dotnet/tree/main/examples) 可供参考。例如，以下代码片段演示了聊天补全 API 的基本用法：

```csharp
using OpenAI.Chat;

ChatClient client = new(model: "gpt-4o", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");

Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
```

虽然您可以将 API 密钥直接传递为字符串，但强烈建议您将其保存在安全位置，并通过环境变量或配置文件如上所示进行访问，以避免将其存储在源代码控制中。

### 命名空间组织

库按 OpenAI REST API 的功能区域组织成命名空间。每个命名空间包含一个相应的客户端类。

| 命名空间                     | 客户端类                 | 备注                                                             |
| ------------------------------|--------------------------|-------------------------------------------------------------------|
| `OpenAI.Assistants`           | `AssistantClient`        | ![实验性](https://img.shields.io/badge/experimental-purple)      |
| `OpenAI.Audio`                | `AudioClient`            |                                                                   |
| `OpenAI.Batch`                | `BatchClient`            | ![实验性](https://img.shields.io/badge/experimental-purple)      |
| `OpenAI.Chat`                 | `ChatClient`             |                                                                   |
| `OpenAI.Embeddings`           | `EmbeddingClient`        |                                                                   |
| `OpenAI.FineTuning`           | `FineTuningClient`       | ![实验性](https://img.shields.io/badge/experimental-purple)      |
| `OpenAI.Files`                | `OpenAIFileClient`       |                                                                   |
| `OpenAI.Images`               | `ImageClient`            |                                                                   |
| `OpenAI.Models`               | `OpenAIModelClient`      |                                                                   |
| `OpenAI.Moderations`          | `ModerationClient`       |                                                                   |
| `OpenAI.VectorStores`         | `VectorStoreClient`      | ![实验性](https://img.shields.io/badge/experimental-purple)      |

### 使用异步 API

每个执行同步 API 调用的客户端方法都有一个异步变体。例如，`ChatClient` 的 `CompleteChat` 方法的异步变体是 `CompleteChatAsync`。要使用异步的对应部分重写上述调用，只需 `await` 对应异步变体的调用：

```csharp
ChatCompletion completion = await client.CompleteChatAsync("Say 'this is a test.'");
```

### 使用 `OpenAIClient` 类

除了上述命名空间外，还有父命名空间 `OpenAI`：

```csharp
using OpenAI;
```

该命名空间包含 `OpenAIClient` 类，当您需要与多个功能区域客户端合作时，它提供了一些便利。具体来说，您可以使用该类的实例创建其他客户端的实例，并使它们共享相同的实现细节，这可能更高效。

您可以通过指定所有客户端将用于身份验证的 API 密钥来创建一个 `OpenAIClient`：

```csharp
OpenAIClient client = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
```

接下来，例如，要创建 `AudioClient` 的实例，可以调用 `OpenAIClient` 的 `GetAudioClient` 方法，传递 `AudioClient` 将使用的 OpenAI 模型，就像您直接使用 `AudioClient` 构造函数一样。如果需要，您可以创建多个相同类型的客户端来针对不同的模型。

```csharp
AudioClient ttsClient = client.GetAudioClient("tts-1");
AudioClient whisperClient = client.GetAudioClient("whisper-1");
```

## 如何使用流式聊天补全

当您请求聊天补全时，默认行为是服务器在发送响应前生成它的完整内容。因此，较长的聊天补全可能需要等待几秒钟才能收到服务器的回复。为了缓解这个问题，OpenAI REST API 支持在生成的过程中流式返回部分结果，使您能够在补全完成之前开始处理补全的开头。

客户端库提供了一种便捷的方式来处理流式聊天补全。如果您想使用流式技术重写上一节中的示例，而不是调用 `ChatClient` 的 `CompleteChat` 方法，您将调用它的 `CompleteChatStreaming` 方法：

```csharp
CollectionResult<StreamingChatCompletionUpdate> completionUpdates = client.CompleteChatStreaming("Say 'this is a test.'");
```

注意，返回的值是 `CollectionResult<StreamingChatCompletionUpdate>` 实例，可以通过它进行枚举，以便在响应块到达时处理流式响应：

```csharp
Console.Write($"[ASSISTANT]: ");
foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
{
    if (completionUpdate.ContentUpdate.Count > 0)
    {
        Console.Write(completionUpdate.ContentUpdate[0].Text);
    }
}
```

或者，您可以通过调用 `CompleteChatStreamingAsync` 方法异步执行此操作，从而获得 `AsyncCollectionResult<StreamingChatCompletionUpdate>`，并使用 `await foreach` 进行枚举：

```csharp
AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = client.CompleteChatStreamingAsync("Say 'this is a test.'");

Console.Write($"[ASSISTANT]: ");
await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
{
    if (completionUpdate.ContentUpdate.Count > 0)
    {
        Console.Write(completionUpdate.ContentUpdate[0].Text);
    }
}
```

## 如何使用工具和函数调用的聊天补全

在这个例子中，您有两个函数。第一个函数可以检索用户的当前位置（例如，通过轮询用户设备的定位服务 API），而第二个函数可以查询给定位置的天气（例如，通过调用某个第三方天气服务的 API）。您希望模型能够在生成聊天补全以响应用户请求时调用这些函数，如果它认为这是必要的信息。为了说明这一点，考虑以下代码：

```csharp
private static string GetCurrentLocation()
{
    // 在这里调用定位 API。
    return "San Francisco";
}

private static string GetCurrentWeather(string location, string unit = "celsius")
{
    // 在这里调用天气 API。
    return $"31 {unit}";
}
```

首先，使用静态 `CreateFunctionTool` 方法创建两个 `ChatTool` 实例，以描述每个函数：

```csharp
private static readonly ChatTool getCurrentLocationTool = ChatTool.CreateFunctionTool(
    functionName: nameof(GetCurrentLocation),
    functionDescription: "获取用户当前位置"
);

private static readonly ChatTool getCurrentWeatherTool = ChatTool.CreateFunctionTool(
    functionName: nameof(GetCurrentWeather),
    functionDescription: "获取给定位置的当前天气",
    functionParameters: BinaryData.FromBytes("""
        {
            "type": "object",
            "properties": {
                "location": {
                    "type": "string",
                    "description": "城市和州，例如 Boston, MA"
                },
                "unit": {
                    "type": "string",
                    "enum": [ "celsius", "fahrenheit" ],
                    "description": "使用的温度单位。从指定位置推断此单位。"
                }
            },
            "required": [ "location" ]
        }
        """u8.ToArray())
);
```

接下来，创建一个 `ChatCompletionOptions` 实例，并将两个工具添加到其 `Tools` 属性。您将在调用 `ChatClient` 的 `CompleteChat` 方法时将 `ChatCompletionOptions` 作为参数传递。

```csharp
List<ChatMessage> messages = 
[
    new UserChatMessage("今天的天气怎么样？"),
];

ChatCompletionOptions options = new()
{
    Tools = { getCurrentLocationTool, getCurrentWeatherTool },
};
```

当生成的 `ChatCompletion` 的 `FinishReason` 属性等于 `ChatFinishReason.ToolCalls` 时，这意味着模型已确定必须调用一个或多个工具，然后助手才能做出适当反应。在这种情况下，您必须首先调用在 `ChatCompletion` 的 `ToolCalls` 中指定的函数，然后再次调用 `ChatClient` 的 `CompleteChat` 方法，同时传递函数的结果作为额外的 `ChatRequestToolMessage`。根据需要重复此过程。

```csharp
bool requiresAction;

do
{
    requiresAction = false;
    ChatCompletion completion = client.CompleteChat(messages, options);

    switch (completion.FinishReason)
    {
        case ChatFinishReason.Stop:
            {
                // 将助手消息添加到会话历史记录中。
                messages.Add(new AssistantChatMessage(completion));
                break;
            }

        case ChatFinishReason.ToolCalls:
            {
                // 首先，将带有工具调用的助手消息添加到会话历史记录中。
                messages.Add(new AssistantChatMessage(completion));

                // 然后，为每个已解决的工具调用添加新的工具消息。
                foreach (ChatToolCall toolCall in completion.ToolCalls)
                {
                    switch (toolCall.FunctionName)
                    {
                        case nameof(GetCurrentLocation):
                            {
                                string toolResult = GetCurrentLocation();
                                messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
                                break;
                            }

                        case nameof(GetCurrentWeather):
                            {
                                // 模型希望用于调用函数的参数以字符串化 JSON 对象的形式指定，基于工具定义中定义的模式。注意，
                                // 模型可能会产生虚构的参数。因此，进行适当的解析和验证至关重要。
                                using JsonDocument argumentsJson = JsonDocument.Parse(toolCall.FunctionArguments);
                                bool hasLocation = argumentsJson.RootElement.TryGetProperty("location", out JsonElement location);
                                bool hasUnit = argumentsJson.RootElement.TryGetProperty("unit", out JsonElement unit);

                                if (!hasLocation)
                                {
                                    throw new ArgumentNullException(nameof(location), "location 参数是必需的。");
                                }

                                string toolResult = hasUnit
                                    ? GetCurrentWeather(location.GetString(), unit.GetString())
                                    : GetCurrentWeather(location.GetString());
                                messages.Add(new ToolChatMessage(toolCall.Id, toolResult));
                                break;
                            }

                        default:
                            {
                                // 处理其他意外调用。
                                throw new NotImplementedException();
                            }
                    }
                }

                requiresAction = true;
                break;
            }

        case ChatFinishReason.Length:
            throw new NotImplementedException("由于 MaxTokens 参数或超出令牌限制，模型输出不完整。");

        case ChatFinishReason.ContentFilter:
            throw new NotImplementedException("由于内容过滤标志而省略的内容。");

        case ChatFinishReason.FunctionCall:
            throw new NotImplementedException("已弃用，建议使用工具调用。");

        default:
            throw new NotImplementedException(completion.FinishReason.ToString());
    }
} while (requiresAction);
```

## 如何使用结构化输出的聊天补全

从 `gpt-4o-mini`、`gpt-4o-mini-2024-07-18` 和 `gpt-4o-2024-08-06` 模型快照开始，结构化输出可用于聊天补全和助手 API 中的顶级响应内容和工具调用。有关该功能的信息，请参见 [结构化输出指南](https://platform.openai.com/docs/guides/structured-outputs/introduction)。

要使用结构化输出来约束聊天补全内容，可以设置适当的 `ChatResponseFormat`，如下例所示：

```csharp
List<ChatMessage> messages =
[
    new UserChatMessage("如何解决 8x + 7 = -23？"),
];

ChatCompletionOptions options = new()
{
    ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
        jsonSchemaFormatName: "math_reasoning",
        jsonSchema: BinaryData.FromBytes("""
            {
                "type": "object",
                "properties": {
                "steps": {
                    "type": "array",
                    "items": {
                    "type": "object",
                    "properties": {
                        "explanation": { "type": "string" },
                        "output": { "type": "string" }
                    },
                    "required": ["explanation", "output"],
                    "additionalProperties": false
                    }
                },
                "final_answer": { "type": "string" }
                },
                "required": ["steps", "final_answer"],
                "additionalProperties": false
            }
            """u8.ToArray()),
        jsonSchemaIsStrict: true)
};

ChatCompletion completion = client.CompleteChat(messages, options);

using JsonDocument structuredJson = JsonDocument.Parse(completion.Content[0].Text);

Console.WriteLine($"最终答案: {structuredJson.RootElement.GetProperty("final_answer")}");
Console.WriteLine("推理步骤：");

foreach (JsonElement stepElement in structuredJson.RootElement.GetProperty("steps").EnumerateArray())
{
    Console.WriteLine($"  - 解释: {stepElement.GetProperty("explanation")}");
    Console.WriteLine($"    输出: {stepElement.GetProperty("output")}");
}
```

## 如何生成文本嵌入

在这个例子中，您希望创建一个旅行计划网站，允许客户编写描述所需酒店类型的提示，然后提供与该描述紧密匹配的酒店推荐。为此，可以使用文本嵌入来测量文本字符串之间的相关性。总之，您可以获取酒店描述的嵌入，将它们存储在向量数据库中，并据此建立您可以使用给定客户提示的嵌入查询的搜索索引。

要生成文本嵌入，使用来自 `OpenAI.Embeddings` 命名空间的 `EmbeddingClient`：

```csharp
using OpenAI.Embeddings;

EmbeddingClient client = new("text-embedding-3-small", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

string description = "如果您喜欢豪华酒店，镇上最好的酒店。他们有一个令人惊叹的无边际游泳池，一个水疗中心，"
    + " 以及一个非常乐于助人的礼宾。位置完美——就在市中心，靠近所有的旅游景点。我们强烈推荐这家酒店。";

OpenAIEmbedding embedding = client.GenerateEmbedding(description);
ReadOnlyMemory<float> vector = embedding.ToFloats();
```

注意，结果嵌入是一个浮点数列表（也称为向量），表示为 `ReadOnlyMemory<float>` 实例。默认情况下，使用 `text-embedding-3-small` 模型时，嵌入向量的长度为 1536；使用 `text-embedding-3-large` 模型时，长度为 3072。通常，较大的嵌入性能更好，但使用它们通常在计算、内存和存储上消耗更大。您可以通过创建 `EmbeddingGenerationOptions` 类的实例，设置 `Dimensions` 属性，并将其作为参数传递给 `GenerateEmbedding` 方法来减少嵌入的维数：

```csharp
EmbeddingGenerationOptions options = new() { Dimensions = 512 };

OpenAIEmbedding embedding = client.GenerateEmbedding(description, options);
```

## 如何生成图像

在这个例子中，您希望构建一个应用程序，帮助室内设计师根据最新的设计趋势原型新想法。作为创意过程的一部分，室内设计师可以使用此应用程序，仅通过描述他们脑海中的场景作为提示来生成灵感图像。如预期，具有较高质量、引人注目的、生动的图像获取更细致的结果。

要生成图像，使用来自 `OpenAI.Images` 命名空间的 `ImageClient`：

```csharp
using OpenAI.Images;

ImageClient client = new("dall-e-3", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
```

生成图像始终需要一个描述所生成内容的 `prompt`。要进一步调整图像生成以满足您的特定需求，您可以创建 `ImageGenerationOptions` 类的实例并相应地设置 `Quality`、`Size` 和 `Style` 属性。注意，您还可以将 `ImageGenerationOptions` 的 `ResponseFormat` 属性设置为 `GeneratedImageFormat.Bytes`，以便在此情况是方便的情况下，将生成的 PNG 作为 `BinaryData` 接收（而不是默认的远程 `Uri`）。

```csharp
string prompt = "融合斯堪的纳维亚简约与日本极简主义的客厅概念，营造宁静和舒适的氛围。这个空间邀请放松和正念，拥有自然光和新鲜空气。使用中性调色，包括白色、米色、灰色和黑色，创造一种和谐感。特色精美木家具，具有干净的线条和细微的曲线，以增添温暖和优雅。陶瓷花盆中的植物和花卉为空间增添色彩和生命。它们可以作为焦点点缀，创造与自然的连接。软面料和有机材质的靠垫为空间增添舒适和柔和感。它们可以作为点缀，增加对比和质感。";

ImageGenerationOptions options = new()
{
    Quality = GeneratedImageQuality.High,
    Size = GeneratedImageSize.W1792xH1024,
    Style = GeneratedImageStyle.Vivid,
    ResponseFormat = GeneratedImageFormat.Bytes
};
```

最后，通过传递提示和 `ImageGenerationOptions` 实例作为参数调用 `ImageClient` 的 `GenerateImage` 方法：

```csharp
GeneratedImage image = client.GenerateImage(prompt, options);
BinaryData bytes = image.ImageBytes;
```

为了说明，您可以将生成的图像保存到本地存储：

```csharp
using FileStream stream = File.OpenWrite($"{Guid.NewGuid()}.png");
bytes.ToStream().CopyTo(stream);
```

## 如何转录音频

在这个例子中，使用 Whisper 语音转文本模型转录音频文件，包括单词级和音频段级的时间戳信息。

```csharp
using OpenAI.Audio;

AudioClient client = new("whisper-1", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

string audioFilePath = Path.Combine("Assets", "audio_houseplant_care.mp3");

AudioTranscriptionOptions options = new()
{
    ResponseFormat = AudioTranscriptionFormat.Verbose,
    TimestampGranularities = AudioTimestampGranularities.Word | AudioTimestampGranularities.Segment,
};

AudioTranscription transcription = client.TranscribeAudio(audioFilePath, options);

Console.WriteLine("转录文本：");
Console.WriteLine($"{transcription.Text}");

Console.WriteLine();
Console.WriteLine($"单词：");
foreach (TranscribedWord word in transcription.Words)
{
    Console.WriteLine($"  {word.Word,15} : {word.StartTime.TotalMilliseconds,5:0} - {word.EndTime.TotalMilliseconds,5:0}");
}

Console.WriteLine();
Console.WriteLine($"段落：");
foreach (TranscribedSegment segment in transcription.Segments)
{
    Console.WriteLine($"  {segment.Text,90} : {segment.StartTime.TotalMilliseconds,5:0} - {segment.EndTime.TotalMilliseconds,5:0}");
}
```

## 如何使用带有检索增强生成（RAG）的助手

在这个例子中，您有一个包含不同产品的每月销售信息的 JSON 文档，您希望构建一个能够分析这些信息并回答相关问题的助手。

为此，使用 `OpenAIFileClient` 来自 `OpenAI.Files` 命名空间和 `AssistantClient` 来自 `OpenAI.Assistants` 命名空间。

重要提示：助手 REST API 目前处于 beta 状态。因此，具体细节可能会发生变化，相应地 `AssistantClient` 被标记为 `[Experimental]`。要使用它，您必须首先抑制 `OPENAI001` 警告。

```csharp
using OpenAI.Assistants;
using OpenAI.Files;

OpenAIClient openAIClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
OpenAIFileClient fileClient = openAIClient.GetOpenAIFileClient();
AssistantClient assistantClient = openAIClient.GetAssistantClient();
```

以下是 JSON 文档可能的格式示例：

```csharp
using Stream document = BinaryData.FromBytes("""
    {
        "description": "该文档包含 Contoso 产品的销售历史数据。",
        "sales": [
            {
                "month": "January",
                "by_product": {
                    "113043": 15,
                    "113045": 12,
                    "113049": 2
                }
            },
            {
                "month": "February",
                "by_product": {
                    "113045": 22
                }
            },
            {
                "month": "March",
                "by_product": {
                    "113045": 16,
                    "113055": 5
                }
            }
        ]
    }
    """u8.ToArray()).ToStream();
```

使用 `OpenAIFileClient` 的 `UploadFile` 方法将该文档上传至 OpenAI，确保使用 `FileUploadPurpose.Assistants` 允许您的助手稍后访问它：

```csharp
OpenAIFile salesFile = fileClient.UploadFile(
    document,
    "monthly_sales.json",
    FileUploadPurpose.Assistants);
```

使用 `AssistantCreationOptions` 类的实例，自定义新助手的选项。这里我们使用：

- 在 Playground 中显示的助手友好 `Name`
- 工具定义实例，助手应能够访问的工具；此处，我们使用 `FileSearchToolDefinition` 处理刚上传的销售文档以及 `CodeInterpreterToolDefinition`，以便分析和可视化数值数据
- 助手使用其工具的资源，这里使用 `VectorStoreCreationHelper` 类型自动创建一个新向量库以索引销售文件；另外，您也可以使用 `VectorStoreClient` 单独管理向量库

```csharp
AssistantCreationOptions assistantOptions = new()
{
    Name = "示例：Contoso 销售 RAG",
    Instructions =
        "您是一个查询销售数据并帮助可视化信息的助手，基于用户的查询。当要求生成图形、图表或其他可视化内容时，请使用"
        + " 代码解释器工具来完成。",
    Tools =
    {
        new FileSearchToolDefinition(),
        new CodeInterpreterToolDefinition(),
    },
    ToolResources = new()
    {
        FileSearch = new()
        {
            NewVectorStores =
            {
                new VectorStoreCreationHelper([salesFile.Id]),
            }
        }
    },
};

Assistant assistant = assistantClient.CreateAssistant("gpt-4o", assistantOptions);
```

接下来，创建一个新线程。为了说明，您可以包括一条初始用户消息，询问关于特定产品的销售信息，然后使用 `AssistantClient` 的 `CreateThreadAndRun` 方法让它开始：

```csharp
ThreadCreationOptions threadOptions = new()
{
    InitialMessages = { "产品 113045 在二月份的销售情况如何？画出它的趋势。" }
};

ThreadRun threadRun = assistantClient.CreateThreadAndRun(assistant.Id, threadOptions);
```

轮询运行的状态，直到其不再排队或进行中：

```csharp
do
{
    Thread.Sleep(TimeSpan.FromSeconds(1));
    threadRun = assistantClient.GetRun(threadRun.ThreadId, threadRun.Id);
} while (!threadRun.Status.IsTerminal);
```

如果一切顺利，运行的终端状态将为 `RunStatus.Completed`。

最后，您可以使用 `AssistantClient` 的 `GetMessages` 方法检索与此线程相关的消息，其中现在包括助手对初始用户消息的响应。

为了说明，您可以将消息打印到控制台，并将助手生成的任何图像保存到本地存储：

```csharp
CollectionResult<ThreadMessage> messages
    = assistantClient.GetMessages(threadRun.ThreadId, new MessageCollectionOptions() { Order = MessageCollectionOrder.Ascending });

foreach (ThreadMessage message in messages)
{
    Console.Write($"[{message.Role.ToString().ToUpper()}]: ");
    foreach (MessageContent contentItem in message.Content)
    {
        if (!string.IsNullOrEmpty(contentItem.Text))
        {
            Console.WriteLine($"{contentItem.Text}");

            if (contentItem.TextAnnotations.Count > 0)
            {
                Console.WriteLine();
            }

            // 包括注释（如果有）。
            foreach (TextAnnotation annotation in contentItem.TextAnnotations)
            {
                if (!string.IsNullOrEmpty(annotation.InputFileId))
                {
                    Console.WriteLine($"* 文件引用，文件 ID: {annotation.InputFileId}");
                }
                if (!string.IsNullOrEmpty(annotation.OutputFileId))
                {
                    Console.WriteLine($"* 文件输出，新文件 ID: {annotation.OutputFileId}");
                }
            }
        }
        if (!string.IsNullOrEmpty(contentItem.ImageFileId))
        {
            OpenAIFile imageInfo = fileClient.GetFile(contentItem.ImageFileId);
            BinaryData imageBytes = fileClient.DownloadFile(contentItem.ImageFileId);
            using FileStream stream = File.OpenWrite($"{imageInfo.Filename}.png");
            imageBytes.ToStream().CopyTo(stream);

            Console.WriteLine($"<image: {imageInfo.Filename}.png>");
        }
    }
    Console.WriteLine();
}
```

这将产生如下输出：

```text
[USER]: 产品 113045 在二月份的销售情况如何？画出它的趋势。

[ASSISTANT]: 产品 113045 在二月份销售了 22 个单位【4:0†monthly_sales.json】。

现在，我将生成一个图表来展示其销售趋势。

* 文件引用，文件 ID: file-hGOiwGNftMgOsjbynBpMCPFn

[ASSISTANT]: <image: 015d8e43-17fe-47de-af40-280f25452280.png>
产品 113045 在过去三个月的销售趋势表明：

- 一月份售出 12 个单位。
- 二月份售出 22 个单位，表明显著增长。
- 三月份的销售略微下降至 16 个单位。

上述图表可视化了这一趋势，显示出二月份的销售高峰。
```

## 如何使用带有流和视觉的助手

此示例显示了如何使用 v2 助手 API 向助手提供图像数据，然后流式传输运行的响应。

如前所述，您将使用 `OpenAIFileClient` 和 `AssistantClient`：

```csharp
OpenAIClient openAIClient = new(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
OpenAIFileClient fileClient = openAIClient.GetOpenAIFileClient();
AssistantClient assistantClient = openAIClient.GetAssistantClient();
```

在本示例中，我们将使用来自本地文件的图像数据以及位于 URL 的图像。对于本地数据，我们使用 `Vision` 上传目的将文件上传，这也允许它稍后被下载和检索。

```csharp
OpenAIFile pictureOfAppleFile = fileClient.UploadFile(
    Path.Combine("Assets", "images_apple.png"),
    FileUploadPurpose.Vision);

Uri linkToPictureOfOrange = new("https://raw.githubusercontent.com/openai/openai-dotnet/refs/heads/main/examples/Assets/images_orange.png");
```

接下来，使用具有视觉能力的模型（如 `gpt-4o`）和引用图像信息的线程创建新助手：

```csharp
Assistant助手 = assistantClient.CreateAssistant(
    "gpt-4o",
    new AssistantCreationOptions()
    {
        Instructions = "当被问及问题时，尽量用简洁的回答。"
            + " 在可行时优先使用一句话回答。"
    });

AssistantThread thread = assistantClient.CreateThread(new ThreadCreationOptions()
{
    InitialMessages =
        {
            new ThreadInitializationMessage(
                MessageRole.User,
                [
                    "你好，助手！请比较这两张图像：",
                    MessageContent.FromImageFileId(pictureOfAppleFile.Id),
                    MessageContent.FromImageUri(linkToPictureOfOrange),
                ]),
        }
});
```

准备好助手和线程后，使用 `CreateRunStreaming` 方法获取可枚举的 `CollectionResult<StreamingUpdate>`。然后，您可以通过 `foreach` 迭代此集合。对于异步调用模式，使用 `CreateRunStreamingAsync` 并通过 `await foreach` 迭代 `AsyncCollectionResult<StreamingUpdate>`。请注意，流式变体也存在于 `CreateThreadAndRunStreaming` 和 `SubmitToolOutputsToRunStreaming`。

```csharp
CollectionResult<StreamingUpdate> streamingUpdates = assistantClient.CreateRunStreaming(
    thread.Id,
    assistant.Id,
    new RunCreationOptions()
    {
        AdditionalInstructions = "在可能的情况下，如果您被要求进行比较，请尝试加入双关语。",
    });
```

最后，为了处理到达的 `StreamingUpdates`，您可以使用 `UpdateKind` 属性以及向下转换为特定期望的更新类型，例如 `MessageContentUpdate` 用于 `thread.message.delta` 事件或 `RequiredActionUpdate` 用于流式工具调用。

```csharp
foreach (StreamingUpdate streamingUpdate in streamingUpdates)
{
    if (streamingUpdate.UpdateKind == StreamingUpdateReason.RunCreated)
    {
        Console.WriteLine($"--- 运行已开始！ ---");
    }
    if (streamingUpdate is MessageContentUpdate contentUpdate)
    {
        Console.Write(contentUpdate.Text);
    }
}
```

这将产生来自运行的流式输出，如下所示：

```text
--- 运行已开始！ ---
第一张图像描绘了一个多色苹果，混合着红色和绿色的色调，而第二张图像显示了一种橙子，具有明亮、多纹理的橙色外皮；人们可能会说这是比较苹果和橙子！
```

## 如何使用 Azure OpenAI

对于 Azure OpenAI 场景，请使用 [Azure SDK](https://github.com/Azure/azure-sdk-for-net)，更具体地说是 [用于 .NET 的 Azure OpenAI 客户端库](https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/openai/Azure.AI.OpenAI/README.md)。

用于 .NET 的 Azure OpenAI 客户端库是该库的补充，所有 OpenAI 和 Azure OpenAI 之间的通用能力共享相同的场景客户端、方法和请求/响应类型。它旨在使特定于 Azure 的场景变得简单，并附加 Azure 特有的概念，例如负责任的 AI 内容过滤结果和您的数据集成。

```c#
AzureOpenAIClient azureClient = new(
    new Uri("https://your-azure-openai-resource.com"),
    new DefaultAzureCredential());
ChatClient chatClient = azureClient.GetChatClient("my-gpt-35-turbo-deployment");

ChatCompletion completion = chatClient.CompleteChat(
    [
        // 系统消息表示助手应该如何表现的指令或其他指导
        new SystemChatMessage("您是一个乐于助人的助手，以海盗的口吻交谈。"),
        // 用户消息表示用户输入，无论是历史输入还是最近输入
        new UserChatMessage("嗨，您能帮我吗？"),
        // 请求中的助手消息代表响应的对话历史
        new AssistantChatMessage("啊哈！当然可以，我的朋友！我能为您做些什么？"),
        new UserChatMessage("训练鹦鹉的最佳方法是什么？"),
    ]);

Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");
```

## 高级场景

### 使用协议方法

除了使用强类型请求和响应对象的客户端方法外，.NET 库还提供了 _协议方法_，用于更直接地访问 REST API。协议方法是“二进制输入，二进制输出”，接受 `BinaryContent` 作为请求体，并提供 `BinaryData` 作为响应体。

例如，要使用 `ChatClient` 的 `CompleteChat` 方法的协议方法变体，传递请求体作为 `BinaryContent`：

```csharp
ChatClient client = new("gpt-4o", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

BinaryData input = BinaryData.FromBytes("""
    {
       "model": "gpt-4o",
       "messages": [
           {
               "role": "user",
               "content": "Say 'this is a test.'"
           }
       ]
    }
    """u8.ToArray());

using BinaryContent content = BinaryContent.Create(input);
ClientResult result = client.CompleteChat(content);
BinaryData output = result.GetRawResponse().Content;

using JsonDocument outputAsJson = JsonDocument.Parse(output.ToString());
string message = outputAsJson.RootElement
    .GetProperty("choices"u8)[0]
    .GetProperty("message"u8)
    .GetProperty("content"u8)
    .GetString();

Console.WriteLine($"[ASSISTANT]: {message}");
```

注意，您可以在这里调用结果的 `ClientResult` 的 `GetRawResponse` 方法，并通过 `PipelineResponse` 的 `Content` 属性检索响应体作为 `BinaryData`。

### 为测试模拟客户端

OpenAI .NET 库旨在支持模拟，提供以下关键功能：

- 客户端方法被声明为虚拟以允许重写。
- 模型工厂协助实例化缺少公共构造函数的 API 输出模型。

为了说明模拟如何工作，假设您想使用 [Moq](https://github.com/devlooped/moq) 库验证以下方法的行为。给定音频文件的路径，它确定是否包含指定的秘密词：

```csharp
public bool ContainsSecretWord(AudioClient client, string audioFilePath, string secretWord)
{
    AudioTranscription transcription = client.TranscribeAudio(audioFilePath);
    return transcription.Text.Contains(secretWord);
}
```

创建 `AudioClient` 和 `ClientResult<AudioTranscription>` 的模拟，设置将被调用的方法和属性，然后测试 `ContainsSecretWord` 方法的行为。因为 `AudioTranscription` 类没有提供公共构造函数，所以必须通过 `OpenAIAudioModelFactory` 静态类实例化：

```csharp
// 实例化 mocks 和 AudioTranscription 对象。

Mock<AudioClient> mockClient = new();
Mock<ClientResult<AudioTranscription>> mockResult = new(null, Mock.Of<PipelineResponse>());
AudioTranscription transcription = OpenAIAudioModelFactory.AudioTranscription(text: "我发誓我昨天看到一个苹果在飞！");

// 设置 mocks 的属性和方法。

mockResult
    .SetupGet(result => result.Value)
    .Returns(transcription);

mockClient.Setup(client => client.TranscribeAudio(
        It.IsAny<string>(),
        It.IsAny<AudioTranscriptionOptions>()))
    .Returns(mockResult.Object);

// 执行验证。

AudioClient client = mockClient.Object;
bool containsSecretWord = ContainsSecretWord(client, "<audioFilePath>", "apple");

Assert.That(containsSecretWord, Is.True);
```

所有命名空间都有各自的模型工厂来支持模拟，除了 `OpenAI.Assistants` 和 `OpenAI.VectorStores` 命名空间，它们的模型工厂即将推出。

### 自动重试错误

默认情况下，客户端类将自动重试以下错误最多三次，使用指数回退策略：

- 408 请求超时
- 429 请求过多
- 500 内部服务器错误
- 502 错误网关
- 503 服务不可用
- 504 网关超时

### 可观察性

OpenAI .NET 库支持与 OpenTelemetry 的实验性分布式跟踪和度量。有关更多详细信息，请查看 [使用 OpenTelemetry 的可观察性](./docs/observability.md)。
