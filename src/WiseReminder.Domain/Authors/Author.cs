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

    // ReSharper disable once UnusedMember.Local
    private Author()
    {
    }

    public AuthorName Name { get; private set; }
    public Biography Biography { get; private set; }
    public Date BirthDate { get; private set; }
    public Date? DeathDate { get; private set; }

    public Guid? UserId { get; private set; }
    public User? User { get; }
    public ICollection<Quote> Quotes { get; } = [];

    public static Result<Author> Create(AuthorName name, Biography biography, Date birthDate,
        Date? deathDate, Guid? userId)
    {
        if (!IsEitherUserOrDeathDate(deathDate, userId))
        {
            return Result.Fail(AuthorErrors.EitherDeathDateOrUserRelation);
        }

        if (!IsValidDateDiapason(birthDate, deathDate))
        {
            return Result.Fail(AuthorErrors.InvalidDateDiapason);
        }

        var author = new Author(name, biography, birthDate, deathDate, userId);

        return Result.Ok(author);
    }

    public Result<Author> Update(AuthorName name, Biography biography, Date birthDate,
        Date? deathDate)
    {
        if (!IsValidDateDiapason(birthDate, deathDate))
        {
            return Result.Fail(AuthorErrors.InvalidDateDiapason);
        }

        Name = name;
        Biography = biography;
        BirthDate = birthDate;
        DeathDate = deathDate;

        return Result.Ok(this);
    }

    private static bool IsEitherUserOrDeathDate(Date? deathDate, Guid? userId)
    {
        if (userId != null && deathDate != null)
        {
            return false;
        }

        return true;
    }

    private static bool IsValidDateDiapason(Date birthDate, Date? deathDate)
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
}