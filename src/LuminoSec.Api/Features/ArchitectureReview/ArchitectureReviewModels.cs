namespace LuminoSec.Api.Features.ArchitectureReview;

public sealed record ArchitectureReviewRequest(string ArchitectureDescription);

public sealed record ArchitectureReviewResult(
    int SecurityScore,
    string AiSummary,
    IReadOnlyList<string> Findings);
