namespace WiseReminder.Domain.Categories;

public sealed class CategoryDetails
{
    public required Category Category { get; init; }
    public required int Quotes { get; init; }
}