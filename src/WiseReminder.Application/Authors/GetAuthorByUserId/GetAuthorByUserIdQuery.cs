namespace WiseReminder.Application.Authors.GetAuthorByUserId;

public sealed record GetAuthorByUserIdQuery : IQuery<Author>
{
    public required Guid Id { get; init; }
}