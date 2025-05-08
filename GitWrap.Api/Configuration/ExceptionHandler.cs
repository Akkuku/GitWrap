using System.Net;
using GitWrap.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GitWrap.Api.Configuration;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is HttpResponseException httpResponseException)
        {
            await WriteProblemDetails(httpContext, cancellationToken, httpResponseException.Message,
                httpResponseException.StatusCode);
        }
        else
        {
            await WriteProblemDetails(httpContext, cancellationToken);
        }

        return true;
    }

    private static async Task WriteProblemDetails(HttpContext httpContext, CancellationToken cancellationToken, string title = "Unexpected error occured", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        var problemDetails = new ProblemDetails
        {
            Title = title,
            Status = (int)statusCode
        };
        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}