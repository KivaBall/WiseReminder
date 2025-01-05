namespace WiseReminder.IntegrationTests.Controllers.Users;

public sealed class UsersControllerPutMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task ChangeUsername_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = UserData.DefaultChangeUsernameRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var preGetResponse = await Client.GetAsync("api/users/own");

        var preUserDto = await preGetResponse.ReadJson<UserDto>();

        var changeUsernameResponse = await Client.PutAsync("api/users/own/username", request);

        var postGetResponse = await Client.GetAsync("api/users/own");

        var postUserDto = await postGetResponse.ReadJson<UserDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preUserDto!.Id.Should().Be(AdminIds.UserId);
        preUserDto.Username.Should().Be(UserData.DefaultUsername);
        preUserDto.Login.Should().Be(UserData.DefaultLoginForEmptyUser);

        changeUsernameResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postUserDto!.Id.Should().Be(AdminIds.UserId);
        postUserDto.Username.Should().Be(UserData.UpdatedUsername);
        postUserDto.Login.Should().Be(UserData.DefaultLoginForEmptyUser);
    }

    [Fact]
    public async Task ChangeUsername_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = UserData.DefaultChangeUsernameRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync("api/users/own/username", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ChangeUsername_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = UserData.DefaultChangeUsernameRequest;

        //Act
        var response = await Client.PutAsync("api/users/own/username", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ChangeUsername_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = UserData.InvalidChangeUsernameRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync("api/users/own/username", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ChangePassword_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var loginRequest = UserData.EmptyUserLoginRequest;
        var changePasswordRequest = UserData.DefaultChangePasswordRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var preLoginResponse = await Client.PostAsync("api/users/login", loginRequest);

        var changePasswordResponse =
            await Client.PutAsync("api/users/own/password", changePasswordRequest);

        var postLoginResponse = await Client.PostAsync("api/users/login", loginRequest);

        //Assert
        preLoginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        changePasswordResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postLoginResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ChangePassword_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = UserData.DefaultChangePasswordRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync("api/users/own/password", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ChangePassword_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = UserData.DefaultChangePasswordRequest;

        //Act
        var response = await Client.PutAsync("api/users/own/password", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ChangePassword_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = UserData.InvalidChangePasswordRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync("api/users/own/password", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}