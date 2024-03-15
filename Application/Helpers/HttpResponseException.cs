using System.Net;

namespace Yomikaze.Application.Helpers;

public class HttpResponseException(int statusCode, object? value = null) : Exception
{
    public HttpResponseException(HttpStatusCode status, object? value = null) : this((int)status, value)
    {
    }

    public int StatusCode { get; } = statusCode;

    public object? Value { get; } = value;
}