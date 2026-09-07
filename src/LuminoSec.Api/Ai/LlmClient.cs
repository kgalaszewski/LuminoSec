using System.Text;
using System.Text.Json;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;

namespace LuminoSec.Api.Ai;

internal sealed class LlmClient(
    IAmazonBedrockRuntime bedrockRuntime,
    BedrockOptions options,
    ILogger<LlmClient> logger) : ILlmClient
{
    public async Task<string> InvokeAsync(string prompt, CancellationToken cancellationToken = default)
    {
        var requestBody = JsonSerializer.Serialize(new
        {
            anthropic_version = options.AnthropicVersion,
            max_tokens = options.MaxTokens,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        });

        logger.LogInformation("Invoking Bedrock model {ModelId}", options.ModelId);

        try
        {
            var response = await bedrockRuntime.InvokeModelAsync(
                new InvokeModelRequest
                {
                    ModelId = options.ModelId,
                    ContentType = "application/json",
                    Accept = "application/json",
                    Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody))
                },
                cancellationToken);

            using var reader = new StreamReader(response.Body);
            var responseBody = await reader.ReadToEndAsync(cancellationToken);

            using var document = JsonDocument.Parse(responseBody);
            var text = document.RootElement
                .GetProperty("content")[0]
                .GetProperty("text")
                .GetString() ?? string.Empty;

            logger.LogInformation("Bedrock model {ModelId} responded with {Length} characters", options.ModelId, text.Length);

            return text;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Bedrock invocation failed for model {ModelId}", options.ModelId);
            throw;
        }
    }
}
