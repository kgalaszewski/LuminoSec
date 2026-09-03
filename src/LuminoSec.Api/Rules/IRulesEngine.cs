namespace LuminoSec.Api.Rules;

internal interface IRulesEngine
{
    IReadOnlyList<RuleFinding> Evaluate(string architectureDescription);
}
