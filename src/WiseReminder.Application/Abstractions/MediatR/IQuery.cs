using MediatR;
using WiseReminder.Domain.Abstractions;

namespace WiseReminder.Application.Abstractions.MediatR;

public interface IQuery<TEntity> : IRequest<Result<TEntity>>;