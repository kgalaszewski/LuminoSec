namespace LuminoSec.Api.Features.ArchitectureReview;

public interface IArchitectureReviewService
{
    Task<ArchitectureReviewResult> AnalyzeAsync(
        ArchitectureReviewRequest request,
        CancellationToken cancellationToken = default);
}
