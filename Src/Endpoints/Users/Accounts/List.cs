using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Users.Accounts;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    private readonly ISender _sender;

    public List(ISender sender) => _sender = sender;

    [HttpGet("/api/users/{userId}/accounts")]
    [SwaggerOperation(OperationId = "Users.Accounts.List", Tags = ["Users"])]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}