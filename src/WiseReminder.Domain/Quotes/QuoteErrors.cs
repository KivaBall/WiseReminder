using WiseReminder.Domain.Abstractions;

namespace WiseReminder.Domain.Quotes;

public static class QuoteErrors
{
    public static Error QuoteNotFound => new("Quote error", "Quote by Id was not found");
}