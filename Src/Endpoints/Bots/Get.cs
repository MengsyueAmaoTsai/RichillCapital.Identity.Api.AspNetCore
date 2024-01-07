using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Bots.GetById;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class Get : AsyncEndpoint
    .WithRequest<GetBotRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public Get(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/api/bots/{botId}", Name = "GetBotById")]
    [SwaggerOperation(OperationId = "Bots.Get", Tags = ["Bots"])]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] GetBotRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new GetBotByIdQuery(request.BotId), cancellationToken))
            .Match(Ok, HandleFailure);
}

public sealed record class GetBotRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; }
}