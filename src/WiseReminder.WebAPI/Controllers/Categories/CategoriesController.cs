﻿namespace WiseReminder.WebAPI.Controllers.Categories;

public sealed class CategoriesController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory(CategoryRequest request)
    {
        var command = request.ToCreateCategoryCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequest request)
    {
        var command = request.ToUpdateCategoryCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand(id);
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
        var query = new GetCategoryDtosQuery();
        return await ExecuteQuery(query);
    }
}