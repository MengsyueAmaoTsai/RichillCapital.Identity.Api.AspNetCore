using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Instruments;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    [HttpGet("/api/instruments")]
    [SwaggerOperation(OperationId = "Instruments.List", Tags = ["Instruments"])]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}