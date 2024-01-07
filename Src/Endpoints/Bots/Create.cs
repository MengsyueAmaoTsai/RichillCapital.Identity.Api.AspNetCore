using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Bots.Create;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class Create : AsyncEndpoint
    .WithRequest<CreateBotRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public Create(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("/api/bots")]
    [SwaggerOperation(OperationId = "Bots.Create", Tags = ["Bots"])]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] CreateBotRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateBotCommand(
            request.BotId,
            request.Name,
            request.Description,
            request.TradingPlatform);

        var errorOr = await _sender.Send(command, cancellationToken);

        return errorOr
            .Match(
                HandleFailure,
                value => CreatedAtRoute(new { BotId = value.Value }, new { BotId = value.Value }));
    }
}

public sealed record class CreateBotRequest
{
    public string BotId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string TradingPlatform { get; init; } = string.Empty;
}