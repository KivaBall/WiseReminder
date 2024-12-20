﻿namespace WiseReminder.Application.Abstractions.MediatR;

public interface ICommand : IRequest<Result>;

public interface ICommand<TEntity> : IRequest<Result<TEntity>>;