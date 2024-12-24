namespace WiseReminder.Application.Abstractions.MediatR;

public interface ICommand : ICommandQuery, IRequest<Result>;

public interface ICommand<TEntity> : ICommandQuery, IRequest<Result<TEntity>>;