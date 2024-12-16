namespace WiseReminder.Domain.Quotes;

public sealed record QuoteText
{
    public QuoteText(string value)
    {
        Value = value;
    }

    public string Value { get; }
}