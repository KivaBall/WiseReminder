namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed record UpdateAuthorRequest(
    Guid Id,
    string Name,
    string Biography,
    DateOnly DateOfBirth,
    DateOnly DateOfDeath);