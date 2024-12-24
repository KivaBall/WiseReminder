namespace WiseReminder.IntegrationTests.Abstractions.Data;

public static class AuthorData
{
    public const string DefaultName = "DefaultName";
    public const string DefaultBiography = "DefaultBiography";
    public const string UpdatedName = "UpdatedName";
    public const string UpdatedBiography = "UpdatedBiography";

    public static readonly DateOnly DefaultBirthDate = new(1980, 1, 1);
    public static readonly DateOnly DefaultDeathDate = new(2010, 01, 01);
    public static readonly DateOnly UpdatedBirthDate = new(1981, 1, 1);
    public static readonly DateOnly UpdatedDeathDate = new(2011, 01, 01);

    public static BaseAuthorRequest BaseAuthorRequest()
    {
        return new BaseAuthorRequest
        {
            Name = DefaultName,
            Biography = DefaultBiography,
            BirthDate = DefaultBirthDate,
            DeathDate = DefaultDeathDate
        };
    }

    public static BaseAuthorRequest NotValidBaseAuthorRequest()
    {
        return new BaseAuthorRequest
        {
            Name = null!,
            Biography = null!,
            BirthDate = default,
            DeathDate = null
        };
    }

    public static UpdateAuthorRequest UpdateAuthorRequest(Guid id)
    {
        return new UpdateAuthorRequest
        {
            Id = id,
            Name = UpdatedName,
            Biography = UpdatedBiography,
            BirthDate = UpdatedBirthDate,
            DeathDate = UpdatedDeathDate
        };
    }

    public static UpdateAuthorRequest NotValidUpdateAuthorRequest(Guid id)
    {
        return new UpdateAuthorRequest
        {
            Id = id,
            Name = null!,
            Biography = null!,
            BirthDate = default,
            DeathDate = null
        };
    }
}