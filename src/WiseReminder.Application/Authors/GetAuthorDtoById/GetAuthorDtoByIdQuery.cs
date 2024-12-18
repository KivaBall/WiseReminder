namespace WiseReminder.Application.Authors.GetAuthorDtoById;

public sealed record GetAuthorDtoByIdQuery : IQuery<AuthorDto>
{
    public Guid Id { get; init; }
}