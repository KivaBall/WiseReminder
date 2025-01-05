namespace WiseReminder.IntegrationTests.Controllers.Users;

public sealed class UsersControllerPostMethodsTests : BaseControllerTests
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
}