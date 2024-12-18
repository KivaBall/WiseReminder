namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed record GetAuthorByIdQuery : IQuery<Author>
{
    public Guid Id { get; init; }
}