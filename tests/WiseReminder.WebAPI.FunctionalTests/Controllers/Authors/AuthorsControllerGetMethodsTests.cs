namespace WiseReminder.IntegrationTests.Controllers.Authors;

public sealed class AuthorsControllerGetMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task GetAllAuthors_WhenAllOk_ReturnsOk()
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
        var response = await Client.GetAsync($"api/authors/{AdminIds.AuthorId}");

        var authorDto = await response.ReadJson<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Name.Should().Be(AuthorData.Name);
        authorDto.Biography.Should().Be(AuthorData.Biography);
        authorDto.BirthDate.Should().Be(AuthorData.BirthDate);
        authorDto.DeathDate.Should().Be(AuthorData.DeathDate);
    }

    [Fact]
    public async Task GetAuthorById_WhenAuthorNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/authors/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyOwnAuthor_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();

        var response = await Client.GetAsync("api/authors/own");

        var authorDto = await response.ReadJson<AuthorDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        authorDto!.Id.Should().Be(UserIds.AuthorId);
        authorDto.Name.Should().Be(AuthorData.Name);
        authorDto.Biography.Should().Be(AuthorData.Biography);
        authorDto.BirthDate.Should().Be(AuthorData.BirthDate);
        authorDto.DeathDate.Should().BeNull();
    }

    [Fact]
    public async Task GetMyOwnAuthor_WhenAuthorNotExists_ReturnsNotFound()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.GetAsync("api/authors/own");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}