namespace WiseReminder.Domain.Categories;

public sealed record Description
{
    public Description(string value)
    {
        Value = value;
    }

    public string Value { get; }
}