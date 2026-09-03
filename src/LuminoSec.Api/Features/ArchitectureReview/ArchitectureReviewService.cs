using LuminoSec.Api.Ai;
using LuminoSec.Api.RulesEngine;
using LuminoSec.Api.Scoring;

namespace LuminoSec.Api.Features.ArchitectureReview;

public sealed class ArchitectureReviewService(
    ILlmClient llmClient,
    IRulesEngine rulesEngine,
    ISecurityScorer securityScorer) : IArchitectureReviewService
{
    public async Task<ArchitectureReviewResult> AnalyzeAsync(
        ArchitectureReviewRequest request,
        CancellationToken cancellationToken = default)
    {
        var aiSummary = await llmClient.InvokeAsync(request.ArchitectureDescription, cancellationToken);
        var findings = rulesEngine.Evaluate(request.ArchitectureDescription);
        var score = securityScorer.Score(findings);

        return new ArchitectureReviewResult(
            score,
            aiSummary,
            findings.Select(f => $"[{f.Severity}] {f.RuleId}: {f.Message}").ToList());
    }
}
