namespace WiseReminder.Application.Authors.GetAuthorDtoByUserId;

public sealed record GetAuthorDtoByUserIdQuery : IQuery<AuthorDto>
{
    public Guid UserId { get; init; }
}