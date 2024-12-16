namespace WiseReminder.Domain.Categories;

public sealed record CategoryDescription
{
    public CategoryDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }
}