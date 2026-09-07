using System.Text;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using LuminoSec.Api.Ai;
using LuminoSec.Api.Features.ArchitectureReview;
using LuminoSec.Api.Rules;
using LuminoSec.Api.Scoring;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace LuminoSec.Api.Tests.Features.ArchitectureReview;

public class ArchitectureReviewServiceTests
{
    [Fact]
    public async Task AnalyzeAsync_CombinesLlmRulesAndScoreIntoResult()
    {
        // Arrange
        var bedrockRuntime = Substitute.For<IAmazonBedrockRuntime>();
        var responseJson = """{"content":[{"type":"text","text":"mocked bedrock response"}]}""";
        bedrockRuntime
            .InvokeModelAsync(Arg.Any<InvokeModelRequest>(), Arg.Any<CancellationToken>())
            .Returns(new InvokeModelResponse
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(responseJson))
            });
        var llmClient = new LlmClient(bedrockRuntime, new BedrockOptions(), NullLogger<LlmClient>.Instance);
        var sut = new ArchitectureReviewService(
            llmClient,
            new RulesEngine(),
            new SecurityScorer(),
            NullLogger<ArchitectureReviewService>.Instance);
        var request = new ArchitectureReviewRequest("any architecture description");

        // Act
        var result = await sut.AnalyzeAsync(request);

        // Assert
        Assert.Equal(72, result.SecurityScore);
        Assert.Equal("Needs Improvement", result.SecurityRating);
        Assert.False(string.IsNullOrWhiteSpace(result.AiSummary));
        Assert.NotEmpty(result.Findings);
    }

    [Fact]
    public async Task AnalyzeAsync_FallsBackToPlaceholderSummaryWhenLlmFails()
    {
        // Arrange
        var bedrockRuntime = Substitute.For<IAmazonBedrockRuntime>();
        bedrockRuntime
            .InvokeModelAsync(Arg.Any<InvokeModelRequest>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new InvalidOperationException("boom"));
        var llmClient = new LlmClient(bedrockRuntime, new BedrockOptions(), NullLogger<LlmClient>.Instance);
        var sut = new ArchitectureReviewService(
            llmClient,
            new RulesEngine(),
            new SecurityScorer(),
            NullLogger<ArchitectureReviewService>.Instance);
        var request = new ArchitectureReviewRequest("any architecture description");

        // Act
        var result = await sut.AnalyzeAsync(request);

        // Assert
        Assert.Equal("AI analysis is temporarily unavailable. Please try again later.", result.AiSummary);
        Assert.Empty(result.Findings);
        Assert.Equal(0, result.SecurityScore);
        Assert.Equal("Critical", result.SecurityRating);
    }
}
