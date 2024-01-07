using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Instruments;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    private readonly ISender _sender;

    public List(ISender sender) => _sender = sender;

    [HttpGet("/api/instruments")]
    [SwaggerOperation(OperationId = "Instruments.List", Tags = ["Instruments"])]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}