using LuminoSec.Api.RulesEngine;

namespace LuminoSec.Api.Scoring;

internal sealed class StubSecurityScorer : ISecurityScorer
{
    public int Score(IReadOnlyList<RuleFinding> findings) => 72;
}
