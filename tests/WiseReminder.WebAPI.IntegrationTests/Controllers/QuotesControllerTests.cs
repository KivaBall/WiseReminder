namespace WiseReminder.IntegrationTests.Controllers;

public sealed class QuotesControllerTests : GenericControllerTests
{
    [Fact]
    public async Task CreateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var initialIds = await Client.SeedDataAsync();
        var request = QuoteData.BaseQuoteRequest(initialIds.AuthorId, initialIds.CategoryId);

        //Act
        var createResponse = await Client.PostAsJsonAsync("api/quotes", request);
        var id = await createResponse.Content.ReadFromJsonAsync<Guid>();
        var getResponse = await Client.GetFromJsonAsync<QuoteDto>($"api/quotes/{id}");

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse!.Id.Should().Be(id);
        getResponse.Text.Should().Be(QuoteData.DefaultText);
        getResponse.AuthorId.Should().Be(initialIds.AuthorId);
        getResponse.CategoryId.Should().Be(initialIds.CategoryId);
        getResponse.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task CreateQuote_WhenUserNotAuthorized_ReturnsNotAuthorized()
    {
        //Arrange
        var request = QuoteData.BaseQuoteRequest(Guid.NewGuid(), Guid.NewGuid());

        //Act
        var response = await Client.PostAsJsonAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateQuote_WhenDataNotValid_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var initialIds = await Client.SeedDataAsync();
        var request =
            QuoteData.NotValidBaseQuoteRequest(initialIds.AuthorId, initialIds.CategoryId);

        //Act
        var createResponse = await Client.PostAsJsonAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var initialIds = await Client.SeedDataAsync();
        var request = QuoteData.UpdateQuoteRequest(initialIds.QuoteId, initialIds.AuthorId,
            initialIds.CategoryId);

        //Act
        var updateResponse = await Client.PutAsJsonAsync("api/quotes", request);
        var getResponse =
            await Client.GetFromJsonAsync<QuoteDto>($"api/quotes/{initialIds.QuoteId}");

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse!.Id.Should().Be(initialIds.QuoteId);
        getResponse.Text.Should().Be(QuoteData.UpdatedText);
        getResponse.AuthorId.Should().Be(initialIds.AuthorId);
        getResponse.CategoryId.Should().Be(initialIds.CategoryId);
        getResponse.QuoteDate.Should().Be(QuoteData.UpdatedQuoteDate);
    }

    [Fact]
    public async Task UpdateQuote_WhenUserNotAuthorized_ReturnsNotAuthorized()
    {
        //Arrange
        var request = QuoteData.UpdateQuoteRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        //Act
        var response = await Client.PutAsJsonAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateQuote_WhenQuoteNotExisting_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var request = QuoteData.UpdateQuoteRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        //Act
        var response = await Client.PutAsJsonAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateQuote_WhenDataNotValid_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var initialIds = await Client.SeedDataAsync();
        var request = QuoteData.NotValidUpdateQuoteRequest(initialIds.QuoteId, initialIds.AuthorId,
            initialIds.CategoryId);

        //Act
        var updateResponse = await Client.PutAsJsonAsync("api/quotes", request);

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).QuoteId;

        //Act
        var deleteResponse = await Client
            .DeleteAsync($"api/quotes/{id}");
        var getResponse = await Client
            .GetAsync($"api/quotes/{id}");

        //Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteQuote_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.DeleteAsync($"api/quotes/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteQuote_WhenQuoteNotExisting_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = Guid.NewGuid();

        //Act
        var response = await Client.DeleteAsync($"api/quotes/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetQuoteById_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var initialIds = await Client.SeedDataAsync();

        //Act
        var response = await Client.GetAsync($"api/quotes/{initialIds.QuoteId}");
        var quote = await response.Content.ReadFromJsonAsync<QuoteDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        quote!.Id.Should().Be(initialIds.QuoteId);
        quote.Text.Should().Be(QuoteData.DefaultText);
        quote.AuthorId.Should().Be(initialIds.AuthorId);
        quote.CategoryId.Should().Be(initialIds.CategoryId);
        quote.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task GetQuoteById_WhenQuoteNotExisting_ReturnsNotFound()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.GetAsync($"api/quotes/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetQuotesByAuthorId_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var authorId = (await Client.SeedDataAsync()).AuthorId;

        //Act
        var response = await Client.GetAsync($"api/quotes/by-author/{authorId}");
        var quotes = await response.Content.ReadFromJsonAsync<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        quotes.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetQuotesByAuthorId_WhenAuthorNotExisting_ReturnsNotFound()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.GetAsync($"api/quotes/by-author/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetQuotesByCategoryId_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var categoryId = (await Client.SeedDataAsync()).CategoryId;

        //Act
        var response = await Client.GetAsync($"api/quotes/by-category/{categoryId}");
        var quotes = await response.Content.ReadFromJsonAsync<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        quotes.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetQuotesByCategoryId_WhenCategoryNotExisting_ReturnsNotFound()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.GetAsync($"api/quotes/by-category/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetRandomQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/random");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRandomQuotes_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var amount = 2;

        //Act
        var response = await Client.GetAsync($"api/quotes/random/{amount}");
        var quotes = await response.Content.ReadFromJsonAsync<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        quotes.Should().HaveCount(amount);
    }

    [Fact]
    public async Task GetQuoteOfTheDay_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/daily");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRecentAddedQuotes_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var amount = 2;

        //Act
        var response = await Client.GetAsync($"api/quotes/recent/{amount}");
        var quotes = await response.Content.ReadFromJsonAsync<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        quotes.Should().HaveCount(amount);
    }
}