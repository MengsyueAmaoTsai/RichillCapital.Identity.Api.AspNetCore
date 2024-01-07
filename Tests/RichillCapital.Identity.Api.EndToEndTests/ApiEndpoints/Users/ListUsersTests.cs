using System.Net;
using System.Text.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using RichillCapital.Identity.Api.Endpoints.Users;

namespace RichillCapital.Identity.Api.EndToEndTests.ApiEndpoints.Users;

[TestClass]
public sealed class CreateSimulatedAccountTests
{
    private const string Route = "/api/users";

    private static TestContext _testContext;
    private static WebApplicationFactory<Program> _factory;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        _testContext = testContext;
        _factory = new CustomWebApplicationFactory<Program>();
    }

    [TestMethod]
    public async Task When_RequestIsValid_Should_ReturnSuccess()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync(Route);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IEnumerable<UserResponse>>(content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().HaveCount(2);
    }
}