namespace Yomikaze.WebAPI.Services;

public class ApiServiceException : Exception
{
    public ApiServiceException(string message) : base(message)
    {
    }
}
