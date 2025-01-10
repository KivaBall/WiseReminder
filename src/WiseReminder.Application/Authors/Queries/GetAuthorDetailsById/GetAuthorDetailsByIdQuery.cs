namespace WiseReminder.Application.Authors.Queries.GetAuthorDetailsById;

public sealed record GetAuthorDetailsByIdQuery(Guid Id) : IQuery<AuthorDetails>;