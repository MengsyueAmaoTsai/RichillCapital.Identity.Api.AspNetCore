using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Bots.Create;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class Delete : AsyncEndpoint
    .WithRequest<string>
    .WithActionResult
{
    private readonly ISender _sender;

    public Delete(ISender sender)
    {
        _sender = sender;
    }

    [HttpDelete("/api/bots/{botId}")]
    [SwaggerOperation(OperationId = "Bots.Delete", Tags = ["Bots"])]
    public override Task<ActionResult> HandleAsync(string request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
