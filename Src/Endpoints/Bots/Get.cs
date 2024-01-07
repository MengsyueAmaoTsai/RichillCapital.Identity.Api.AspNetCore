using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class Get : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    private readonly ISender _sender;

    public Get(ISender sender)
    {
        _sender = sender;
    }

    [HttpDelete("/api/bots/{botId}")]
    [SwaggerOperation(OperationId = "Bots.Get", Tags = ["Bots"])]
    public override Task<ActionResult> HandleAsync(string request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
