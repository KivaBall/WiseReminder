namespace WiseReminder.Application.Abstractions.Translation;

public interface ITranslationService
{
    Task TranslateAsync(QuoteDto quote, string targetLanguage, CancellationToken cancellationToken);

    Task TranslateAsync(ICollection<QuoteDto> quotes, string targetLanguage,
        CancellationToken cancellationToken);
}