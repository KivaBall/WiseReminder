namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed record CreateAuthorRequest(string Name, string Biography, DateOnly DateOfBirth, DateOnly DateOfDeath);