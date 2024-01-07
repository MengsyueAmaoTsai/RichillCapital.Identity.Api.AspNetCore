using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Users.Accounts;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    [HttpGet("/api/users/{userId}/accounts")]
    [SwaggerOperation(OperationId = "Users.Accounts.List", Tags = ["Users"])]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}