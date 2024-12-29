namespace WiseReminder.Application.Authors.GetAuthorDtoById;

public sealed record GetAuthorDtoByIdQuery(Guid Id) : IQuery<AuthorDto>;