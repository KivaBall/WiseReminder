using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseReminder.Application.Categories.CreateCategory;
using WiseReminder.Application.Categories.DeleteCategory;
using WiseReminder.Application.Categories.GetAllCategories;
using WiseReminder.Application.Categories.GetCategoryById;
using WiseReminder.Application.Categories.UpdateCategory;

namespace WiseReminder.WebAPI.Controllers.Categories;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name, request.Description);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var query = new GetCategoryByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : BadRequest(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCategories()
    {
        var query = new GetAllCategoriesQuery();

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : BadRequest(result);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(request.Id, request.Name, request.Description);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }
}