namespace WiseReminder.Application.Abstractions.MediatR;

public interface IQuery<TEntity> : ICommon, IRequest<Result<TEntity>>;