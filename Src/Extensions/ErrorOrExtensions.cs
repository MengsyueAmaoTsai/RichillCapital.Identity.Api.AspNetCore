using Microsoft.AspNetCore.Mvc;

using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Identity.Api.Extensions;

public static class ErrorOrExtensions
{
    public static ActionResult Match<TValue>(
        this ErrorOr<TValue> errorOr,
        Func<Error, ActionResult> onError,
        Func<TValue, ActionResult> onNoError)
    {
        return errorOr.HasError ? onError(errorOr.Error) : onNoError(errorOr.Value);
    }
}