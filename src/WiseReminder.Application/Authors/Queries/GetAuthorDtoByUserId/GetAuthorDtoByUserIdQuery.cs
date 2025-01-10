namespace WiseReminder.Application.Authors.Queries.GetAuthorDtoByUserId;

public sealed record GetAuthorDtoByUserIdQuery(Guid UserId) : IQuery<AuthorDto>;