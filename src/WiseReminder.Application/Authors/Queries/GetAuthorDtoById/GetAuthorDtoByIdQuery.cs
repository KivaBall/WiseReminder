namespace WiseReminder.Application.Authors.Queries.GetAuthorDtoById;

public sealed record GetAuthorDtoByIdQuery(Guid Id) : IQuery<AuthorDto>;