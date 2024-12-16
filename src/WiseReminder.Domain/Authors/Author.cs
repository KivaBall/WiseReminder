namespace WiseReminder.Domain.Authors;

public sealed class Author : Entity<Author>
{
    private Author(AuthorName name, AuthorBiography biography, Date birthDate, Date? deathDate)
    {
        Name = name;
        Biography = biography;
        BirthDate = birthDate;
        DeathDate = deathDate;
    }

    // ReSharper disable once UnusedMember.Local
    private Author()
    {
    }

    public AuthorName Name { get; private set; }
    public AuthorBiography Biography { get; private set; }
    public Date BirthDate { get; private set; }
    public Date? DeathDate { get; private set; }

    public ICollection<Quote> Quotes { get; } = [];

    public static Result<Author> Create(AuthorName name, AuthorBiography biography, Date birthDate,
        Date? deathDate)
    {
        if (!IsValidDateDiapason(birthDate, deathDate))
        {
            return Result.Fail("Invalid date diapason between birth and death");
        }

        var author = new Author(name, biography, birthDate, deathDate);

        return Result.Ok(author);
    }

    public Result<Author> Update(AuthorName name, AuthorBiography biography, Date birthDate,
        Date? deathDate)
    {
        if (!IsValidDateDiapason(birthDate, deathDate))
        {
            return Result.Fail("Invalid date diapason between birth and death");
        }

        Name = name;
        Biography = biography;
        BirthDate = birthDate;
        DeathDate = deathDate;

        return Result.Ok(this);
    }

    private static bool IsValidDateDiapason(Date birthDate, Date? deathDate)
    {
        if (deathDate == null)
        {
            return true;
        }

        if (deathDate.Year - birthDate.Year >= 10)
        {
            return true;
        }

        return false;
    }
}