namespace WiseReminder.Application.Abstractions.MediatR;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TEntity> : IRequestHandler<TCommand, Result<TEntity>>
    where TCommand : ICommand<TEntity>;