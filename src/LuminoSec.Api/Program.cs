using LuminoSec.Api;
using LuminoSec.Api.Features.ArchitectureReview;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiInfrastructure(builder.Configuration);
builder.Services.AddArchitectureReviewFeature();

var app = builder.Build();

app.UseApiInfrastructure();
app.MapArchitectureReviewEndpoints();

app.Run();
