namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class SeedHelper
{
    public static async Task<IdsFixture> SeedDataAsync(this HttpClient client)
    {
        await client.AdminLoginAsync();

        var categoryRequest = CategoryData.CreateCategoryRequest;

        var categoryId =
            await (await client.PostAsJsonAsync("api/categories", categoryRequest))
                .Content.ReadFromJsonAsync<Guid>();

        var authorRequest = AuthorData.CreateAdminAuthorRequest;

        var authorId =
            await (await client.PostAsJsonAsync("api/authors", authorRequest)).Content
                .ReadFromJsonAsync<Guid>();

        var quoteRequest = QuoteData.CreateAdminQuoteRequest(authorId, categoryId);

        var quoteId =
            await (await client.PostAsJsonAsync("api/quotes", quoteRequest)).Content
                .ReadFromJsonAsync<Guid>();

        var userRequest = UserData.CreateUserRequest;

        var userId =
            await (await client.PostAsJsonAsync("api/users/register", userRequest)).Content
                .ReadFromJsonAsync<Guid>();

        var initialIds = new IdsFixture
        {
            CategoryId = categoryId,
            AuthorId = authorId,
            QuoteId = quoteId,
            UserId = userId
        };

        client.Logout();

        return initialIds;
    }
}