namespace WiseReminder.IntegrationTests.Controllers.Quotes;

public sealed class QuotesControllerPutMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminUpdateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        var preQuoteDto = await preGetResponse.ReadJson<QuoteDto>();

        var updateResponse = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        var postGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        var postQuoteDto = await postGetResponse.ReadJson<QuoteDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preQuoteDto!.Id.Should().Be(AdminIds.QuoteId);
        preQuoteDto.Text.Should().Be(QuoteData.DefaultText);
        preQuoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        preQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        preQuoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postQuoteDto!.Id.Should().Be(AdminIds.QuoteId);
        postQuoteDto.Text.Should().Be(QuoteData.UpdatedText);
        postQuoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        postQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        postQuoteDto.QuoteDate.Should().Be(QuoteData.UpdatedQuoteDate);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidAdminQuoteRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenQuoteBelongsToUser_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(UserIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenQuoteNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, Guid.NewGuid());

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(Guid.NewGuid(), AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/quotes/{UserIds.QuoteId}");

        var preQuoteDto = await preGetResponse.ReadJson<QuoteDto>();

        var updateResponse = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);

        var postGetResponse = await Client.GetAsync($"api/quotes/{UserIds.QuoteId}");

        var postQuoteDto = await postGetResponse.ReadJson<QuoteDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preQuoteDto!.Id.Should().Be(UserIds.QuoteId);
        preQuoteDto.Text.Should().Be(QuoteData.DefaultText);
        preQuoteDto.AuthorId.Should().Be(UserIds.AuthorId);
        preQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        preQuoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postQuoteDto!.Id.Should().Be(UserIds.QuoteId);
        postQuoteDto.Text.Should().Be(QuoteData.UpdatedText);
        postQuoteDto.AuthorId.Should().Be(UserIds.AuthorId);
        postQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        postQuoteDto.QuoteDate.Should().Be(QuoteData.UpdatedQuoteDate);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidUserQuoteRequest;

        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenNotUserQuote_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenQuoteNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(Guid.NewGuid());

        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}