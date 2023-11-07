namespace Yomikaze.WebAPI.Services;

public class ApiServiceException : Exception
{
    public ApiServiceException(string message) : base(message)
    {
    }

    public ApiServiceException(IEnumerable<string> errors) : base(string.Join("\n", errors))
    {
    }
}
