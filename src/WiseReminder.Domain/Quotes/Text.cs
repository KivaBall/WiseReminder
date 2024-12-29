namespace WiseReminder.Domain.Quotes;

public sealed record Text
{
    public Text(string value)
    {
        Value = value;
    }

    public string Value { get; }
}