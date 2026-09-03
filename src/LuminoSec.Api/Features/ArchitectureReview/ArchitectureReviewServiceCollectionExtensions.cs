using LuminoSec.Api.Ai;
using LuminoSec.Api.Rules;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Features.ArchitectureReview;

internal static class ArchitectureReviewServiceCollectionExtensions
{
    internal static IServiceCollection AddArchitectureReviewFeature(this IServiceCollection services)
    {
        services.AddSingleton<ILlmClient, LlmClient>();
        services.AddSingleton<IRulesEngine, RulesEngine>();
        services.AddSingleton<ISecurityScorer, SecurityScorer>();
        services.AddScoped<IArchitectureReviewService, ArchitectureReviewService>();

        return services;
    }
}
