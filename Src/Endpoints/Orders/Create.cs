using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Orders.Create;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Orders;

public sealed class Create : AsyncEndpoint
    .WithRequest<CreateOrderRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public Create(ISender sender) => _sender = sender;

    [HttpPost("/api/orders")]
    [SwaggerOperation(OperationId = "Orders.Create", Tags = ["Orders"])]
    public override async Task<ActionResult> HandleAsync(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateOrderCommand(
            request.AccountId,
            request.Symbol,
            request.TradeType,
            request.OrderType,
            request.Quantity,
            request.TimeInForce,
            request.Price);

        return (await _sender.Send(command, cancellationToken))
            .Match(Ok, HandleFailure);
    }
}

public class CreateOrderRequest
{
    public string AccountId { get; init; } = string.Empty;

    public string Symbol { get; init; } = string.Empty;

    public string TradeType { get; init; } = string.Empty;

    public string OrderType { get; init; } = string.Empty;

    public decimal Quantity { get; init; }

    public string TimeInForce { get; init; } = string.Empty;

    public decimal Price { get; init; }
}