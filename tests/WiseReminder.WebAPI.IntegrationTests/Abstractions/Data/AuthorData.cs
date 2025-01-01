namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class AuthorData
{
    public static string DefaultName = "DefaultName";
    public static string DefaultBiography = "DefaultBiography";
    public static readonly DateOnly DefaultBirthDate = new(1980, 1, 1);
    public static readonly DateOnly DefaultDeathDate = new(2010, 01, 01);

    public static string UpdatedName = "UpdatedName";
    public static string UpdatedBiography = "UpdatedBiography";
    public static readonly DateOnly UpdatedBirthDate = new(1981, 1, 1);
    public static readonly DateOnly UpdatedDeathDate = new(2011, 01, 01);

    public static AdminAuthorRequest CreateAdminAuthorRequest =>
        AdminAuthorRequest(DefaultName, DefaultBiography, DefaultBirthDate, DefaultDeathDate);

    public static AdminAuthorRequest UpdateAdminAuthorRequest =>
        AdminAuthorRequest(UpdatedName, UpdatedBiography, UpdatedBirthDate, UpdatedDeathDate);

    public static AdminAuthorRequest InvalidAdminAuthorRequest =>
        AdminAuthorRequest(null!, null!, default, null!);

    public static UserAuthorRequest CreateUserAuthorRequest =>
        UserAuthorRequest(DefaultName, DefaultBiography, DefaultBirthDate);

    public static UserAuthorRequest UpdateUserAuthorRequest =>
        UserAuthorRequest(UpdatedName, UpdatedBiography, UpdatedBirthDate);

    public static UserAuthorRequest InvalidUserAuthorRequest =>
        UserAuthorRequest(null!, null!, default);

    private static AdminAuthorRequest AdminAuthorRequest(string name, string biography,
        DateOnly birthDate, DateOnly? deathDate)
    {
        return new AdminAuthorRequest
        {
            Name = name,
            Biography = biography,
            BirthDate = birthDate,
            DeathDate = deathDate
        };
    }

    private static UserAuthorRequest UserAuthorRequest(string name, string biography,
        DateOnly birthDate)
    {
        return new UserAuthorRequest
        {
            Name = name,
            Biography = biography,
            BirthDate = birthDate
        };
    }
}