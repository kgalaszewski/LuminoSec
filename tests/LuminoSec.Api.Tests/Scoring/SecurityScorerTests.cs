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

    [Theory]
    [InlineData(100, "Secure")]
    [InlineData(90, "Secure")]
    [InlineData(89, "Good")]
    [InlineData(75, "Good")]
    [InlineData(74, "Needs Improvement")]
    [InlineData(50, "Needs Improvement")]
    [InlineData(49, "Critical")]
    [InlineData(0, "Critical")]
    public void Rate_MapsScoreToExpectedBand(int score, string expectedRating)
    {
        // Arrange
        var sut = new SecurityScorer();

        // Act
        var rating = sut.Rate(score);

        // Assert
        Assert.Equal(expectedRating, rating);
    }
}
