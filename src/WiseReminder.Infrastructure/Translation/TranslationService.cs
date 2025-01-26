namespace WiseReminder.Infrastructure.Translation;

public sealed class TranslationService(
    IConfiguration config,
    HttpClient client)
    : ITranslationService
{
    private const string SinglePrompt =
        """
        You're WebAPI. A user has sent you the text of a quote that needs to be translated into a given target language. So you just write the translation and nothing else. If for some reason you can't translate the text, then just don't write anything. Don't use indents or newlines. You write everything in one line. \n is forbidden.
        Target language: {0}
        Quote: {1}
        """;

    private const string MultiplePrompt =
        """
        You are a WebAPI. You've been sent the text of several quotes by a user that need to be translated into a given target language. The quotes are separated by “+++” characters. That is, just write a translation between these quotes, and be sure not to put after and before these pluses any indents, spaces and so on. So it should come out like this: “FIRST QUOTE FIRST QUOTE. +++ SECOND QUOTE SECOND QUOTE SECOND QUOTE.”. If for some reason you can not translate the text - then just do not write anything. Don't use indents or newlines. Write everything in one line. \n is forbidden
        Target language: {0}
        Quotes combined by "+++": {1}
        """;

    private readonly string _url =
        "https://generativelanguage.googleapis.com/v1beta/models/" +
        $"gemini-1.5-flash:generateContent?key={
            config["GeminiApiKey"] ?? throw new InvalidOperationException("GeminiApiKey is not configured")}";

    public async Task<string?> TranslateAsync(string text, string targetLanguage,
        CancellationToken cancellationToken)
    {
        var prompt = string.Format(SinglePrompt, targetLanguage, text);

        var i = 0;

        while (i++ < 5)
        {
            var translatedText = await SendRequestAsync(prompt, cancellationToken);

            if (!string.IsNullOrEmpty(translatedText))
            {
                return translatedText;
            }
        }

        return null;
    }

    public async Task<IList<string>?> TranslateAsync(ICollection<string> texts,
        string targetLanguage,
        CancellationToken cancellationToken)
    {
        var joinedTexts = string.Join("+++", texts);

        var prompt = string.Format(MultiplePrompt, targetLanguage, joinedTexts);

        var i = 0;

        while (i++ < 5)
        {
            var translatedTexts = await SendRequestAsync(prompt, cancellationToken);

            if (string.IsNullOrEmpty(translatedTexts))
            {
                continue;
            }

            var list = translatedTexts.Split("+++").ToList();

            if (list.Count == texts.Count)
            {
                return list;
            }
        }

        return null;
    }

    private async Task<string?> SendRequestAsync(string prompt, CancellationToken cancellationToken)
    {
        var payload = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var response = await client.PostAsJsonAsync(_url, payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        using var document = JsonDocument.Parse(json);

        var str = document.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return str?.Replace("\n", string.Empty);
    }
}