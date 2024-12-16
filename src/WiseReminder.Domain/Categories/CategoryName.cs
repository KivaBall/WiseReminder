namespace WiseReminder.Domain.Categories;

public sealed record CategoryName
{
    public CategoryName(string value)
    {
        Value = value;
    }

    public string Value { get; }
}