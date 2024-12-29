namespace WiseReminder.Application.Abstractions.Mapping;

public static class AuthorToDtoMapper
{
    public static AuthorDto ToAuthorDto(this Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name.Value,
            Biography = author.Biography.Value,
            BirthDate = author.BirthDate.Value,
            DeathDate = author.DeathDate?.Value,
            UserId = author.UserId
        };
    }
}