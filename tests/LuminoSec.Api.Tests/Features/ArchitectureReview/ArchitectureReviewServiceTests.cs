using LuminoSec.Api.Ai;
using LuminoSec.Api.Features.ArchitectureReview;
using LuminoSec.Api.Rules;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Tests.Features.ArchitectureReview;

public class ArchitectureReviewServiceTests
{
    [Fact]
    public async Task AnalyzeAsync_CombinesLlmRulesAndScoreIntoResult()
    {
        // Arrange
        var sut = new ArchitectureReviewService(new LlmClient(), new RulesEngine(), new SecurityScorer());
        var request = new ArchitectureReviewRequest("any architecture description");

        // Act
        var result = await sut.AnalyzeAsync(request);

        // Assert
        Assert.Equal(72, result.SecurityScore);
        Assert.False(string.IsNullOrWhiteSpace(result.AiSummary));
        Assert.NotEmpty(result.Findings);
    }
}
