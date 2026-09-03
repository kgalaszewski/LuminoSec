namespace LuminoSec.Api.Rules;

internal sealed class RulesEngine : IRulesEngine
{
    public IReadOnlyList<RuleFinding> Evaluate(string architectureDescription)
    {
        return
        [
            new RuleFinding("MOCK-001", "Low", "This is a stubbed finding — real rules aren't implemented yet.")
        ];
    }
}
