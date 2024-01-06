using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

namespace RichillCapital.Identity.Api.Endpoints.Users;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}