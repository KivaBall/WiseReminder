namespace WiseReminder.WebAPI.Controllers.Authors;

//TODO: Replace with inherited records
public sealed record CreateAuthorRequest(string Name, string Biography, DateOnly DateOfBirth, DateOnly DateOfDeath);