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
            DateOfBirth = author.DateOfBirth.Value,
            DateOfDeath = author.DateOfDeath.Value
        };
    }
}