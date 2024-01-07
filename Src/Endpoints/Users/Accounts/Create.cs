using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Users.CreateSimulatedAccount;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Users.Accounts;

public sealed class Create : AsyncEndpoint
    .WithRequest<CreateSimulatedAccountRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public Create(ISender sender) => _sender = sender;

    [HttpPost("/api/users/{userId}/accounts")]
    [SwaggerOperation(OperationId = "Users.Accounts.Create", Tags = ["Users"])]
    public override async Task<ActionResult> HandleAsync(
        CreateSimulatedAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateSimulatedAccountCommand(
            request.UserId,
            request.Body.Name,
            request.Body.Currency,
            request.Body.InitialBalance);

        return (await _sender.Send(command, cancellationToken))
            .Match(Ok, HandleFailure);
    }
}

public class CreateSimulatedAccountRequest
{
    [FromRoute(Name = "userId")]
    public string UserId { get; init; } = string.Empty;

    [FromBody]
    public CreateSimulatedAccountRequestBody Body { get; init; } = new();
}

public class CreateSimulatedAccountRequestBody
{
    public string Name { get; init; } = string.Empty;

    public string Currency { get; init; } = string.Empty;

    public int InitialBalance { get; init; }
}