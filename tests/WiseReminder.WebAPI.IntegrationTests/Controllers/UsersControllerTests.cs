namespace WiseReminder.IntegrationTests.Controllers;

public sealed class UsersControllerTests : BaseControllerTests
{
    [Fact]
    public async Task RegisterUser_WhenAllOk_ReturnsOk()
    {
        // Arrange
        var request = new UserRequest
        {
            Username = UserData.DefaultUsername,
            Login = "RegisterUser",
            Password = UserData.DefaultPassword
        };

        // Act
        var response = await Client.PostAsJsonAsync("api/users/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RegisterUser_WhenInvalid_ReturnsBadRequest()
    {
        // Arrange
        var request = UserData.InvalidUserRequest;

        // Act
        var response = await Client.PostAsJsonAsync("api/users/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RegisterUser_WhenLoginExists_ReturnsBadRequest()
    {
        // Arrange
        var request = UserData.CreateEmptyUserRequest;

        // Act
        var response = await Client.PostAsJsonAsync("api/users/register", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminLogin_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = UserData.DefaultAdminLoginRequest;

        //Act
        var response = await Client.PostAsync("api/users/admin-login", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task AdminLogin_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = UserData.InvalidAdminLoginRequest;

        //Act
        var response = await Client.PostAsync("api/users/admin-login", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserLogin_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = UserData.EmptyUserLoginRequest;

        //Act
        var response = await Client.PostAsync("api/users/login", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UserLogin_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = UserData.InvalidUserLoginRequest;

        //Act
        var response = await Client.PostAsync("api/users/login", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

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
        preUserDto.Login.Should().Be(UserData.DefaultUsername);

        changeUsernameResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postUserDto!.Id.Should().Be(AdminIds.UserId);
        postUserDto.Username.Should().Be(UserData.UpdatedUsername);
        postUserDto.Login.Should().Be(UserData.DefaultUsername);
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

    [Fact]
    public async Task DeleteUser_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        var deleteResponse = await Client.DeleteAsync($"api/users/{AdminIds.UserId}");

        var postGetResponse = await Client.GetAsync($"api/users/{AdminIds.UserId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
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

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteMyOwnUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
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