namespace WiseReminder.Application.Authors.GetAuthorDtoByUserId;

public sealed record GetAuthorDtoByUserIdQuery(Guid UserId) : IQuery<AuthorDto>;