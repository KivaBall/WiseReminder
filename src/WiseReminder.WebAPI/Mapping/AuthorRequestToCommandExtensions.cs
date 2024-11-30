namespace WiseReminder.WebAPI.Mapping;

public static class AuthorRequestToCommandExtensions
{
    public static CreateAuthorCommand ToCreateAuthorCommand(this BaseAuthorRequest request)
    {
        if (request.Name == null
            || request.Biography == null
            || request.DateOfBirth == null
            || request.DateOfDeath == null)
        {
            throw new ArgumentNullException($"{nameof(BaseAuthorRequest)} has null property");
        }

        return new CreateAuthorCommand
        {
            Name = request.Name,
            Biography = request.Biography,
            DateOfBirth = (DateOnly)request.DateOfBirth,
            DateOfDeath = (DateOnly)request.DateOfDeath
        };
    }

    public static UpdateAuthorCommand ToUpdateAuthorCommand(this UpdateAuthorRequest request)
    {
        if (request.Id == null
            || request.Name == null
            || request.Biography == null
            || request.DateOfBirth == null
            || request.DateOfDeath == null)
        {
            throw new ArgumentNullException($"{nameof(UpdateAuthorRequest)} has null property");
        }

        return new UpdateAuthorCommand
        {
            Id = (Guid)request.Id,
            Name = request.Name,
            Biography = request.Biography,
            DateOfBirth = (DateOnly)request.DateOfBirth,
            DateOfDeath = (DateOnly)request.DateOfDeath
        };
    }
}