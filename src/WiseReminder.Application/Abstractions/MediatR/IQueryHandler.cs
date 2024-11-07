using MediatR;
using WiseReminder.Domain.Abstractions;

namespace WiseReminder.Application.Abstractions.MediatR;

public interface IQueryHandler<in TQuery, TEntity> : IRequestHandler<TQuery, Result<TEntity>>
    where TQuery : IQuery<TEntity>;