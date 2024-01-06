using Microsoft.AspNetCore.Mvc;

using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Identity.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult Match<TValue>(
        this Result<TValue> result,
        Func<TValue, ActionResult> onSuccess,
        Func<Error, ActionResult> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
    }

    public static ActionResult Match(
      this Result result,
      Func<ActionResult> onSuccess,
      Func<Error, ActionResult> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }
}