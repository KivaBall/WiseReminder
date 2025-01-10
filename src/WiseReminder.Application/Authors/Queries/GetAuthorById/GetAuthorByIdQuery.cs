namespace WiseReminder.Application.Authors.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(Guid Id) : IQuery<Author>;