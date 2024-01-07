using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    private readonly ISender _sender;

    public List(ISender sender)
    {
        _sender = sender;
    }

    [HttpDelete("/api/bots")]
    [SwaggerOperation(OperationId = "Bots.List", Tags = ["Bots"])]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
