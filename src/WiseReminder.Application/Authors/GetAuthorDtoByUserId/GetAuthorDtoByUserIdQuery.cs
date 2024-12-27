namespace WiseReminder.Application.Authors.GetAuthorByUserId;

public sealed record GetAuthorDtoByUserIdQuery : IQuery<AuthorDto>
{
    public Guid UserId { get; init; }
}