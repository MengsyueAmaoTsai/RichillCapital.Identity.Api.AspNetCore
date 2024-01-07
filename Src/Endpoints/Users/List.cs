using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Core.Features.Users.List;
using RichillCapital.Extensions.Primitives;
using RichillCapital.Identity.Api.Extensions;
using RichillCapital.Presentation.Abstractions.AspNetCore;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Identity.Api.Endpoints.Users;

public sealed class List : AsyncEndpoint
    .WithoutRequest
    .WithActionResult<IEnumerable<UserResponse>>
{
    private readonly ISender _sender;

    public List(ISender sender) => _sender = sender;

    [HttpGet("/api/users")]
    [SwaggerOperation(OperationId = "Users.List", Tags = ["Users"])]
    public override async Task<ActionResult<IEnumerable<UserResponse>>> HandleAsync(CancellationToken cancellationToken = default) =>
        (await _sender.Send(new ListUsersQuery(), cancellationToken))
            .Map(users => users
                .Select(user => new UserResponse(
                    user.Id,
                    user.Email,
                    user.Name)))
            .Match(Ok, HandleFailure);
}

public sealed record UserResponse(string Id, string Email, string Name);