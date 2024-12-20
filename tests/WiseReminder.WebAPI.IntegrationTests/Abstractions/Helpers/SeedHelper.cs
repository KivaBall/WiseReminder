namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class SeedHelper
{
    public static async Task<InitialIdsFixture> SeedDataAsync(this HttpClient httpClient)
    {
        var baseCategoryRequest = CategoryData.BaseCategoryRequest();

        var categoryId =
            await (await httpClient.PostAsJsonAsync("api/categories", baseCategoryRequest))
                .Content.ReadFromJsonAsync<Guid>();

        var baseAuthorRequest = AuthorData.BaseAuthorRequest();

        var authorId =
            await (await httpClient.PostAsJsonAsync("api/authors", baseAuthorRequest)).Content
                .ReadFromJsonAsync<Guid>();

        var baseQuoteRequest = QuoteData.BaseQuoteRequest(authorId, categoryId);

        var quoteId =
            await (await httpClient.PostAsJsonAsync("api/quotes", baseQuoteRequest)).Content
                .ReadFromJsonAsync<Guid>();

        var initialIds = new InitialIdsFixture
        {
            CategoryId = categoryId,
            AuthorId = authorId,
            QuoteId = quoteId
        };

        return initialIds;
    }
}