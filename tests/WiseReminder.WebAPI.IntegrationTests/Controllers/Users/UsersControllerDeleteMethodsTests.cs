namespace WiseReminder.IntegrationTests.Controllers.Users;

public sealed class UsersControllerDeleteMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task DeleteUser_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/users/{UserIds.UserId}");

        var deleteResponse = await Client.DeleteAsync($"api/users/{UserIds.UserId}");

        var postGetResponse = await Client.GetAsync($"api/users/{UserIds.UserId}");

        var getAuthorResponse = await Client.GetAsync($"api/authors/{UserIds.AuthorId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        getAuthorResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteUser_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/users/{AdminIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteUser_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/users/{AdminIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteUser_WhenUserNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/users/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteMyOwnUser_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var preGetResponse = await Client.GetAsync("api/users/own");

        var deleteMyOwnUserResponse = await Client.DeleteAsync("api/users/own");

        var postGetResponse = await Client.GetAsync("api/users/own");

        var getAuthorResponse = await Client.GetAsync("api/authors/own");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteMyOwnUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        getAuthorResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteMyOwnUser_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync("api/users/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteMyOwnUser_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync("api/users/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}