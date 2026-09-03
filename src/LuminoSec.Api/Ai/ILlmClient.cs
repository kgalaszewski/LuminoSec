namespace LuminoSec.Api.Ai;

// Abstraction over the LLM provider (AWS Bedrock, via cross-region inference
// profile — see architecture decision log). Kept separate from any specific
// provider so it can be swapped/mocked without touching callers.
public interface ILlmClient
{
    Task<string> InvokeAsync(string prompt, CancellationToken cancellationToken = default);
}
