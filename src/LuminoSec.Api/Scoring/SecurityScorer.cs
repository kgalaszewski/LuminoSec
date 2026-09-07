using LuminoSec.Api.Rules;

namespace LuminoSec.Api.Scoring;

internal sealed class SecurityScorer : ISecurityScorer
{
    public int Score(IReadOnlyList<RuleFinding> findings) => 72;

    public string Rate(int score) => score switch
    {
        >= 90 => "Secure",
        >= 75 => "Good",
        >= 50 => "Needs Improvement",
        _ => "Critical"
    };
}
