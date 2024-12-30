namespace WiseReminder.WebAPI.Mapping;

public static class AuthorRequestsToCommandExtensions
{
    public static AdminCreateAuthorCommand ToAdminCreateAuthorCommand(
        this AdminAuthorRequest request)
    {
        return new AdminCreateAuthorCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static UserCreateAuthorCommand ToUserCreateAuthorCommand(
        this UserAuthorRequest request,
        Guid userId)
    {
        return new UserCreateAuthorCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            UserId = userId
        };
    }

    public static AdminUpdateAuthorCommand ToAdminUpdateAuthorCommand(
        this AdminAuthorRequest request,
        Guid id)
    {
        return new AdminUpdateAuthorCommand
        {
            Id = id,
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static UserUpdateAuthorCommand ToUserUpdateAuthorCommand(
        this UserAuthorRequest request,
        Guid userId)
    {
        return new UserUpdateAuthorCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            UserId = userId
        };
    }
}