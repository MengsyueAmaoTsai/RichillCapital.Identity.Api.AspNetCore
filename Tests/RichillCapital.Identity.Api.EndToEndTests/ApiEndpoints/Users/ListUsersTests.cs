using System.Net;

using FluentAssertions;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace RichillCapital.Identity.Api.EndToEndTests.ApiEndpoints.Users;

[TestClass]
public sealed class ListUsersTests
{
    private static CustomWebApplicationFactory<Program>? _factory;
    private static HttpClient? _httpClient;

    [ClassInitialize]
    public static void ClassInitialize()
    {
        _factory = new();
        _httpClient = _factory.CreateClient();
    }

    [TestMethod]
    public async Task When_Should()
    {
        if (_httpClient is not null)
        {
            var response = await _httpClient.GetAsync(new Uri("/api/users"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}