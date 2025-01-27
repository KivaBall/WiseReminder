namespace WiseReminder.IntegrationTests.Controllers.Authors;

public sealed class AuthorsControllerPostMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task AdminCreateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.CreateAuthorByAdminRequest();

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/authors", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/authors/{id}");

        var authorDto = await getResponse.ReadJson<AuthorDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(id);
        authorDto.Name.Should().Be(AuthorData.Name);
        authorDto.Biography.Should().Be(AuthorData.Biography);
        authorDto.BirthDate.Should().Be(AuthorData.BirthDate);
        authorDto.DeathDate.Should().Be(AuthorData.DeathDate);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.CreateAuthorByAdminRequest();

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.CreateAuthorByAdminRequest();

        //Act
        var response = await Client.PostAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAuthorByAdminRequest();

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.CreateAuthorByUserRequest();

        //Act
        await Client.EmptyUserLoginAsync();

        var createResponse = await Client.PostAsync("api/authors/own", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/authors/{id}");

        var authorDto = await getResponse.ReadJson<AuthorDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(id);
        authorDto.Name.Should().Be(AuthorData.Name);
        authorDto.Biography.Should().Be(AuthorData.Biography);
        authorDto.BirthDate.Should().Be(AuthorData.BirthDate);
        authorDto.DeathDate.Should().BeNull();
    }

    [Fact]
    public async Task UserCreateAuthor_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.CreateAuthorByUserRequest();

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.CreateAuthorByUserRequest();

        //Act
        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAuthorByUserRequest();

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateAuthor_WhenAuthorExistsInUser_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAuthorByUserRequest();

        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.PostAsync("api/authors/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}