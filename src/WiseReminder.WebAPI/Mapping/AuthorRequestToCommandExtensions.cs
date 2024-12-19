namespace WiseReminder.WebAPI.Mapping;

public static class AuthorRequestToCommandExtensions
{
    public static CreateAuthorCommand ToCreateAuthorCommand(this BaseAuthorRequest request)
    {
        return new CreateAuthorCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            BirthDate = request.BirthDate,
            DeathDate = request.DeathDate
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