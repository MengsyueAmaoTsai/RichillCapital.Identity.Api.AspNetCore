using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace RichillCapital.Identity.Api.Middlewares;

public sealed class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) =>
        _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var details = GetExceptionDetails(exception);

            var problemDetails = new ProblemDetails
            {
                Status = details.Status,
                Type = details.Type,
                Title = details.Title,
                Detail = details.Detail,
            };

            if (details.Errors is not null)
            {
                problemDetails.Extensions["errors"] = details.Errors;
            }

            context.Response.StatusCode = details.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception) =>
         exception switch
         {
             ValidationException validationException => new ExceptionDetails(
                 StatusCodes.Status400BadRequest,
                 "ValidationError",
                 "Validation error",
                 "One or more validation errors has occurred",
                 validationException.Errors),

             _ => new ExceptionDetails(
                 StatusCodes.Status500InternalServerError,
                 "InternalServerError",
                 "Server error",
                 "An unexpected error occurred",
                 null)
         };
}

internal sealed record ExceptionDetails(
    int Status,
    string Type,
    string Title,
    string Detail,
    IEnumerable<object>? Errors);