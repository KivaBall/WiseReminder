namespace WiseReminder.Application.Abstractions.Mappers;

public static class AuthorMapper
{
    public static AuthorDto ToAuthorDto(this AuthorDetails authorDetails)
    {
        return new AuthorDto
        {
            Id = authorDetails.Author.Id,
            Name = authorDetails.Author.Name.Value,
            Biography = authorDetails.Author.Biography.Value,
            BirthDate = authorDetails.Author.BirthDate.Value,
            DeathDate = authorDetails.Author.DeathDate?.Value,
            Quotes = authorDetails.Quotes,
            MinQuoteDate = authorDetails.MinQuoteDate?.Value,
            MaxQuoteDate = authorDetails.MaxQuoteDate?.Value
        };
    }
}