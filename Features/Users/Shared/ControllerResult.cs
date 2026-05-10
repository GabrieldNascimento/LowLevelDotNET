using LowLevelDotNET.Features.Users.Shared.Enums;

namespace LowLevelDotNET.Features.Users.Shared
{
    public class ControllerResult<T>
{
     public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public ResultType Type { get; set; }

    public static ControllerResult<T> Ok(T data, string message = "")
    {
        return new ControllerResult<T>
        {
            Success = true,
            Data = data,
            Message = message,
            Type = ResultType.Success
        };
    }

    public static ControllerResult<T> BadRequest(string message)
    {
        return new ControllerResult<T>
        {
            Success = false,
            Message = message,
            Type = ResultType.BadRequest
        };
    }

    public static ControllerResult<T> NotFound(string message)
    {
        return new ControllerResult<T>
        {
            Success = false,
            Message = message,
            Type = ResultType.NotFound
        };
    }

    public static ControllerResult<T> InternalError(string message)
    {
        return new ControllerResult<T>
        {
            Success = false,
            Message = message,
            Type = ResultType.InternalError
        };
    }
}
}