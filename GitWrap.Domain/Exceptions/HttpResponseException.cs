using System.Net;

namespace GitWrap.Domain.Exceptions;

public class HttpResponseException(string message, Exception? innerException = null) : Exception(message, innerException)
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

    public HttpResponseException(string message, HttpStatusCode statusCode, Exception? innerException = null) : this(message, innerException)
    {
        StatusCode = statusCode;
    }
}