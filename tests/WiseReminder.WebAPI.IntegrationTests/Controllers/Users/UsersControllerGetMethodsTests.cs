namespace WiseReminder.IntegrationTests.Controllers.Users;

public sealed class UsersControllerGetMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task GetUserById_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        var userDto = await response.ReadJson<UserDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        userDto!.Id.Should().Be(AdminIds.UserId);
        userDto.Username.Should().Be(UserData.DefaultUsername);
        userDto.Login.Should().Be(UserData.DefaultLoginForEmptyUser);
    }

    [Fact]
    public async Task GetUserById_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetUserById_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetUserById_WhenUserNotExists_ReturnsNotFound()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync($"api/users/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyOwnUser_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.GetAsync("api/users/own");

        var userDto = await response.ReadJson<UserDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        userDto!.Id.Should().Be(AdminIds.UserId);
        userDto.Username.Should().Be(UserData.DefaultUsername);
        userDto.Login.Should().Be(UserData.DefaultLoginForEmptyUser);
    }

    [Fact]
    public async Task GetMyOwnUser_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.GetAsync("api/users/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetMyOwnUser_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.GetAsync("api/users/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}