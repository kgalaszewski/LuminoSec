namespace LuminoSec.Api.Features.ArchitectureReview;

public static class ArchitectureReviewEndpoints
{
    public static void MapArchitectureReviewEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/architecture-review/analyze", async (
                ArchitectureReviewRequest request,
                IArchitectureReviewService service,
                CancellationToken cancellationToken) =>
            {
                var result = await service.AnalyzeAsync(request, cancellationToken);
                return Results.Ok(result);
            })
            .WithName("AnalyzeArchitecture");
    }
}
