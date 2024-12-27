# 发布历史

## 2.1.0 (2024-12-04)

### 新增功能

- OpenAI.Assistants:
  - 在 `RunStepFileSearchResult` 中添加了 `Content` 属性 ([`step_details.tool_calls.file_search.results.content` 在 REST API 中](https://platform.openai.com/docs/api-reference/run-steps/step-object))。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
    - 使用带文件搜索工具的助手时，可以使用此属性检索模型使用的文件搜索结果的内容。
  - 在 `RunStepDetailsUpdate` 中添加了 `FileSearchRankingOptions` 和 `FileSearchResults` 属性。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))

### 预览 APIs 的重大变更

- OpenAI.RealtimeConversation:
  - 将 `ConversationContentPart` 上的 `From*()` 工厂方法重命名为 `Create*Part()` 以保持一致性。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
  - 从 `ConversationItem.CreateSystemMessage()` 中移除了多余的 `toolCallId` 参数。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
- OpenAI.Assistants:
  - 将 `RunStepType` 重命名为 `RunStepKind`。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
  - 将 `RunStepKind` 从“可扩展枚举”更改为常规枚举。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
  - 将 `RunStepToolCall` 的 `ToolCallId` 属性重命名为 `Id`。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
  - 将 `RunStepToolCall` 的 `ToolKind` 属性重命名为 `Kind`。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
  - 用一个新的 `FileSearchRankingOptions` 属性替换了 `RunStepToolCall` 的 `FileSearchRanker` 和 `FileSearchScoreThreshold` 属性，该属性包含这两个值，以更清晰地显示它们之间的关系。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))

### 修复的错误

- OpenAI.RealtimeConversation:
  - 修复了 `ConversationItem` 创建系统和助手消息时的序列化问题。 ([bf3f0ed](https://github.com/openai/openai-dotnet/commit/bf3f0eddeda1957a998491e36d7fb551e99be916))
  - 修复了调用 `RealtimeConversationSession` 的 `SendInputAudio` 方法重载时发生死锁的问题，该方法接收 `BinaryData` 参数。 ([f491c2d](https://github.com/openai/openai-dotnet/commit/f491c2d5a3894953e0bc112431ea3844a64496da))

## 2.1.0-beta.2 (2024-11-04)

### 新增功能

- OpenAI.Chat:
  - 在 `ChatCompletionOptions` 中添加了一个 `StoredOutputEnabled` 属性 ([`store` 在 REST API 中](https://platform.openai.com/docs/api-reference/chat/create#chat-create-store))。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))
    - 使用此属性指示是否存储聊天完成的输出以供模型蒸馏或评估使用。
  - 在 `ChatCompletionOptions` 中添加了一个 `Metadata` 属性 ([`metadata` 在 REST API 中](https://platform.openai.com/docs/api-reference/chat/create#chat-create-metadata))。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))
    - 使用此属性为聊天完成添加自定义标签和值，以便在 OpenAI 控制面板中过滤。
  - 在 `ChatTokenUsage` 中添加了一个 `InputTokenDetails` 属性 ([`usage.prompt_token_details` 在 REST API 中](https://platform.openai.com/docs/api-reference/chat/object#chat/object-usage))。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))
    - 该属性是一个名为 `ChatInputTokenUsageDetails` 的新类型，包含 `AudioTokenCount` 和 `CachedTokenCount` 的属性，用于支持的模型的使用。
  - 在 `ChatOutputTokenUsageDetails` 中添加了一个 `AudioTokenCount` 属性 ([`usage.completion_token_details` 在 REST API 中](https://platform.openai.com/docs/api-reference/chat/object#chat/object-usage))。 聊天完成的音频支持很快就会推出。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))
- OpenAI.Moderations:
  - 在 `ModerationResult` 中添加了 `Illicit` 和 `IllicitViolent` 属性以表示这两个新的审查类别。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))

### 预览 APIs 的重大变更

- OpenAI.RealtimeConversation:
  - 对实验性的实时 API 进行了改进。请注意，该功能区域目前正在快速开发中，并且所有更改可能不会在此处反映。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))
    - 由于一致性和清晰性，多个类型已重命名。
    - `ConversationRateLimitsUpdate`（以前称为 `ConversationRateLimitsUpdatedUpdate`）现在包含命名 `RequestDetails` 和 `TokenDetails` 属性，映射到基础 `rate_limits` 命令有效负载中的相应命名项。

### 修复的错误

- OpenAI.RealtimeConversation:
  - 修复了 `ConversationToolChoice` 字面值（如 `"required"`）的序列化和反序列化问题。 ([9de3709](https://github.com/openai/openai-dotnet/commit/9de37095eaad6f1e2e87c201fd693ac1d9757142))

### 其他更改

- 将 `System.ClientModel` 依赖项更新到 `1.2.1` 版本。 ([b0f9e5c](https://github.com/openai/openai-dotnet/commit/b0f9e5c3b9708a802afa6ce7489636d2084e7d61))
  - 这将 `System.Text.Json` 的传递性依赖项更新到版本 `6.0.10`，其中包含针对 [CVE-2024-43485](https://github.com/advisories/GHSA-8g4q-xg66-9fp4) 的安全合规修复。请注意，OpenAI 库未受到此漏洞的影响，因为它不使用 `[JsonExtensionData]` 特性。

## 2.1.0-beta.1 (2024-10-01)

> [!注意]
> 在此更新的预览库版本中，我们很高兴地为新宣布的 `/realtime` 测试 API 带来早期支持。您可以在此处了解更多有关 `/realtime` 的信息：https://openai.com/index/introducing-the-realtime-api/。考虑到该功能区域的范围和近期性，新 `RealtimeConversationClient` 将在接下来的几周内经历重大改进和更改——此版本仅旨在尽快高效地支持 `gpt-4o-realtime-preview` 的早期开发。

### 新增功能

- 在相应的场景命名空间中添加了一个新的 `RealtimeConversationClient`。 ([ff75da4](https://github.com/openai/openai-dotnet/commit/ff75da4167bc83fa85eb69ac142cab88a963ed06))
  - 此客户端映射到新的 `/realtime` 测试端点，因此标记了一个新的 `[Experimental("OPENAI002")]` 诊断标签。
  - 这是方便界面的早期版本，因此可能会发生重大更改。
  - 文档和示例将很快到来；在此期间，请参见 [场景测试文件](/tests/RealtimeConversation) 以了解基本用法。
  - 您还可以在 https://github.com/Azure-Samples/aoai-realtime-audio-sdk/tree/main/dotnet/samples/console 找到一个使用此客户端的外部示例，提供了 Azure OpenAI 的支持。

## 2.0.0 (2024-09-30)

> [!注意]
> 官方 .NET 用的 OpenAI 库的第一个稳定版本。

### 新增功能

- 支持 OpenAI 最新的旗舰模型，包括 GPT-4o、GPT-4o mini、o1-preview 和 o1-mini。
- 支持完整的 OpenAI REST API，包括：
  - 结构化输出
  - 推理令牌
  - 对助手 beta v2 的实验性支持
- 支持同步和异步 API
- 方便的 API 以简化流式聊天完成和助手的工作
- 其他大量质量提升功能，便于使用和提高生产力

### 重大变更

> [!注意]
> 以下重大变更仅适用于从之前的 2.0.0-beta.* 版本升级的用户。

- 实现了 `ChatMessageContent` 来封装 `ChatMessage`、`ChatCompletion` 和 `StreamingChatCompletionUpdate` 中内容部分的表示。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 更改 `ChatToolCall`、`StreamingChatToolCallUpdate`、`ChatFunctionCall` 和 `StreamingChatFunctionCallUpdate` 中的函数参数表示为 `BinaryData`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 将 `OpenAIClientOptions` 的 `ApplicationId` 更改为 `UserAgentApplicationId`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 将 `StreamingChatToolCallUpdate` 的 `Id` 更改为 `ToolCallId`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 将 `StreamingChatCompletionUpdate` 的 `Id` 更改为 `CompletionId`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 在已弃用的 `ChatFunctionChoice` 中将 `Auto` 和 `None` 替换为 `CreateAutoChoice()` 和 `CreateNoneChoice()`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 用 `CreateNamedChoice(string functionName)` 替换已弃用的 `ChatFunctionChoice(ChatFunction)` 构造函数。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 将 `FileClient` 重命名为 `OpenAIFileClient`，并将 `OpenAIClient` 中的相应 `GetFileClient()` 方法重命名为 `GetOpenAIFileClient()`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))
- 将 `ModelClient` 重命名为 `OpenAIModelClient`，并将 `OpenAIClient` 中的相应 `GetModelClient()` 方法重命名为 `GetOpenAIModelClient()`。 ([31c2ba6](https://github.com/openai/openai-dotnet/commit/31c2ba63c625b1b4fc2640ddf378a97e89b89167))

## 2.0.0-beta.13 (2024-09-27)

### 重大变更

- 通过将 `ModerationCategories` 和 `ModerationCategoryScores` 合并为单独的 `ModerationCategory` 属性（每个属性都有 `Flagged` 和 `Score` 属性）来重构 `ModerationResult`。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将类型 `OpenAIFileInfo` 重命名为 `OpenAIFile`，并将 `OpenAIFileInfoCollection` 重命名为 `OpenAIFileCollection`。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将类型 `OpenAIModelInfo` 重命名为 `OpenAIModel`，并将 `OpenAIModelInfoCollection` 重命名为 `OpenAIModelCollection`。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将类型 `Embedding` 重命名为 `OpenAIEmbedding`，并将 `EmbeddingCollection` 重命名为 `OpenAIEmbeddingCollection`。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将属性 `ImageUrl` 重命名为 `ImageUri`，并将方法 `FromImageUrl` 重命名为 `FromImageUri`。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将属性 `ParallelToolCallsEnabled` 重命名为 `AllowParallelToolCalls`，用于 `RunCreationOptions`、`ThreadRun` 和 `ChatCompletionOptions` 类型。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将属性 `PromptTokens` 重命名为 `InputTokenCount`，`CompletionTokens` 重命名为 `OutputTokenCount`，`TotalTokens` 重命名为 `TotalTokenCount`，在 `RunTokenUsage` 和 `RunStepTokenUsage` 类型中。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将属性 `InputTokens` 重命名为 `InputTokenCount`，`TotalTokens` 重命名为 `TotalTokenCount`，在 `EmbeddingTokenUsage` 类型中。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 将属性 `MaxPromptTokens` 重命名为 `MaxInputTokenCount`，将 `MaxCompletionTokens` 重命名为 `MaxOutputTokenCount`，在 `ThreadRun`、`RunCreationOptions` 和 `RunIncompleteReason` 类型中。 ([19ceae4](https://github.com/openai/openai-dotnet/commit/19ceae44172fdc17af1f47aa30edf4a3bddcb9d6))
- 从所有客户端删除了 `virtual` 关键字。 ([75eded5](https://github.com/openai/openai-dotnet/commit/75eded51db8c8bcec41cd894f3575374e40a4103))
- 将 `AudioTranscriptionOptions` 的 `Granularities` 属性重命名为 `TimestampGranularities`。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `AudioTranscriptionFormat` 从枚举更改为“可扩展枚举”。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `AudioTranslationFormat` 从枚举更改为“可扩展枚举”。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `GenerateImageFormat` 从枚举更改为“可扩展枚举”。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `GeneratedImageQuality` 从枚举更改为“可扩展枚举”。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `GeneratedImageStyle` 从枚举更改为“可扩展枚举”。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 删除了 `AssistantClient` 和 `VectorStoreClient` 中的重载方法，这些方法采用复杂参数，而是使用接受简单字符串 ID 的方法。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 更新 `TranscribedSegment` 类型中的 `TokenIds` 属性类型，从 `IReadOnlyList<int>` 更改为 `ReadOnlyMemory<int>`。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 更新 `EmbeddingClient` 的 `GenerateEmbeddings` 和 `GenerateEmbeddingsAsync` 方法中的 `inputs` 参数类型，从 `IEnumerable<IEnumerable<int>>` 更改为 `IEnumerable<ReadOnlyMemory<int>>`。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `ChatMessageContentPartKind` 从可扩展枚举更改为枚举。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `ChatToolCallKind` 从可扩展枚举更改为枚举。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `ChatToolKind` 从可扩展枚举更改为枚举。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `OpenAIFilePurpose` 从可扩展枚举更改为枚举。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `OpenAIFileStatus` 从可扩展枚举更改为枚举。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `OpenAIFilePurpose` 重命名为 `FilePurpose`。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 将 `OpenAIFileStatus` 重命名为 `FileStatus`。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))
- 删除了接受字符串 API 密钥和选项的构造函数。 ([a330c2e](https://github.com/openai/openai-dotnet/commit/a330c2e703e48179991905e991b0f4186a017198))

## 2.0.0-beta.12 (2024-09-20)

### 新增功能

- 该库现在支持新的 [OpenAI o1](https://openai.com/o1/) 模型系列。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - `ChatCompletionOptions` 将自动将其 `MaxOutputTokenCount` 值（从 `MaxTokens` 重命名）应用到新的 `max_completion_tokens` 请求主体属性。
  - `Usage` 包含一个新属性 `OutputTokenDetails`，其中包含一个 `ReasoningTokenCount` 值，该值将反映 `o1` 模型使用这种输出令牌的新子类别。
  - 请注意，`OutputTokenCount`（`completion_tokens`）是模型生成的显示令牌的和（在适用时）这些推理令牌。
- 助手文件搜索现在包括对 `RankingOptions` 的支持。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - 目前，仅通过协议方法可以使用 `include[]` 查询字符串参数和检索运行步骤详细结果内容。
- 在 `FileClient` 中添加了对上传 API 的支持。此 `实验性` 功能允许分多部分上传大文件。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - 该功能受 `CreateUpload`、`AddUploadPart`、`CompleteUpload` 和 `CancelUpload` 协议方法的支持。

### 重大变更

- 将 `ChatMessageContentPart` 的 `CreateTextMessageContentPart` 工厂方法重命名为 `CreateTextPart`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ChatMessageContentPart` 的 `CreateImageMessageContentPart` 工厂方法重命名为 `CreateImagePart`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ChatMessageContentPart` 的 `CreateRefusalMessageContentPart` 工厂方法重命名为 `CreateRefusalPart`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ImageChatMessageContentPartDetail` 重命名为 `ChatImageDetailLevel`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 删除 `ChatMessageContentPart` 的 `ToString` 重载。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ChatCompletionOptions` 中的 `MaxTokens` 属性重命名为 `MaxOutputTokenCount`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 在 `ChatTokenUsage` 中重命名以下属性：
  - `InputTokens` 重命名为 `InputTokenCount`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - `OutputTokens` 重命名为 `OutputTokenCount`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - `TotalTokens` 重命名为 `TotalTokenCount`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 从顶级 `OpenAI` 命名空间中删除了公共 `ListOrder` 枚举，以便在相应的子命名空间中使用单独的枚举。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `PageSize` 属性重命名为 `PageSizeLimit`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 更新删除方法以返回结果对象而不是 `bool`。受影响的方法：
  - `DeleteAssitant`、`DeleteMessage` 和 `DeleteThread` 在 `AssistantClient` 中。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - `DeleteVectorStore` 和 `RemoveFileFromStore` 在 `VectorStoreClient` 中。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - `DeleteModel` 在 `ModelClient` 中。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
  - `DeleteFile` 在 `FileClient` 中。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 从集合属性中删除了设置器。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ChatTokenLogProbabilityInfo` 重命名为 `ChatTokenLogProbabilityDetails`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ChatTokenTopLogProbabilityInfo` 重命名为 `ChatTokenTopLogProbabilityDetails`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ChatTokenLogProbabilityDetails` 和 `ChatTokenTopLogProbabilityDetails` 的 `Utf8ByteValues` 属性重命名为 `Utf8Bytes`，并将它们的类型从 `IReadOnlyList<int>` 更改为 `ReadOnlyMemory<byte>?`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `TranscribedSegment` 和 `TranscribedWord` 的 `Start` 和 `End` 属性重命名为 `StartTime` 和 `EndTime`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `TranscribedSegment` 的 `AverageLogProbability` 和 `NoSpeechProbability` 属性的类型从 `double` 更改为 `float`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `TranscribedSegment` 的 `SeekOffset` 属性的类型从 `long` 更改为 `int`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `TranscribedSegment` 的 `TokenIds` 属性的类型从 `IReadOnlyList<long>` 更改为 `IReadOnlyList<int>`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 更新 `Embedding.Vector` 属性至 `Embedding.ToFloats()` 方法。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 删除 `VectorStoreCreationHelper`、`AssistantChatMessage` 和 `ChatFunction` 的构造函数中的可选参数。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 从 `FileClient.GetFilesAsync` 和 `FileClient.GetFiles` 方法中删除可选的 `purpose` 参数，并添加 `purpose` 为必需的重载。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 将 `ModerationClient` 的 `ClassifyTextInput` 方法重命名为 `ClassifyText`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 从 `GeneratedImageCollection` 中删除重复的 `Created` 属性。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))

### 修复的错误

- 解决了导致微调作业、检查点和事件的多页查询失败的问题。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 现在可以通过 `ModelReaderWriter.Write()` 在调用 `CompleteChat` 之前序列化 `ChatCompletionOptions`。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))

### 其他更改

- 在 `ModelClient` 方法中添加对 `CancellationToken` 的支持。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))
- 在适当的位置添加了 `Obsolete` 特性，以与 REST API 中现有的弃用保持一致。 ([2ab1a94](https://github.com/openai/openai-dotnet/commit/2ab1a94269125e6bed45d134a402ad8addd8fea4))

## 2.0.0-beta.11 (2024-09-03)

### 新增功能

- 在 `OpenAI.Chat` 命名空间中添加了 `OpenAIChatModelFactory`（一个静态类，可用于在非实时测试场景中实例化 OpenAI 模型）。 ([79014ab](https://github.com/openai/openai-dotnet/commit/79014abc01a00e13d5a334d3f6529ed590b8ee98))

### 重大变更

- 更新微调分页方法 `GetJobs`、`GetEvents` 和 `GetJobCheckpoints` 以返回 `IEnumerable<ClientResult>`，而不是 `ClientResult`。 ([5773292](https://github.com/openai/openai-dotnet/commit/57732927575c6c48f30bded0afb9f5b16d4f30da))
- 更新批处理分页方法 `GetBatches` 以返回 `IEnumerable<ClientResult>`，而不是 `ClientResult`。 ([5773292](https://github.com/openai/openai-dotnet/commit/57732927575c6c48f30bded0afb9f5b16d4f30da))
- 更改 `GeneratedSpeechVoice` 从枚举更改为“可扩展枚举”。 ([79014ab](https://github.com/openai/openai-dotnet/commit/79014abc01a00e13d5a334d3f6529ed590b8ee98))
- 更改 `GeneratedSpeechFormat` 从枚举更改为“可扩展枚举”。 ([cc9169a](https://github.com/openai/openai-dotnet/commit/cc9169ad2ff92bb7312eed3b7e64e45da5da1d18))

### 修复的错误

- 修复了解析问题，导致近期对 Assistants `file_search` 的更新在流式运行时失败。尚未包含 `ranking_options` 的强类型支持，但很快将推出。 ([cc9169a](https://github.com/openai/openai-dotnet/commit/cc9169ad2ff92bb7312eed3b7e64e45da5da1d18))
- 缓解了 .NET 运行时的问题，防止 `ChatResponseFormat` 在包含 Unity 的目标上正确序列化。 ([cc9169a](https://github.com/openai/openai-dotnet/commit/cc9169ad2ff92bb7312eed3b7e64e45da5da1d18))

### 其他更改

- 恢复了默认端点 URL 中访问版本路径参数 "v1" 的功能。 ([583e9f6](https://github.com/openai/openai-dotnet/commit/583e9f6f519feeee0e2907e80bf7d5bf8302d93f))
- 在以下 API 中添加了 `Experimental` 特性：
  - `OpenAI.Assistants` 命名空间中的所有公共 API。 ([79014ab](https://github.com/openai/openai-dotnet/commit/79014abc01a00e13d5a334d3f6529ed590b8ee98))
  - `OpenAI.VectorStores` 命名空间中的所有公共 API。 ([79014ab](https://github.com/openai/openai-dotnet/commit/79014abc01a00e13d5a334d3f6529ed590b8ee98))
  - `OpenAI.Batch` 命名空间中的所有公共 API。 ([0f5e024](https://github.com/openai/openai-dotnet/commit/0f5e0249cffd42755fc9a820e65fb025fd4f986c))
  - `OpenAI.FineTuning` 命名空间中的所有公共 API。 ([0f5e024](https://github.com/openai/openai-dotnet/commit/0f5e0249cffd42755fc9a820e65fb025fd4f986c))
  - `ChatCompletionOptions.Seed` 属性。 ([0f5e024](https://github.com/openai/openai-dotnet/commit/0f5e0249cffd42755fc9a820e65fb025fd4f986c))

## 2.0.0-beta.10 (2024-08-26)

### 重大变更

- 将 `AudioClient` 的 `GenerateSpeechFromText` 方法重命名为简单的 `GenerateSpeech`。 ([d84bf54](https://github.com/openai/openai-dotnet/commit/d84bf54df14ddac4c49f6efd61467b600d34ecd7))
- 将 `OpenAIFileInfo` 的 `SizeInBytes` 属性的类型从 `long?` 更改为 `int?`。 ([d84bf54](https://github.com/openai/openai-dotnet/commit/d84bf54df14ddac4c49f6efd61467b600d34ecd7)) 

### 修复的错误

- 修复了一个新发现的错误（[#185](https://github.com/openai/openai-dotnet/pull/185)），提供 `OpenAIClientOptions` 给顶级 `OpenAIClient` 时未能传递到通过顶级客户端创建的方案性客户端（例如 `ChatClient`）中。 ([d84bf54](https://github.com/openai/openai-dotnet/commit/d84bf54df14ddac4c49f6efd61467b600d34ecd7))

### 其他更改

- 从默认端点 URL 中删除了版本路径参数 "v1"。 ([d84bf54](https://github.com/openai/openai-dotnet/commit/d84bf54df14ddac4c49f6efd61467b600d34ecd7))

## 2.0.0-beta.9 (2024-08-23)

### 新增功能

- 添加了对新的 [结构化输出](https://platform.openai.com/docs/guides/structured-outputs/introduction) 响应格式功能的支持，使聊天完成、助手和各个工具能够提供特定的 JSON Schema，以确保生成内容符合该规范。 ([3467b53](https://github.com/openai/openai-dotnet/commit/3467b535c918e72237a4c0dc36d4bda5548edb7a))
  - 要为响应内容启用顶层结构化输出，请在诸如 `ChatCompletionOptions` 的方法选项中使用 `ChatResponseFormat.CreateJsonSchemaFormat()` 和 `AssistantResponseFormat.CreateJsonSchemaFormat()` 作为 `ResponseFormat`。
  - 要为函数工具启用结构化输出，请在工具定义中将 `StrictParameterSchemaEnabled` 设置为 `true`。
  - 有关更多信息，请参见 [readme.md 中的新部分](readme.md#how-to-use-structured-outputs)。
- 聊天完成：`AssistantChatMessage`、`SystemChatMessage` 和 `ToolChatMessage` 的请求消息类型现在除了简单字符串输入外，还支持基于数组的内容部分集合。 ([3467b53](https://github.com/openai/openai-dotnet/commit/3467b535c918e72237a4c0dc36d4bda5548edb7a))
- 添加了以下模型工厂（静态类，可用于在非实时测试场景中实例化 OpenAI 模型）：
  - `OpenAIAudioModelFactory` 在 `OpenAI.Audio` 命名空间中 ([3284295](https://github.com/openai/openai-dotnet/commit/3284295e7fd9922a3395d921513473bcb483655e))
  - `OpenAIEmbeddingsModelFactory` 在 `OpenAI.Embeddings` 命名空间中 ([3284295](https://github.com/openai/openai-dotnet/commit/3284295e7fd9922a3395d921513473bcb483655e))
  - `OpenAIFilesModelFactory` 在 `OpenAI.Files` 命名空间中 ([b1ce397](https://github.com/openai/openai-dotnet/commit/b1ce397ff4f9a55db797167be9e86e138ed5d403))
  - `OpenAIImagesModelFactory` 在 `OpenAI.Images` 命名空间中 ([3284295](https://github.com/openai/openai-dotnet/commit/3284295e7fd9922a3395d921513473bcb483655e))
  - `OpenAIModelsModelFactory` 在 `OpenAI.Models` 命名空间中 ([b1ce397](https://github.com/openai/openai-dotnet/commit/b1ce397ff4f9a55db797167be9e86e138ed5d403))
  - `OpenAIModerationsModelFactory` 在 `OpenAI.Moderations` 命名空间中 ([b1ce397](https://github.com/openai/openai-dotnet/commit/b1ce397ff4f9a55db797167be9e86e138ed5d403))

### 重大变更

- 删除不明确采用 API 密钥参数或通过 `OpenAIClientOptions` 参数的端点的客户端构造函数，从而使客户实例化变得更加清晰。 ([13a9c68](https://github.com/openai/openai-dotnet/commit/13a9c68647c8d54475f1529a63b13ad711bd4ba6))
- 从所有客户端构造函数中删除了端点参数，明确说明必须通过 `OpenAIClientOptions` 参数指定替代端点。 ([13a9c68](https://github.com/openai/openai-dotnet/commit/13a9c68647c8d54475f1529a63b13ad711bd4ba6))
- 删除了 `OpenAIClient` 的 `Endpoint` `protected` 属性。 ([13a9c68](https://github.com/openai/openai-dotnet/commit/13a9c68647c8d54475f1529a63b13ad711bd4ba6))
- 将 `OpenAIClient` 的构造函数，该构造函数采取 `ClientPipeline` 参数更改为 `protected internal` 而不仅仅是 `protected`。 ([13a9c68](https://github.com/openai/openai-dotnet/commit/13a9c68647c8d54475f1529a63b13ad711bd4ba6))
- 将适用选项类中的 `User` 属性重命名为 `EndUserId`，以使其用途更明确。 ([13a9c68](https://github.com/openai/openai-dotnet/commit/13a9c68647c8d54475f1529a63b13ad711bd4ba6))

### 修复的错误

- `Assistants` 命名空间中的 `VectorStoreCreationHelper` 类型现在正确包含 `ChunkingStrategy` 属性。 ([3467b53](https://github.com/openai/openai-dotnet/commit/3467b535c918e72237a4c0dc36d4bda5548edb7a))

### 其他更改

- 由于没有内容，`ChatCompletion.ToString()` 不再抛出异常，因为针对工具调用的情况也是如此。此外，如果存在没有内容的工具调用，`ToString()` 将返回第一个可用工具调用的序列化形式。 ([3467b53](https://github.com/openai/openai-dotnet/commit/3467b535c918e72237a4c0dc36d4bda5548edb7a))

## 2.0.0-beta.8 (2024-07-31)

### 重大变更

- 将返回流式集合的方法的返回类型从 `ResultCollection` 更改为 `CollectionResult`。 ([7bdecfd](https://github.com/openai/openai-dotnet/commit/7bdecfd8d294be933c7779c7e5b6435ba8a8eab0))
- 将返回分页集合的方法的返回类型从 `PageableCollection` 更改为 `PageCollection`。 ([7bdecfd](https://github.com/openai/openai-dotnet/commit/7bdecfd8d294be933c7779c7e5b6435ba8a8eab0))
- 用户现在必须在页面集合上调用 `GetAllValues` 以直接枚举集合项。相应的协议方法返回 `IEnumerable<ClientResult>`，每个集合项表示持有一页值的单个服务响应。 ([7bdecfd](https://github.com/openai/openai-dotnet/commit/7bdecfd8d294be933c7779c7e5b6435ba8a8eab0))
- 更新 `VectorStoreFileCounts` 和 `VectorStoreFileAssociationError` 类型，从 `readonly struct` 更改为 `class`。 ([58f93c8](https://github.com/openai/openai-dotnet/commit/58f93c8d5ea080adfee8b37ae3cc034ebb06c79f))

### 修复的错误

- （[#49](https://github.com/openai/openai-dotnet/issues/49)）修复了实现不区分大小写的相等，但哈希代码区分大小写的可扩展枚举的错误。 ([0c12500](https://github.com/openai/openai-dotnet/commit/0c125002ffd791594597ef837f4d10582bdff004))
- （[#57](https://github.com/openai/openai-dotnet/issues/57)）修复了在 .NET Framework（net481）上的请求 URI 可能包含格式错误的双问号（`??`）的错误。 ([0c12500](https://github.com/openai/openai-dotnet/commit/0c125002ffd791594597ef837f4d10582bdff004))
- 为 `AssistantClient` 和 `VectorStore` 客户端的方法添加可选的 `CancellationToken` 参数，以保持与过去在 [19a65a0](https://github.com/openai/openai-dotnet/commit/19a65a0a943fa3bef1ec8504708aaa526a1ee03a) 中的更改一致。 ([d77539c](https://github.com/openai/openai-dotnet/commit/d77539ca04467c166f848953eb866012a265555c))
- 修复了 `FileSearchToolDefinition` 的 `MaxResults` 参数未能适当地序列化和反序列化的问题。 ([d77539c](https://github.com/openai/openai-dotnet/commit/d77539ca04467c166f848953eb866012a265555c))
- 为 `AssistantClient` 协议方法添加缺少的 `[EditorBrowsable(EditorBrowsableState.Never)]` 特性，以提高对强类型方法的可发现性。 ([d77539c](https://github.com/openai/openai-dotnet/commit/d77539ca04467c166f848953eb866012a265555c))

### 其他更改

- 删除 `init` 的使用，将属性更新为使用 `set`。 ([58f93c8](https://github.com/openai/openai-dotnet/commit/58f93c8d5ea080adfee8b37ae3cc034ebb06c79f))

## 2.0.0-beta.7 (2024-06-24)

### 修复的错误

- （[#84](https://github.com/openai/openai-dotnet/issues/84)）修复了在 `OpenAIClientOptions` 为 null 时添加自定义头政策时引发的 `NullReferenceException`。 ([0b97311](https://github.com/openai/openai-dotnet/commit/0b97311f58dfb28bd883d990f68d548da040a807))

## 2.0.0-beta.6 (2024-06-21)

### 新增功能

- `OrganizationId` 和 `ProjectId` 现在出现在 `OpenAIClientOptions` 中。当实例化客户端时，提供一个设置了这些属性的 `OpenAIClientOptions` 实例将导致客户端添加适当的请求头，消除手动配置头的需要。 ([9ee7dff](https://github.com/openai/openai-dotnet/commit/9ee7dff064a9412c069a793ff62096b8db4aa43d))

### 修复的错误

- （[#72](https://github.com/openai/openai-dotnet/issues/72)）修复了使用 `multipart/form-data` 的操作中心添加的 `filename` 请求编码的问题，包括 `files` 和 `audio`。 ([2ba8e69](https://github.com/openai/openai-dotnet/commit/2ba8e69512e187ea0b761edda8bce2cd5c79c58))
- （[#79](https://github.com/openai/openai-dotnet/issues/79)）修复了在调用创建的 Assistants API 消息的线程时的硬编码 `user` 角色。 ([d665b61](https://github.com/openai/openai-dotnet/commit/d665b61fc7ef1ada00a8ef5c00d1a47d276be032))
- 修复了非流式 Assistants API 运行步骤详细信息在存在时未能报告代码解释器日志的问题。 ([d665b61](https://github.com/openai/openai-dotnet/commit/d665b61fc7ef1ada00a8ef5c00d1a47d276be032))

### 重大变更

**助手（测试）**：

- `AssistantClient.CreateMessage()` 和 `ThreadInitializationMessage` 的显式构造函数现在需要一个 `MessageRole` 参数。这适当地启用了创建表示新线程上的对话历史的助手消息的能力。 ([d665b61](https://github.com/openai/openai-dotnet/commit/d665b61fc7ef1ada00a8ef5c00d1a47d276be032))

## 2.0.0-beta.5 (2024-06-14)

### 新增功能

- API 更新，当前基于 [openai/openai-openapi@dd73070b](https://github.com/openai/openai-openapi/commit/dd73070b1d507645d24c249a63ebebd3ec38c0cb) ([1af6569](https://github.com/openai/openai-dotnet/commit/1af6569e2ceae9d840b8826e42d7e3b2569b43f6))
  - 这包括对助手 `FileSearchToolDefinition` 的 `MaxResults`，对助手和聊天中函数工具的 `ParallelToolCallsEnabled`，以及对助手向量存储的 `FileChunkingStrategy`。
- 大多数方法现在直接提供可选的 `CancellationToken` 参数，消除了使用协议方法的必要性。 ([19a65a0](https://github.com/openai/openai-dotnet/commit/19a65a0a943fa3bef1ec8504708aaa526a1ee03a))

### 修复的错误

- （[#30](https://github.com/openai/openai-dotnet/issues/30)）减轻了一个 .NET 运行时问题，防止聊天消息内容和其他几种类型在包含 mono 和 wasm 的目标上正确序列化。 ([896b9e0](https://github.com/openai/openai-dotnet/commit/896b9e0bc60f0ace90bd0d1af1254cf2680f8df6))
- 助手：修复了在流式运行时接收代码解释器运行步骤日志时引发异常的问题。 ([207d597](https://github.com/openai/openai-dotnet/commit/207d59762e7eeb666b7ab2728a0bbee7c0cdd918))
- 修复了并发条件，可能导致 `multipart/form-data` 请求不再生成随机内容部分边界（没有直接场景影响）。 ([7cacdee](https://github.com/openai/openai-dotnet/commit/7cacdee2366df3cfa7e6c43bb050da54d23f8db9))

### 重大变更

**助手（测试）**：

- 根据 [openai/openai-openapi@dd73070b](https://github.com/openai/openai-openapi/commit/dd73070b1d507645d24c249a63ebebd3ec38c0cb)，从助手 `TextAnnotation` 和 `TextAnnotationUpdate` 中移除了 `InputQuote`。 ([1af6569](https://github.com/openai/openai-dotnet/commit/1af6569e2ceae9d840b8826e42d7e3b2569b43f6))

### 其他更改

- 向存储库添加了基于环境变量的测试项目，覆盖基准场景。 ([db6328a](https://github.com/openai/openai-dotnet/commit/db6328accdd7927f19915cdc5412eb841f2447c1))
- 整个项目的构建/分析器警告已清理。 ([45fc4d7](https://github.com/openai/openai-dotnet/commit/45fc4d72c12314aea83264ebe2e1dc18870e5c06), [b1fa082](https://github.com/openai/openai-dotnet/commit/b1fa0828a875906ef33ffe43ff1cd1a85fbd1a60), [22ab606](https://github.com/openai/openai-dotnet/commit/22ab606b867bbe0ea7f6918843dbc5e11dfe78eb))
- 主动对齐库对服务端事件（SSE）处理的实现，与传入的 `System.Net.ServerSentEvents` 命名空间的源保持一致。 ([674e0f7](https://github.com/openai/openai-dotnet/commit/674e0f773b26a22eb039e879539c4c7a44fdffdd))

## 2.0.0-beta.4 (2024-06-10)

### 新增功能

- 添加新的内置助手，简化文本消息内容的使用 ([1c40de6](https://github.com/openai/openai-dotnet/commit/1c40de673a67ddf834b673aaabb94b2c42076e03))

### 修复的错误

- 优化嵌入反序列化，并解决在大端系统上的正确性问题。 ([e28b5a7](https://github.com/openai/openai-dotnet/commit/e28b5a7787df4b1baa772406b09a0f650a45c77f))
- 通过正则表达式优化 b64_json 消息解析。 ([efd76b5](https://github.com/openai/openai-dotnet/commit/efd76b50f094c585350240aea051ba342c6f6622))

## 2.0.0-beta.3 (2024-06-07)

### 修复的错误

- 删除了一个多余的包引用。 ([5874f53](https://github.com/openai/openai-dotnet/commit/5874f533722ab46a3e077dacb6c3474e0ecca96e))

## 2.0.0-beta.2 (2024-06-06)

### 修复的错误

- 解决了一个程序集属性问题。 ([bf21eb5](https://github.com/openai/openai-dotnet/commit/bf21eb5ad367aaac418dbbf320f98187fee5089a))
- 向包中添加了迁移指南。 ([f150666](https://github.com/openai/openai-dotnet/commit/f150666cd2ed552720207098b3b604a8e1ca73df))

## 2.0.0-beta.1 (2024-06-06)

### 新增功能

这是官方的 OpenAI 客户端库，用于 C# / .NET。它提供了方便的访问 OpenAI REST API，从 .NET 应用程序中支持所有最新功能。它根据我们的 [OpenAPI 规范](https://github.com/openai/openai-openapi) 与 Microsoft 合作生成。

### 重大变更

如果您是从版本 1.11.0 或更早版本迁移的用户，我们将很快共享一份迁移指南，以帮助您入门。

- ***附录：**现在可以查看 [迁移指南](https://github.com/openai/openai-dotnet/blob/main/MigrationGuide.md)。*
