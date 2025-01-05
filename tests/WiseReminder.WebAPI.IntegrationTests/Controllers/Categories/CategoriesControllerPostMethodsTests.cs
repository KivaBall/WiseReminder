namespace WiseReminder.IntegrationTests.Controllers.Categories;

public sealed class CategoriesControllerPostMethodsTests : BaseControllerTests
{
    [Fact]
    public async Task CreateCategory_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/categories", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/categories/{id}");

        var categoryDto = await getResponse.ReadJson<CategoryDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        categoryDto!.Id.Should().Be(id);
        categoryDto.Name.Should().Be(CategoryData.DefaultName);
        categoryDto.Description.Should().Be(CategoryData.DefaultDescription);
    }

    [Fact]
    public async Task CreateCategory_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateCategory_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = CategoryData.CreateCategoryRequest;

        //Act
        var response = await Client.PostAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateCategory_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = CategoryData.InvalidCategoryRequest;

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/categories", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}