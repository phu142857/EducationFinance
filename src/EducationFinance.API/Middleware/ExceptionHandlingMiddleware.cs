using EducationFinance.Application.Common.Exceptions;
using EducationFinance.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EducationFinance.API.Middleware;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException =>
                (StatusCodes.Status404NotFound, "Resource not found"),

            ValidationException =>
                (StatusCodes.Status400BadRequest, "Validation failed"),

            ArgumentException =>
                (StatusCodes.Status400BadRequest, "Invalid request"),

            UnauthorizedAccessException =>
                (StatusCodes.Status403Forbidden, "Forbidden"),

            DomainException =>
                (StatusCodes.Status409Conflict, "Business rule violation"),

            _ =>
                (StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred")
        };

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            logger.LogError(
                exception,
                "Unhandled exception. TraceId: {TraceId}",
                httpContext.TraceIdentifier);
        }
        else
        {
            logger.LogWarning(
                "Request failed with status {StatusCode}. " +
                "Path: {Path}, TraceId: {TraceId}",
                statusCode,
                httpContext.Request.Path,
                httpContext.TraceIdentifier);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] =
            httpContext.TraceIdentifier;

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] =
                validationException.Errors;
        }

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}
