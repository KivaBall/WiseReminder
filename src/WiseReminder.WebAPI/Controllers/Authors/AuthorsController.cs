using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseReminder.Application.Authors.CreateAuthor;
using WiseReminder.Application.Authors.DeleteAuthor;
using WiseReminder.Application.Authors.GetAllAuthors;
using WiseReminder.Application.Authors.GetAuthorById;
using WiseReminder.Application.Authors.UpdateAuthor;

namespace WiseReminder.WebAPI.Controllers.Authors;

[ApiController]
[Route("api/authors")]
public sealed class AuthorsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateAuthor(CreateAuthorRequest request)
    {
        var command =
            new CreateAuthorCommand(request.Name, request.Biography, request.DateOfBirth, request.DateOfDeath);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var query = new GetAuthorByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : NotFound(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAuthors()
    {
        var query = new GetAllAuthorsQuery();

        var result = await _sender.Send(query);

        return Ok(result.Entity);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest request)
    {
        var command = new UpdateAuthorCommand(request.Id, request.Name, request.Biography, request.DateOfBirth,
            request.DateOfDeath);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest();
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var command = new DeleteAuthorCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest();
    }
}