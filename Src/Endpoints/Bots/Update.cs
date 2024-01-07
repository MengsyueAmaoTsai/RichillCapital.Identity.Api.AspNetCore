using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class Update : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    private readonly ISender _sender;

    public Update(ISender sender)
    {
        _sender = sender;
    }

    [HttpPatch("/api/bots/{botId}")]
    [SwaggerOperation(OperationId = "Bots.Update", Tags = ["Bots"])]
    public override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
