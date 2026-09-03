namespace LuminoSec.Api.RulesEngine;

internal interface IRulesEngine
{
    IReadOnlyList<RuleFinding> Evaluate(string architectureDescription);
}
