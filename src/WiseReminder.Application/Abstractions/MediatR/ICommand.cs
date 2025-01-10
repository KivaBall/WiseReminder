namespace WiseReminder.Application.Abstractions.MediatR;

public interface ICommand : ICommon, IRequest<Result>;

public interface ICommand<TEntity> : ICommon, IRequest<Result<TEntity>>;