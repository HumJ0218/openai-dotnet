## 使用 OpenTelemetry 的可观察性

> 注意：
> OpenAI .NET SDK 的仪器化仍在开发中，尚未完成。请参阅 [可用源和测量](#available-sources-and-meters) 部分获取涵盖的操作列表。

OpenAI .NET 库使用 .NET [跟踪](https://learn.microsoft.com/dotnet/core/diagnostics/distributed-tracing) 和 [度量](https://learn.microsoft.com/dotnet/core/diagnostics/metrics-instrumentation) API 进行分布式跟踪和指标仪器化，并支持 [OpenTelemetry](https://learn.microsoft.com/dotnet/core/diagnostics/observability-with-otel)。

OpenAI .NET 仪器化遵循 [生成 AI 系统的 OpenTelemetry 语义约定](https://github.com/open-telemetry/semantic-conventions/tree/main/docs/gen-ai)。

### 如何启用

该仪器化是 **实验性的** - 监测项目的数量和语义可能会有所变化。

要启用仪器化：

1. 使用以下选项之一设置仪器化特性标志：

   - 将 `OPENAI_EXPERIMENTAL_ENABLE_OPEN_TELEMETRY` 环境变量设置为 `"true"`
   - 在应用程序启动并初始化任何 OpenAI 客户端之前，在您的应用程序代码中将 `OpenAI.Experimental.EnableOpenTelemetry` 上下文开关设置为 true。例如：

     ```csharp
     AppContext.SetSwitch("OpenAI.Experimental.EnableOpenTelemetry", true);
     ```

2. 启用 OpenAI 监测：

   ```csharp
   builder.Services.AddOpenTelemetry()
       .WithTracing(b =>
       {
           b.AddSource("OpenAI.*")
             ...
            .AddOtlpExporter();
       })
       .WithMetrics(b =>
       {
           b.AddMeter("OpenAI.*")
            ...
            .AddOtlpExporter();
       });
   ```

   分布式跟踪通过 `AddSource("OpenAI.*")` 启用，这告诉 OpenTelemetry 监听所有名称以 `OpenAI.*` 开头的 [ActivitySources](https://learn.microsoft.com/dotnet/api/system.diagnostics.activitysource)。

   同样，度量通过 `AddMeter("OpenAI.*")` 配置，启用所有与 OpenAI 相关的 [Meters](https://learn.microsoft.com/dotnet/api/system.diagnostics.metrics.meter)。

请考虑启用 [HTTP 客户端仪器化](https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Http)，以查看您的应用程序所做的所有 HTTP 客户端调用，包括 OpenAI SDK 的调用。
有关更多详细信息，请查看 [OpenTelemetry 文档](https://opentelemetry.io/docs/languages/net/getting-started/)。

### 可用源和测量

可用的源和测量如下：

- `OpenAI.ChatClient` - 记录 `ChatClient` 操作的跟踪和指标（不包括流和协议方法，这些方法尚未仪器化）
