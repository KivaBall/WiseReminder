namespace WiseReminder.Domain.Authors;

public sealed class Author : Entity<Author>
{
    private Author(AuthorName name, Biography biography, Date birthDate, Date? deathDate,
        Guid? userId)
    {
        Name = name;
        Biography = biography;
        BirthDate = birthDate;
        DeathDate = deathDate;
        UserId = userId;
    }

    public AuthorName Name { get; private set; }
    public Biography Biography { get; private set; }
    public Date BirthDate { get; private set; }
    public Date? DeathDate { get; private set; }
    public Guid? UserId { get; private set; }

    public static Result<Author> CreateByAdmin(AuthorName name, Biography biography, Date birthDate,
        Date? deathDate)
    {
        if (!IsValidBirthAndDeathDateRange(birthDate, deathDate))
        {
            return AuthorErrors.InvalidBirthAndDeathDateRange;
        }

        var author = new Author(name, biography, birthDate, deathDate, null);

        return Result.Ok(author);
    }

    public static Author CreateByUser(AuthorName name, Biography biography, Date birthDate,
        Guid userId)
    {
        var author = new Author(name, biography, birthDate, null, userId);

        return author;
    }

    public Result UpdateByAdmin(AuthorName name, Biography biography, Date birthDate,
        Date? deathDate, Date minQuoteDate, Date maxQuoteDate)
    {
        if (!IsValidBirthAndDeathDateRange(birthDate, deathDate))
        {
            return AuthorErrors.InvalidBirthAndDeathDateRange;
        }

        if (!IsValidBirthAndMinQuoteDateRange(birthDate, minQuoteDate))
        {
            return AuthorErrors.InvalidBirthAndMinQuoteDateRange;
        }

        if (!IsValidDeathAndMaxQuoteDateRange(deathDate, maxQuoteDate))
        {
            return AuthorErrors.InvalidDeathAndMaxQuoteDateRange;
        }

        Name = name;
        Biography = biography;
        BirthDate = birthDate;
        DeathDate = deathDate;

        return Result.Ok();
    }

    public Result UpdateByUser(AuthorName name, Biography biography, Date birthDate,
        Date minQuoteDate)
    {
        if (!IsValidBirthAndMinQuoteDateRange(birthDate, minQuoteDate))
        {
            return AuthorErrors.InvalidBirthAndMinQuoteDateRange;
        }

        Name = name;
        Biography = biography;
        BirthDate = birthDate;

        return Result.Ok();
    }

    private static bool IsValidBirthAndDeathDateRange(Date birthDate, Date? deathDate)
    {
        if (deathDate == null)
        {
            return true;
        }

        if (deathDate.Value.Year - birthDate.Value.Year >= 10)
        {
            return true;
        }

        return false;
    }

    private static bool IsValidBirthAndMinQuoteDateRange(Date birthDate, Date minQuoteDate)
    {
        if (minQuoteDate.Value.Year - birthDate.Value.Year >= 10)
        {
            return true;
        }

        return false;
    }

    private static bool IsValidDeathAndMaxQuoteDateRange(Date? deathDate, Date maxQuoteDate)
    {
        if (deathDate == null)
        {
            return true;
        }

        if (deathDate.Value.Year - maxQuoteDate.Value.Year >= 0)
        {
            return true;
        }

        return false;
    }
}