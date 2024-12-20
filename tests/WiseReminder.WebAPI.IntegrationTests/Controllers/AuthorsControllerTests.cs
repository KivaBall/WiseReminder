namespace WiseReminder.IntegrationTests.Controllers;

public sealed class AuthorsControllerTests : GenericControllerTests
{
    [Fact]
    public async Task CreateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var request = AuthorData.BaseAuthorRequest();

        //Act
        var createResponse = await Client.PostAsJsonAsync("api/authors", request);
        var id = await createResponse.Content.ReadFromJsonAsync<Guid>();
        var getResponse = await Client.GetFromJsonAsync<AuthorDto>($"api/authors/{id}");

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse!.Id.Should().Be(id);
        getResponse.Name.Should().Be(AuthorData.DefaultName);
        getResponse.Biography.Should().Be(AuthorData.DefaultBiography);
        getResponse.DateOfBirth.Should().Be(AuthorData.DefaultBirthDate);
        getResponse.DateOfDeath.Should().Be(AuthorData.DefaultDeathDate);
    }

    [Fact]
    public async Task CreateAuthor_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.BaseAuthorRequest();

        //Act
        var response = await Client.PostAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateAuthor_WhenDataNotValid_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var request = AuthorData.NotValidBaseAuthorRequest();

        //Act
        var response = await Client.PostAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).AuthorId;
        var request = AuthorData.UpdateAuthorRequest(id);

        //Act
        var updateResponse = await Client.PutAsJsonAsync("api/authors", request);
        var getResponse = await Client.GetFromJsonAsync<AuthorDto>($"api/authors/{id}");

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getResponse!.Id.Should().Be(id);
        getResponse.Name.Should().Be(AuthorData.UpdatedName);
        getResponse.Biography.Should().Be(AuthorData.UpdatedBiography);
        getResponse.DateOfBirth.Should().Be(AuthorData.UpdatedBirthDate);
        getResponse.DateOfDeath.Should().Be(AuthorData.UpdatedDeathDate);
    }

    [Fact]
    public async Task UpdateAuthor_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.UpdateAuthorRequest(Guid.NewGuid());

        //Act
        var response = await Client.PutAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateAuthor_WhenDataNotValid_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).AuthorId;
        var request = AuthorData.NotValidUpdateAuthorRequest(id);

        //Act
        var response = await Client.PutAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).AuthorId;

        //Act
        var response = await Client.DeleteAsync($"api/authors/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteAuthor_WhenUserNotAuthorized_ReturnsUnauthorized()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.DeleteAsync($"api/authors/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteAuthor_WhenAuthorNotExisting_ReturnsBadRequest()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = Guid.NewGuid();

        //Act
        var response = await Client.DeleteAsync($"api/authors/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAuthors_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/authors");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAuthorById_WhenAllOk_ReturnsOk()
    {
        //Arrange
        await Client.LoginAsAdminAsync();
        var id = (await Client.SeedDataAsync()).AuthorId;

        //Act
        var response = await Client.GetAsync($"api/authors/{id}");
        var author = await response.Content.ReadFromJsonAsync<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        author!.Name.Should().Be(AuthorData.DefaultName);
        author.Biography.Should().Be(AuthorData.DefaultBiography);
        author.DateOfBirth.Should().Be(AuthorData.DefaultBirthDate);
        author.DateOfDeath.Should().Be(AuthorData.DefaultDeathDate);
    }

    [Fact]
    public async Task GetAuthorById_WhenAuthorNotExisting_ReturnsNotFound()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var response = await Client.GetAsync($"api/authors/{id}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}