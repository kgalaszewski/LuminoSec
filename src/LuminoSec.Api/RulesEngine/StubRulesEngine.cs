namespace LuminoSec.Api.RulesEngine;

public sealed class StubRulesEngine : IRulesEngine
{
    public IReadOnlyList<RuleFinding> Evaluate(string architectureDescription)
    {
        return
        [
            new RuleFinding("MOCK-001", "Low", "This is a stubbed finding — real rules aren't implemented yet.")
        ];
    }
}
