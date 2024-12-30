namespace WiseReminder.WebAPI.Controllers.Categories;

public sealed record CategoryRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}