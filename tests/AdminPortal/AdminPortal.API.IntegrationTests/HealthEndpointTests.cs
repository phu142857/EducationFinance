using System.Net;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using ApiProgram = AdminPortal.API.Program;

namespace AdminPortal.API.IntegrationTests;

public sealed class HealthEndpointTests
{
    [Fact]
    public async Task Health_endpoint_returns_success_when_host_starts()
    {
        await using var app = ApiProgram.CreateApplication();
        app.Urls.Add("http://127.0.0.1:0");

        await app.StartAsync();

        var server = app.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();
        var address = Assert.Single(addresses!.Addresses);
        using var client = new HttpClient { BaseAddress = new Uri(address) };

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Healthy", await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
    }
}
