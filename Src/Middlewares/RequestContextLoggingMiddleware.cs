using Microsoft.Extensions.Primitives;

using Serilog.Context;

namespace RichillCapital.Identity.Api.Middlewares;

public sealed class RequestContextLoggingMiddleware : IMiddleware
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.Headers
            .TryGetValue(CorrelationIdHeaderName, out StringValues correlationId);

        using (LogContext.PushProperty(
            "CorrelationId",
            correlationId.FirstOrDefault() ?? context.TraceIdentifier))
        {
            return next.Invoke(context);
        }
    }
}