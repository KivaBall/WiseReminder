namespace WiseReminder.WebAPI.Controllers.Authors;

public record BaseAuthorRequest
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
    public required DateOnly? DeathDate { get; init; }
}