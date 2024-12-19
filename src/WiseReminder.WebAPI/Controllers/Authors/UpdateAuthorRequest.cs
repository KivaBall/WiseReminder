namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed record UpdateAuthorRequest : BaseAuthorRequest
{
    public required Guid Id { get; init; }
}