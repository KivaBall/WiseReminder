namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed record GetAuthorByIdQuery : IQuery<Author>
{
    public required Guid Id { get; init; }
}