namespace Yomikaze.WebAPI.Models;

public abstract class ResponseModel
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Error { get; set; }
}