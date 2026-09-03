namespace LuminoSec.Api.RulesEngine;

public interface IRulesEngine
{
    IReadOnlyList<RuleFinding> Evaluate(string architectureDescription);
}

public sealed record RuleFinding(string RuleId, string Severity, string Message);
