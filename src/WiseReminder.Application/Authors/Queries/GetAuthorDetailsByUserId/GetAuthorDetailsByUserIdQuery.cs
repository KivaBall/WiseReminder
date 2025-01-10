namespace WiseReminder.Application.Authors.Queries.GetAuthorDetailsByUserId;

public sealed record GetAuthorDetailsByUserIdQuery(Guid UserId) : IQuery<AuthorDetails>;