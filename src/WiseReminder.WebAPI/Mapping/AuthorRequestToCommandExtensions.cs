namespace WiseReminder.WebAPI.Mapping;

public static class AuthorRequestToCommandExtensions
{
    public static CreateAuthorCommand ToCreateAuthorCommand(this CreateAuthorRequest request)
    {
        return new CreateAuthorCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate,
            UserId = request.UserId
        };
    }

    public static UpdateAuthorCommand ToUpdateAuthorCommand(this UpdateAuthorRequest request)
    {
        return new UpdateAuthorCommand
        {
            Id = request.Id,
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
        };
    }
}