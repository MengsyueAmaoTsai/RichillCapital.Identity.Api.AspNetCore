using Microsoft.AspNetCore.Mvc.Testing;

namespace RichillCapital.Identity.Api.EndToEndTests;

public sealed class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
}