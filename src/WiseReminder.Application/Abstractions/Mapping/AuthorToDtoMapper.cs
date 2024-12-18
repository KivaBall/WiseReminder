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
            DateOfBirth = new DateOnly(author.BirthDate.Year, author.BirthDate.Month,
                author.BirthDate.Day),
            DateOfDeath = author.DeathDate == null
                ? null
                : new DateOnly(author.DeathDate.Year, author.DeathDate.Month, author.DeathDate.Day)
        };
    }
}