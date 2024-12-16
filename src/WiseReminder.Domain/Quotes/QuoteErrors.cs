namespace WiseReminder.Domain.Quotes;

public static class QuoteErrors
{
    public static IError QuoteNotFound =>
        new Error("Quote by Id was not found");
}