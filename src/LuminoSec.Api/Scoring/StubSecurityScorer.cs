using LuminoSec.Api.RulesEngine;

namespace LuminoSec.Api.Scoring;

public sealed class StubSecurityScorer : ISecurityScorer
{
    public int Score(IReadOnlyList<RuleFinding> findings) => 72;
}
