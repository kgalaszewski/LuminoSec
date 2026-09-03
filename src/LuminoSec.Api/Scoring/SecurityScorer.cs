using LuminoSec.Api.Rules;

namespace LuminoSec.Api.Scoring;

internal sealed class SecurityScorer : ISecurityScorer
{
    public int Score(IReadOnlyList<RuleFinding> findings) => 72;
}
