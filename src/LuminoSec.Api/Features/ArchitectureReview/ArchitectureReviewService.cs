using LuminoSec.Api.Ai;
using LuminoSec.Api.Rules;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Features.ArchitectureReview;

internal sealed class ArchitectureReviewService(
    ILlmClient llmClient,
    IRulesEngine rulesEngine,
    ISecurityScorer securityScorer,
    ILogger<ArchitectureReviewService> logger) : IArchitectureReviewService
{
    private const string FallbackAiSummary = "AI analysis is temporarily unavailable. Please try again later.";

    public async Task<ArchitectureReviewResult> AnalyzeAsync(
        ArchitectureReviewRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Analyzing architecture description ({Length} characters)", request.ArchitectureDescription.Length);

        var prompt = $"""
            You are a cloud security architecture reviewer. Analyze the following
            system architecture description and provide a concise summary of the
            main security risks and recommendations. Keep it focused and actionable.
            If you decide to propose X over Y, explain why X is better than X in terms
            of security. If there is no risk at all in the given description, don't
            try to make up something that is not necessary, instead try to propose
            what could be done to improve the security even more.

            Architecture description:
            {request.ArchitectureDescription}
            """;

        string aiSummary;
        IReadOnlyList<RuleFinding> findings;
        int score;
        string rating;
        try
        {
            aiSummary = await llmClient.InvokeAsync(prompt, cancellationToken);
            findings = rulesEngine.Evaluate(request.ArchitectureDescription);
            score = securityScorer.Score(findings);
            rating = securityScorer.Rate(score);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Architecture analysis failed — returning a fallback result");
            aiSummary = FallbackAiSummary;
            findings = [];
            score = 0;
            rating = securityScorer.Rate(score);
        }

        logger.LogInformation("Analysis complete: score {Score} ({Rating}), {FindingCount} findings", score, rating, findings.Count);

        return new ArchitectureReviewResult(
            score,
            rating,
            aiSummary,
            findings.Select(f => $"[{f.Severity}] {f.RuleId}: {f.Message}").ToList());
    }
}
