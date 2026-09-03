using LuminoSec.Api.Ai;
using LuminoSec.Api.RulesEngine;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Features.ArchitectureReview;

internal static class ArchitectureReviewServiceCollectionExtensions
{
    internal static IServiceCollection AddArchitectureReviewFeature(this IServiceCollection services)
    {
        services.AddSingleton<ILlmClient, LlmClient>();
        services.AddSingleton<IRulesEngine, StubRulesEngine>();
        services.AddSingleton<ISecurityScorer, StubSecurityScorer>();
        services.AddScoped<IArchitectureReviewService, ArchitectureReviewService>();

        return services;
    }
}
