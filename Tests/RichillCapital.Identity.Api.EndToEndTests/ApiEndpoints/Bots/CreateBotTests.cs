using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using RichillCapital.Identity.Api.Endpoints.Bots;

namespace RichillCapital.Identity.Api.EndToEndTests.ApiEndpoints.Bots;

[TestClass]
public sealed class CreateBotTests
{
    private const string Route = "/api/bots";

    private static readonly CreateBotRequest Request = new()
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
    public async Task When_BotIdIsEmpty_Should_Return_400()
    {
        // Act
        using var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync(
            Route,
            Request with { BotId = string.Empty });

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task When_NameIsEmpty_Should_Return_400()
    {
        // Act
        using var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync(
            Route,
            Request with { Name = string.Empty });

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task When_DescriptionIsEmpty_Should_Return_400()
    {
        // Act
        using var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync(
            Route,
            Request with { Name = string.Empty });

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task When_TradingPlatformIsInvalid_Should_Return_400()
    {
        // Act
        using var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync(
            Route,
            Request with { TradingPlatform = string.Empty });

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task When_BotIdIsNotUnique_Should_Return_409()
    {
        // Act
        using var client = _factory.CreateClient();
        _ = await client.PostAsJsonAsync(
            Route,
            Request);

        var response = await client.PostAsJsonAsync(
            Route,
            Request with { Name = "Name2" });

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [TestMethod]
    public async Task When_NameIsNotUnique_Should_Return_409()
    {
        // Act
        using var client = _factory.CreateClient();
        _ = await client.PostAsJsonAsync(
            Route,
            Request);

        var response = await client.PostAsJsonAsync(
            Route,
            Request with { BotId = "BotId2" });

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [TestMethod]
    public async Task When_IdAndNameIsUnique_Should_Return_201()
    {
        // Act
        using var client = _factory.CreateClient();
        _ = await client.PostAsJsonAsync(
            Route,
            Request);

        var response = await client.PostAsJsonAsync(
            Route,
            Request with { BotId = "BotId3", Name = "Name2" });

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}