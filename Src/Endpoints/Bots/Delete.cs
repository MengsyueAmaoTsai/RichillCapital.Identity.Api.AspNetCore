using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Bots.Delete;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Bots;

public sealed class Delete : AsyncEndpoint
    .WithRequest<DeleteBotRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public Delete(ISender sender) => _sender = sender;

    [HttpDelete("/api/bots/{botId}")]
    [SwaggerOperation(OperationId = "Bots.Delete", Tags = ["Bots"])]
    public override async Task<ActionResult> HandleAsync(
        DeleteBotRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new DeleteBotCommand(request.BotId), cancellationToken))
            .Match(NoContent, HandleFailure);
}

public sealed record class DeleteBotRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}
