namespace WiseReminder.WebAPI.Mappers;

public static class AuthorRequestMapper
{
    public static CreateAuthorByAdminCommand ToCreateAuthorByAdminCommand(
        this AuthorByAdminRequest request)
    {
        return new CreateAuthorByAdminCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }

    public static CreateAuthorByUserCommand ToCreateAuthorByUserCommand(
        this AuthorByUserRequest request,
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

    public static UpdateAuthorByAdminCommand ToUpdateAuthorByAdminCommand(
        this AuthorByAdminRequest request,
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

    public static UpdateAuthorByUserCommand ToUpdateAuthorByUserCommand(
        this AuthorByUserRequest request,
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