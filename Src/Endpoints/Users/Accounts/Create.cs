using Microsoft.AspNetCore.Mvc;

using RichillCapital.Presentation.Abstractions.AspNetCore;

namespace RichillCapital.Identity.Api.Endpoints.Users.Accounts;

public sealed class Create : AsyncEndpoint
    .WithRequest<CreateSimulatedAccountRequest>
    .WithActionResult
{
    [HttpPost("/api/users/{userId}/accounts")]
    public override Task<ActionResult> HandleAsync(CreateSimulatedAccountRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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