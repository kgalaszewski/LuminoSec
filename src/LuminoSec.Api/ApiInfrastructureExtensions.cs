namespace LuminoSec.Api;

internal static class ApiInfrastructureExtensions
{
    private const string FrontendCorsPolicy = "FrontendCorsPolicy";

    internal static IServiceCollection AddApiInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddHealthChecks();

        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        services.AddCors(options =>
        {
            options.AddPolicy(FrontendCorsPolicy, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    internal static WebApplication UseApiInfrastructure(this WebApplication app)
    {
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

        return app;
    }
}
