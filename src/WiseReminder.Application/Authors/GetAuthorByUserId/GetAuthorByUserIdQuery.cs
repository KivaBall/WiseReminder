namespace WiseReminder.Application.Authors.GetAuthorByUserId;

public sealed record GetAuthorByUserIdQuery(Guid Id) : IQuery<Author>;