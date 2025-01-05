namespace WiseReminder.IntegrationTests.Controllers.Quotes;

public sealed class QuotesControllerPostMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminCreateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/quotes/{id}");

        var quoteDto = await getResponse.ReadJson<QuoteDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDto!.Id.Should().Be(id);
        quoteDto.Text.Should().Be(QuoteData.DefaultText);
        quoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        quoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        quoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(Guid.NewGuid(), Guid.NewGuid());

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(Guid.NewGuid(), Guid.NewGuid());

        //Act
        var response = await Client.PostAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidAdminQuoteRequest;

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenQuotesForAuthorOfUser_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(UserIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(Guid.NewGuid(), AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(AdminIds.AuthorId, Guid.NewGuid());

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes/own", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/quotes/{id}");

        var quoteDto = await getResponse.ReadJson<QuoteDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDto!.Id.Should().Be(id);
        quoteDto.Text.Should().Be(QuoteData.DefaultText);
        quoteDto.AuthorId.Should().Be(UserIds.AuthorId);
        quoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        quoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task UserCreateQuote_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserCreateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserCreateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidUserQuoteRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}