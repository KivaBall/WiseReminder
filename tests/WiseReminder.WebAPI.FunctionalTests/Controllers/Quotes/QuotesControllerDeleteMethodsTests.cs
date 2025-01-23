namespace WiseReminder.IntegrationTests.Controllers.Quotes;

public sealed class QuotesControllerDeleteMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminDeleteQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        var deleteResponse = await Client.DeleteAsync($"api/quotes/{AdminIds.QuoteId}");

        var postGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenQuoteBelongsToUser_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/{UserIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenQuoteNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/quotes/{UserIds.QuoteId}");

        var deleteResponse = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");

        var postGetResponse = await Client.GetAsync($"api/quotes/{UserIds.QuoteId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenQuoteBelongsToAdmin_ReturnsBadRequest()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/own/{AdminIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenQuoteNotExists_ReturnnBadRequest()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/own/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenAuthorNotExists_ReturnnBadRequest()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}