using LuminoSec.Api.Rules;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Tests.Scoring;

public class SecurityScorerTests
{
    [Fact]
    public void Score_ReturnsFixedMockValue()
    {
        // Arrange
        var sut = new SecurityScorer();
        var findings = Array.Empty<RuleFinding>();

        // Act
        var score = sut.Score(findings);

        // Assert
        Assert.Equal(72, score);
    }
}
