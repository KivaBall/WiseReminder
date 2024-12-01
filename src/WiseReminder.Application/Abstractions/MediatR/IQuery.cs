namespace WiseReminder.Application.Abstractions.MediatR;

public interface IQuery<TEntity> : IRequest<Result<TEntity>>;