namespace WiseReminder.IntegrationTests.Controllers.Categories;

public sealed class CategoriesControllerPutMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task UpdateCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var preCategoryDto = await preGetResponse.ReadJson<CategoryDto>();

        var updateResponse =
            await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        var postGetResponse = await Client.GetAsync($"api/categories/{AdminIds.CategoryId}");

        var postCategoryDto = await postGetResponse.ReadJson<CategoryDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        preCategoryDto!.Id.Should().Be(AdminIds.CategoryId);
        preCategoryDto.Name.Should().Be(CategoryData.Name);
        preCategoryDto.Description.Should().Be(CategoryData.Description);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        postCategoryDto!.Id.Should().Be(AdminIds.CategoryId);
        postCategoryDto.Name.Should().Be(CategoryData.NewName);
        postCategoryDto.Description.Should().Be(CategoryData.NewDescription);
    }

    [Fact]
    public async Task UpdateCategory_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        var response = await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateCategory_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = CategoryData.UpdateCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/categories/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateCategory_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = CategoryData.InvalidCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/categories/{AdminIds.CategoryId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}