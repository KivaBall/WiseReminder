namespace WiseReminder.IntegrationTests.Controllers.Categories;

public sealed class CategoriesControllerDeleteMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task DeleteCategory_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var deleteResponse = await Client.DeleteAsync($"api/categories/{AdminIds.CategoryId}");

        var postGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var getQuoteResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

        getQuoteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteCategory_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.DeleteAsync($"api/categories/{AdminIds.CategoryId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/categories/{AdminIds.CategoryId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();

        var response = await Client.DeleteAsync($"api/categories/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}