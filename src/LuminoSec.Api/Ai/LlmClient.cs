namespace LuminoSec.Api.Ai;

internal sealed class LlmClient : ILlmClient
{
    public Task<string> InvokeAsync(string prompt, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            "MOCK AI RESPONSE: this architecture shows a suspicious lack of rubber ducks. " +
            "Recommend adding at least one (1) rubber duck to the critical path before production.");
    }
}
