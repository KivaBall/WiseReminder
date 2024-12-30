namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed record UserAuthorRequest
{
    public required string Name { get; init; }
    public required string Biography { get; init; }
    public required DateOnly BirthDate { get; init; }
}