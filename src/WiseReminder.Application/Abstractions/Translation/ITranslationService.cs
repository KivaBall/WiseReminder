namespace WiseReminder.Application.Abstractions.Translation;

public interface ITranslationService
{
    Task<string?> TranslateAsync(string text, string targetLanguage,
        CancellationToken cancellationToken);

    Task<IList<string>?> TranslateAsync(ICollection<string> text, string targetLanguage,
        CancellationToken cancellationToken);
}