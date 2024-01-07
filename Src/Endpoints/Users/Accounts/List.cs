using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

namespace RichillCapital.Identity.Api.Endpoints.Users.Accounts;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    [HttpGet("/api/users/{userId}/accounts")]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}