using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace RichillCapital.Identity.Api.EndToEndTests.ApiEndpoints.Users;

[TestClass]
public sealed class CreateSimulatedAccountTests
{
    private static TestContext _testContext;
    private static WebApplicationFactory<Program> _factory;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        _testContext = testContext;
        _factory = new CustomWebApplicationFactory<Program>();
    }

    [TestMethod]
    public async Task Should()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync(new Uri("/api/users"));

        response.Should().NotBeNull();
    }
}