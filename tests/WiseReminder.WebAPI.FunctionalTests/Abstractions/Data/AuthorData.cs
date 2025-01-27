namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class AuthorData
{
    public const string Name = "Name";
    public const string NewName = "NewName";

    public const string Biography = "Biography";
    public const string NewBiography = "NewBiography";

    public static readonly DateOnly BirthDate = new DateOnly(1980, 1, 1);
    public static readonly DateOnly NewBirthDate = new DateOnly(1981, 1, 1);

    public static readonly DateOnly DeathDate = new DateOnly(2010, 1, 1);
    public static readonly DateOnly NewDeathDate = new DateOnly(2011, 1, 1);

    public static AuthorByAdminRequest CreateAuthorByAdminRequest()
    {
        return ToAuthorByAdminRequest(Name, Biography, BirthDate, DeathDate);
    }

    public static AuthorByAdminRequest UpdateAuthorByAdminRequest()
    {
        return ToAuthorByAdminRequest(NewName, NewBiography, NewBirthDate, NewDeathDate);
    }

    public static AuthorByAdminRequest InvalidAuthorByAdminRequest()
    {
        return ToAuthorByAdminRequest(null!, null!, default, null!);
    }

    private static AuthorByAdminRequest ToAuthorByAdminRequest(string name, string biography,
        DateOnly birthDate, DateOnly? deathDate)
    {
        return new AuthorByAdminRequest
        {
            Name = name,
            Biography = biography,
            BirthDate = birthDate,
            DeathDate = deathDate
        };
    }

    public static AuthorByUserRequest CreateAuthorByUserRequest()
    {
        return ToAuthorByUserRequest(Name, Biography, BirthDate);
    }

    public static AuthorByUserRequest UpdateAuthorByUserRequest()
    {
        return ToAuthorByUserRequest(NewName, NewBiography, NewBirthDate);
    }

    public static AuthorByUserRequest InvalidAuthorByUserRequest()
    {
        return ToAuthorByUserRequest(null!, null!, default);
    }

    private static AuthorByUserRequest ToAuthorByUserRequest(string name, string biography,
        DateOnly birthDate)
    {
        return new AuthorByUserRequest
        {
            Name = name,
            Biography = biography,
            BirthDate = birthDate
        };
    }
}