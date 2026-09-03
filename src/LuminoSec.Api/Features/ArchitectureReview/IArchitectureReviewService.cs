namespace LuminoSec.Api.Features.ArchitectureReview;

internal interface IArchitectureReviewService
{
    Task<ArchitectureReviewResult> AnalyzeAsync(
        ArchitectureReviewRequest request,
        CancellationToken cancellationToken = default);
}
