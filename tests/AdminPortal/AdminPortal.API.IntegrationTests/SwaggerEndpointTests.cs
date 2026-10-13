using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using ApiProgram = AdminPortal.API.Program;

namespace AdminPortal.API.IntegrationTests;

public sealed class SwaggerEndpointTests
{
    [Fact]
    public async Task Swagger_UI_and_Bearer_security_scheme_are_available()
    {
        await using var app = ApiProgram.CreateApplication();
        app.Urls.Add("http://127.0.0.1:0");
        await app.StartAsync();

        var server = app.Services.GetRequiredService<IServer>();
        var address = Assert.Single(server.Features.Get<IServerAddressesFeature>()!.Addresses);
        using var client = new HttpClient { BaseAddress = new Uri(address) };

        var swaggerUi = await client.GetAsync("/swagger/index.html");
        var openApi = await client.GetAsync("/openapi/v1.json");

        Assert.Equal(HttpStatusCode.OK, swaggerUi.StatusCode);
        Assert.Contains("Swagger UI", await swaggerUi.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        Assert.Equal(HttpStatusCode.OK, openApi.StatusCode);

        using var document = await JsonDocument.ParseAsync(await openApi.Content.ReadAsStreamAsync());
        var bearer = document.RootElement
            .GetProperty("components")
            .GetProperty("securitySchemes")
            .GetProperty("Bearer");
        Assert.Equal("http", bearer.GetProperty("type").GetString());
        Assert.Equal("bearer", bearer.GetProperty("scheme").GetString());
        Assert.Equal("JWT", bearer.GetProperty("bearerFormat").GetString());
    }
}
