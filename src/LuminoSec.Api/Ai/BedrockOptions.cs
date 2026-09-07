namespace LuminoSec.Api.Ai;

// TODO: move these into AWS Secrets Manager (or Parameter Store) once the
// API is deployed, and read them at startup via the ECS task role — hardcoded
// defaults here only for the local/"simplest possible" Bedrock integration.
internal sealed class BedrockOptions
{
    public string ModelId { get; init; } = "global.anthropic.claude-sonnet-4-5-20250929-v1:0";

    public string AnthropicVersion { get; init; } = "bedrock-2023-05-31";

    public int MaxTokens { get; init; } = 1024;
}
