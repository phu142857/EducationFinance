using AdminPortal.Application;
using AdminPortal.API.OpenApi;
using AdminPortal.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AdminPortal.API;

public partial class Program
{
    public static void Main(string[] args)
    {
        var app = CreateApplication(args);
        app.Run();
    }

    public static WebApplication CreateApplication(string[]? args = null)
    {
        var builder = WebApplication.CreateBuilder(args ?? []);

        builder.Services
            .AddAdminPortalApplication();
        builder.Services.AddAdminPortalInfrastructure(builder.Configuration);
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Authentication:Authority"];
                options.Audience = builder.Configuration["Authentication:Audience"];
                options.RequireHttpsMetadata = builder.Configuration.GetValue("Authentication:RequireHttpsMetadata", true);
                options.MapInboundClaims = false;
            });
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddOperationTransformer<BearerSecurityRequirementTransformer>();
        });

        var app = builder.Build();

        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "AdminPortal API v1");
            options.RoutePrefix = "swagger";
            options.DisplayRequestDuration();
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
            .WithName("AdminPortalHealth")
            .AllowAnonymous();

        return app;
    }
}
