using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Signals.Emit;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Signals;

public sealed class Create : AsyncEndpoint
    .WithRequest<EmitSignalRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public Create(ISender sender) => _sender = sender;

    [HttpPost("/api/signals")]
    [SwaggerOperation(OperationId = "Signals.Create", Tags = ["Signals"])]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] EmitSignalRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new EmitSignalCommand(
            request.Time,
            request.Symbol,
            request.TradeType,
            request.Quantity,
            request.Price);

        return (await _sender.Send(command, cancellationToken))
            .Match(Ok, HandleFailure);
    }
}

public class EmitSignalRequest
{
    public DateTimeOffset Time { get; init; }

    public string Symbol { get; init; } = string.Empty;

    public string TradeType { get; init; } = string.Empty;

    public decimal Quantity { get; init; }

    public decimal Price { get; init; }
}