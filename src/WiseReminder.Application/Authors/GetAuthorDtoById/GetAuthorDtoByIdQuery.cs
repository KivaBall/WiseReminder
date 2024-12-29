namespace WiseReminder.Application.Authors.GetAuthorDtoById;

public sealed record GetAuthorDtoByIdQuery : IQuery<AuthorDto>
{
    public required Guid Id { get; init; }
}