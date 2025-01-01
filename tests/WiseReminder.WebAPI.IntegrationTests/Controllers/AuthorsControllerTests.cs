namespace WiseReminder.IntegrationTests.Controllers;

public sealed class AuthorsControllerTests : BaseControllerTests
{
    [Fact]
    public async Task AdminCreateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.CreateAdminAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsJsonAsync("api/authors", request);

        var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var getResponse = await Client.GetFromJsonAsync<AuthorDto>($"api/authors/{id}");

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse!.Id.Should().Be(id);
        getResponse.Name.Should().Be(AuthorData.DefaultName);
        getResponse.Biography.Should().Be(AuthorData.DefaultBiography);
        getResponse.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        getResponse.DeathDate.Should().Be(AuthorData.DefaultDeathDate);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.CreateAdminAuthorRequest;

        //Act
        var response = await Client.PostAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.CreateAdminAuthorRequest;

        //Act
        var response = await Client.PostAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminCreateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAdminAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsJsonAsync("api/authors", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = AuthorData.UpdateAdminAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var updateResponse = await Client.PutAsJsonAsync($"api/authors/{Ids.AuthorId}", request);

        var getResponse = await Client.GetFromJsonAsync<AuthorDto>($"api/authors/{Ids.AuthorId}");

        //Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse!.Id.Should().Be(Ids.AuthorId);
        getResponse.Name.Should().Be(AuthorData.UpdatedName);
        getResponse.Biography.Should().Be(AuthorData.UpdatedBiography);
        getResponse.BirthDate.Should().Be(AuthorData.UpdatedBirthDate);
        getResponse.DeathDate.Should().Be(AuthorData.UpdatedDeathDate);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = AuthorData.UpdateAdminAuthorRequest;

        //Act
        var response = await Client.PutAsJsonAsync($"api/authors/{Ids.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = AuthorData.UpdateAdminAuthorRequest;

        //Act
        var response = await Client.PutAsJsonAsync($"api/authors/{Ids.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.UpdateAdminAuthorRequest;

        //Act
        var response = await Client.PutAsJsonAsync($"api/authors/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateAuthor_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = AuthorData.InvalidAdminAuthorRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsJsonAsync($"api/authors/{Ids.AuthorId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/authors/{Ids.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenUser_ReturnsForbidden()
    {
        //Act
        var response = await Client.DeleteAsync($"api/authors/{Ids.AuthorId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminDeleteAuthor_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/authors/{Ids.AuthorId}");

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
        //Act
        var response = await Client.GetAsync($"api/authors/{Ids.AuthorId}");
        
        var author = await response.Content.ReadFromJsonAsync<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        author!.Name.Should().Be(AuthorData.DefaultName);
        author.Biography.Should().Be(AuthorData.DefaultBiography);
        author.BirthDate.Should().Be(AuthorData.DefaultBirthDate);
        author.DeathDate.Should().Be(AuthorData.DefaultDeathDate);
    }

    [Fact]
    public async Task GetAuthorById_WhenAuthorNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/authors/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}