using LuminoSec.Api.RulesEngine;

namespace LuminoSec.Api.Scoring;

internal interface ISecurityScorer
{
    int Score(IReadOnlyList<RuleFinding> findings);
}
