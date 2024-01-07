using Microsoft.AspNetCore.Mvc.Testing;

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
}