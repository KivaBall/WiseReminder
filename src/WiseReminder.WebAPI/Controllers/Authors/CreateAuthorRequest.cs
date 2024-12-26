namespace WiseReminder.WebAPI.Controllers.Authors;

public record CreateAuthorRequest : BaseAuthorRequest
{
    public required Guid? UserId { get; init; }
}