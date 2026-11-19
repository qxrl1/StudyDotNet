using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudyDotNet.Application.Exceptions;
using StudyDotNet.Domain.Exceptions;

namespace StudyDotNet.Api;

public sealed class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            ResourceNotFoundException =>
                (StatusCodes.Status404NotFound, "Resource not found"),
            ConflictException =>
                (StatusCodes.Status409Conflict, "Resource conflict"),
            InvalidRequestException or InvalidSubmissionException or DomainRuleViolationException =>
                (StatusCodes.Status400BadRequest, "Invalid request"),
            _ => (0, string.Empty)
        };

        if (statusCode == 0)
        {
            return false;
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";
        await JsonSerializer.SerializeAsync(
            httpContext.Response.Body,
            new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            },
            cancellationToken: cancellationToken);

        return true;
    }
}
