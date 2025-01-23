namespace WiseReminder.Domain.UnitTests.Data;

public static class AuthorData
{
    public static AuthorName AuthorName => new AuthorName("AuthorName");

    public static AuthorName NewAuthorName => new AuthorName("NewAuthorName");

    public static Biography Biography => new Biography("Biography");

    public static Biography NewBiography => new Biography("NewBiography");

    public static Date BirthDate => Date.Create(new DateOnly(1950, 01, 01)).Value;

    public static Date NewBirthDate => Date.Create(new DateOnly(1965, 01, 01)).Value;

    public static Date DeathDate => Date.Create(new DateOnly(2000, 01, 01)).Value;

    public static Date NewDeathDate => Date.Create(new DateOnly(1995, 01, 01)).Value;

    public static Guid UserId => Guid.Empty;

    public static Author AdminAuthor =>
        Author.CreateByAdmin(AuthorName, Biography, BirthDate, DeathDate).Value;

    public static Author UserAuthor =>
        Author.CreateByUser(AuthorName, Biography, BirthDate, UserId);
}