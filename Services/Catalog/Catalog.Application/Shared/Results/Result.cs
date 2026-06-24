namespace Catalog.Application.Shared.Results;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public T? Data { get; set; }
    public ResultType Type { get; set; }

    public static Result<T> Success(T data)=> new()
    {
        IsSuccess = true,
        Data = data,
        Type = ResultType.Success
    };

    public static Result<T> Failure(string? error , ResultType type) => new()
    {
        IsSuccess = false,
        Error = error,
        Type = type,
    };
    
    


}