namespace WiseReminder.WebAPI.Mapping;

public static class AuthorRequestToCommandExtensions
{
    public static CreateAuthorAsAdminCommand ToCreateAuthorAsAdminCommand(
        this BaseAuthorAsAdminRequest request)
    {
        return new CreateAuthorAsAdminCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static CreateAuthorAsUserCommand ToCreateAuthorAsUserCommand(
        this BaseAuthorAsUserRequest request,
        Guid userId)
    {
        return new CreateAuthorAsUserCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            UserId = userId
        };
    }

    public static UpdateAuthorAsAdminCommand ToUpdateAuthorAsAdminCommand(
        this BaseAuthorAsAdminRequest request,
        Guid id)
    {
        return new UpdateAuthorAsAdminCommand
        {
            Id = id,
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static UpdateAuthorAsUserCommand ToUpdateAuthorAsUserCommand(
        this BaseAuthorAsUserRequest request,
        Guid userId)
    {
        return new UpdateAuthorAsUserCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            UserId = userId
        };
    }
}