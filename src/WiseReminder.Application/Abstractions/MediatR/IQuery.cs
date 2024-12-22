namespace WiseReminder.Application.Abstractions.MediatR;

public interface IQuery<TEntity> : ICommandQuery, IRequest<Result<TEntity>>;