using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Models;

public class ResponseModel<TData, TError>
{
    public virtual bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public virtual TData? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TError Errors { get; set; } = default!;
}

public class ResponseModel<T> : ResponseModel<T, IDictionary<string, string[]>>
{
}

public class ResponseModel : ResponseModel<object, IDictionary<string, string[]>>
{
    public static ResponseModel CreateSuccess()
    {
        return new ResponseModel { Success = true };
    }

    public static ResponseModel CreateSuccess(string message)
    {
        return new ResponseModel { Success = true, Message = message };
    }

    public static ResponseModel<T> CreateSuccess<T>(T data)
    {
        return new ResponseModel<T> { Success = true, Data = data };
    }

    public static ResponseModel<T> CreateSuccess<T>(string message, T data)
    {
        return new ResponseModel<T> { Success = true, Message = message, Data = data };
    }

    public static ResponseModel CreateError()
    {
        return new ResponseModel { Success = false };
    }

    public static ResponseModel CreateError(string message)
    {
        return new ResponseModel { Success = false, Message = message };
    }

    public static ResponseModel<object, E> CreateError<E>(string message, E errors)
    {
        return new ResponseModel<object, E> { Success = false, Message = message, Errors = errors };
    }
}