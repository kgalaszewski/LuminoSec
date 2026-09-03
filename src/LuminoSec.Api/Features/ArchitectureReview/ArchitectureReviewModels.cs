namespace LuminoSec.Api.Features.ArchitectureReview;

internal sealed record ArchitectureReviewRequest(string ArchitectureDescription);

internal sealed record ArchitectureReviewResult(
    int SecurityScore,
    string AiSummary,
    IReadOnlyList<string> Findings);
