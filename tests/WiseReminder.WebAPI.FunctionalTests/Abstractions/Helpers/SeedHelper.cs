namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class SeedHelper
{
    public static async Task<AdminIdsFixture> SeedAdminDataAsync(this HttpClient client)
    {
        await client.AdminLoginAsync();


        var categoryRequest = CategoryData.CreateCategoryRequest();

        var categoryResponse = await client.PostAsync("api/categories", categoryRequest);
        await client.PostAsync("api/categories", categoryRequest);
        await client.PostAsync("api/categories", categoryRequest);

        var categoryId = await categoryResponse.ReadJson<Guid>();


        var authorRequest = AuthorData.CreateAuthorByAdminRequest();

        var authorResponse = await client.PostAsync("api/authors", authorRequest);
        await client.PostAsync("api/authors", authorRequest);
        await client.PostAsync("api/authors", authorRequest);

        var authorId = await authorResponse.ReadJson<Guid>();


        var quoteRequest = QuoteData.CreateQuoteByAdminRequest(authorId, categoryId);

        var quoteResponse = await client.PostAsync("api/quotes", quoteRequest);
        await client.PostAsync("api/quotes", quoteRequest);
        await client.PostAsync("api/quotes", quoteRequest);

        var quoteId = await quoteResponse.ReadJson<Guid>();


        var userRequest = UserData.CreateEmptyUserRequest();

        var userResponse = await client.PostAsync("api/users/register", userRequest);

        var userId = await userResponse.ReadJson<Guid>();


        client.Logout();


        var ids = new AdminIdsFixture
        {
            CategoryId = categoryId,
            AuthorId = authorId,
            QuoteId = quoteId,
            UserId = userId
        };

        return ids;
    }

    public static async Task<UserIdsFixture> SeedUserWithDataAsync(
        this HttpClient client,
        Guid categoryId)
    {
        var userRequest = UserData.CreateUserWithDataRequest();

        var userResponse = await client.PostAsync("api/users/register", userRequest);

        var userId = await userResponse.ReadJson<Guid>();


        await client.UserWithDataLoginAsync();


        var authorRequest = AuthorData.CreateAuthorByUserRequest();

        var authorResponse = await client.PostAsync("api/authors/own", authorRequest);

        var authorId = await authorResponse.ReadJson<Guid>();


        var quoteRequest = QuoteData.CreateUserQuoteRequest(categoryId);

        var quoteResponse = await client.PostAsync("api/quotes/own", quoteRequest);

        var quoteId = await quoteResponse.ReadJson<Guid>();


        client.Logout();


        var ids = new UserIdsFixture
        {
            UserId = userId,
            AuthorId = authorId,
            QuoteId = quoteId
        };

        return ids;
    }
}