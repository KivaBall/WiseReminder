namespace WiseReminder.WebAPI.Mapping;

public static class AuthorRequestsToCommandExtensions
{
    public static CreateAuthorByAdminCommand ToAdminCreateAuthorCommand(
        this AdminAuthorRequest request)
    {
        return new CreateAuthorByAdminCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static CreateAuthorByUserCommand ToUserCreateAuthorCommand(
        this UserAuthorRequest request,
        Guid userId)
    {
        return new CreateAuthorByUserCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            UserId = userId
        };
    }

    public static UpdateAuthorByAdminCommand ToAdminUpdateAuthorCommand(
        this AdminAuthorRequest request,
        Guid id)
    {
        return new UpdateAuthorByAdminCommand
        {
            Id = id,
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static UpdateAuthorByUserCommand ToUserUpdateAuthorCommand(
        this UserAuthorRequest request,
        Guid userId)
    {
        return new UpdateAuthorByUserCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            UserId = userId
        };
    }
}