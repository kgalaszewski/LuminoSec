using Amazon;
using Amazon.BedrockRuntime;
using Amazon.Runtime.Credentials;
using LuminoSec.Api.Ai;
using LuminoSec.Api.Rules;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Features.ArchitectureReview;

internal static class ArchitectureReviewServiceCollectionExtensions
{
    internal static IServiceCollection AddArchitectureReviewFeature(this IServiceCollection services)
    {
        // Bedrock's default identity resolver tries bearer-token auth (Bedrock
        // API keys) before falling back to SigV4 — explicitly resolving
        // AWSCredentials here forces the SigV4/IAM role path we actually use.
        services.AddSingleton<IAmazonBedrockRuntime>(
            _ => new AmazonBedrockRuntimeClient(
                DefaultAWSCredentialsIdentityResolver.GetCredentials(),
                RegionEndpoint.EUCentral1));
        services.AddSingleton<BedrockOptions>();
        services.AddSingleton<ILlmClient, LlmClient>();
        services.AddSingleton<IRulesEngine, RulesEngine>();
        services.AddSingleton<ISecurityScorer, SecurityScorer>();
        services.AddScoped<IArchitectureReviewService, ArchitectureReviewService>();

        return services;
    }
}
