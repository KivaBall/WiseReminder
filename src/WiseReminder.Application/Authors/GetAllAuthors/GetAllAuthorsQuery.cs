namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed record GetAllAuthorsQuery : IQuery<ICollection<AuthorDto>>;