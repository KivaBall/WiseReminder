namespace WiseReminder.Domain.Abstractions;

public class Result(bool isSuccess, Error error)
{
    public bool IsSuccess { get; } = isSuccess;
    public Error Error { get; } = error;

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static Result<T> Success<T>(T entity)
    {
        return new Result<T>(true, Error.None, entity);
    }

    public static Result<T> Failure<T>(T? entity, Error error)
    {
        return new Result<T>(false, error, entity);
    }
}

public sealed class Result<TEntity>(bool isSuccess, Error error, TEntity? entity) : Result(isSuccess, error)
{
    public TEntity? Entity { get; } = entity;
}