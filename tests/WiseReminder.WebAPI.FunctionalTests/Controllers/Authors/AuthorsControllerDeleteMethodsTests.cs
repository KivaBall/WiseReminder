namespace WiseReminder.IntegrationTests.Controllers.Authors;

public sealed class AuthorsControllerDeleteMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminDeleteAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        var deleteResponse = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        var postGetResponse = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        var getQuoteResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        getQuoteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/authors/{AdminIds.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenAuthorBelongsToUser_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{UserIds.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync("api/authors/own");

        var deleteResponse = await Client.DeleteAsync("api/authors/own");

        var postGetResponse = await Client.GetAsync("api/authors/own");

        var getQuoteResponse = await Client.GetAsync($"api/quotes/{UserIds.QuoteId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        getQuoteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync("api/authors/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync("api/authors/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserDeleteAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var deleteResponse = await Client.DeleteAsync("api/authors/own");

        //Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}