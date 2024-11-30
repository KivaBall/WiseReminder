namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed record UpdateAuthorRequest : BaseAuthorRequest
{
    public Guid? Id { get; init; }
}