using LuminoSec.Api.Rules;

namespace LuminoSec.Api.Scoring;

internal interface ISecurityScorer
{
    int Score(IReadOnlyList<RuleFinding> findings);
}
