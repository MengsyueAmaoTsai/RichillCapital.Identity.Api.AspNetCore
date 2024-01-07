using MediatR;

using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("/api/bots/{botId}")]
    [SwaggerOperation(OperationId = "Bots.Get", Tags = ["Bots"])]
    public override Task<ActionResult> HandleAsync(GetBotRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public sealed record class GetBotRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; }
}