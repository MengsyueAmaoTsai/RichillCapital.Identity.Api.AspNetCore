using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Bots.List;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult
{
    private readonly ISender _sender;

    public List(ISender sender) => _sender = sender;

    [HttpGet("/api/bots")]
    [SwaggerOperation(OperationId = "Bots.List", Tags = ["Bots"])]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default) =>
        (await _sender.Send(new ListBotsQuery(), cancellationToken))
            .Match(Ok, HandleFailure);
}
