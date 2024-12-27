namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed record GetAuthorDtosQuery : IQuery<ICollection<AuthorDto>>;