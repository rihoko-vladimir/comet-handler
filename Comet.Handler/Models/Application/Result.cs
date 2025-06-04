namespace CometHandler.Models.Application;

public class Result<T>
{
    private Result(T value, bool isSuccess, string? error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public T Value { get; }
    public string? Error { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, null);
    }

    public static Result<T?> Failure(string? error)
    {
        return new Result<T?>(default, false, error);
    }
}