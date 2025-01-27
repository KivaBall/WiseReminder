namespace WiseReminder.IntegrationTests.Controllers.Quotes;

public sealed class QuotesControllerGetMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task GetQuoteById_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        var quoteDto = await response.ReadJson<QuoteDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDto!.Id.Should().Be(AdminIds.QuoteId);
        quoteDto.OriginalText.Should().Be(QuoteData.Text);
        quoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        quoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        quoteDto.QuoteDate.Should().Be(QuoteData.QuoteDate);
    }

    [Fact]
    public async Task GetQuoteById_WhenQuoteNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/{Guid.NewGuid()}");

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
        const int amount = 2;

        //Act
        var response = await Client.GetAsync($"api/quotes/random?amount={amount}");

        var quoteDtos = await response.ReadJson<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDtos.Should().HaveCount(amount);
    }

    [Fact]
    public async Task GetDailyQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/daily");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRecentQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/recent");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRecentQuotes_WhenAllOk_ReturnsOk()
    {
        //Arrange
        const int amount = 2;

        //Act
        var response = await Client.GetAsync($"api/quotes/recent?amount={amount}");

        var quoteDtos = await response.ReadJson<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDtos.Should().HaveCount(amount);
    }

    [Fact]
    public async Task GetMyOwnQuotes_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.GetAsync("api/quotes/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetMyOwnQuotes_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync("api/quotes/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetMyOwnQuotes_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}