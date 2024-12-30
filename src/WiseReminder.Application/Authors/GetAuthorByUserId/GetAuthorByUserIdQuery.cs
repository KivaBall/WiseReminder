namespace WiseReminder.Application.Authors.GetAuthorByUserId;

public sealed record GetAuthorByUserIdQuery(Guid UserId) : IQuery<Author>;