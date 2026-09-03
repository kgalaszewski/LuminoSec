using LuminoSec.Api.Ai;
using LuminoSec.Api.Features.ArchitectureReview;
using LuminoSec.Api.RulesEngine;
using LuminoSec.Api.Scoring;

const string FrontendCorsPolicy = "FrontendCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<ILlmClient, MockLlmClient>();
builder.Services.AddSingleton<IRulesEngine, StubRulesEngine>();
builder.Services.AddSingleton<ISecurityScorer, StubSecurityScorer>();
builder.Services.AddScoped<IArchitectureReviewService, ArchitectureReviewService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);

app.MapHealthChecks("/health");
app.MapArchitectureReviewEndpoints();

app.Run();
