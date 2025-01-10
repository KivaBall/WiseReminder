namespace WiseReminder.Application.Authors.Queries.GetAuthorByUserId;

public sealed record GetAuthorByUserIdQuery(Guid UserId) : IQuery<Author>;