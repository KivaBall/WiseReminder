namespace WiseReminder.WebAPI.Controllers.Authors;

public record BaseAuthorRequest
{
    public string? Name { get; init; }
    public string? Biography { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public DateOnly? DateOfDeath { get; init; }
}