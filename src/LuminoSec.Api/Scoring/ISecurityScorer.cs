using LuminoSec.Api.RulesEngine;

namespace LuminoSec.Api.Scoring;

public interface ISecurityScorer
{
    int Score(IReadOnlyList<RuleFinding> findings);
}
