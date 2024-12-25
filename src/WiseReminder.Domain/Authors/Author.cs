namespace WiseReminder.Domain.Authors;

public sealed class Author : Entity<Author>
{
    private Author(AuthorName name, AuthorBiography biography, Date birthDate, Date? deathDate,
        User? user)
    {
        Name = name;
        Biography = biography;
        BirthDate = birthDate;
        DeathDate = deathDate;
        UserId = user?.Id;
        User = user;
    }

    // ReSharper disable once UnusedMember.Local
    private Author()
    {
    }

    public AuthorName Name { get; private set; }
    public AuthorBiography Biography { get; private set; }
    public Date BirthDate { get; private set; }
    public Date? DeathDate { get; private set; }

    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    public ICollection<Quote> Quotes { get; } = [];

    public static Result<Author> Create(AuthorName name, AuthorBiography biography, Date birthDate,
        Date? deathDate, User? user)
    {
        if (!IsEitherUserOrDeathDate(deathDate, user))
        {
            return Result.Fail("Unnecessary death date when user is bound");
        }

        if (!IsValidDateDiapason(birthDate, deathDate))
        {
            return Result.Fail("Invalid date diapason between birth and death");
        }

        var author = new Author(name, biography, birthDate, deathDate, user);

        return Result.Ok(author);
    }

    private static bool IsEitherUserOrDeathDate(Date? deathDate, User? user)
    {
        if (user != null && deathDate != null)
        {
            return false;
        }

        return true;
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