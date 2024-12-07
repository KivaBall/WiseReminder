namespace WiseReminder.WebAPI.Controllers.Categories;

[Route("api/categories")]
public sealed class CategoriesController(ISender sender) : GenericController(sender)
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCategory(BaseCategoryRequest request)
    {
        var command = request.ToCreateCategoryCommand();
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var query = new GetCategoryDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var query = new GetAllCategoriesQuery();
        return await ExecuteQuery(query);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
    {
        var command = request.ToUpdateCategoryCommand();
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand(id);
        return await ExecuteCommand(command);
    }
}