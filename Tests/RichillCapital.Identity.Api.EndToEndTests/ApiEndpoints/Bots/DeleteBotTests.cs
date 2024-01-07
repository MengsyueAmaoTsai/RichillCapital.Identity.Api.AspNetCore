using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using MongoDB.Driver;

using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Identity.Api.Endpoints.Bots;

namespace RichillCapital.Identity.Api.EndToEndTests.ApiEndpoints.Bots;

[TestClass]
public sealed class DeleteBotTests
{
    private const string Route = "/api/bots";

    private static readonly CreateBotRequest CreateBotRequest = new()
    {
        BotId = "XQ-IS-TW-M5-0100",
        Name = "BotName",
        Description = "Description",
        TradingPlatform = "XQ",
    };

    private static TestContext _testContext;
    private static WebApplicationFactory<Program> _factory;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        _testContext = testContext;
        _factory = new CustomWebApplicationFactory<Program>();
    }

    [TestMethod]
    public async Task When_BotNotFound_Should_Return_404()
    {
        using var client = _factory.CreateClient();
        var response = await client.DeleteAsync(Route + $"/{123}", default);

        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [TestMethod]
    public async Task When_BotFound_Should_Return_204()
    {
        using var client = _factory.CreateClient();

        _ = await client.PostAsJsonAsync(Route, CreateBotRequest);

        var response = await client.DeleteAsync(Route + $"/{CreateBotRequest.BotId}", default);

        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}