using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseReminder.Application.Quotes.CreateQuote;
using WiseReminder.Application.Quotes.DeleteQuote;
using WiseReminder.Application.Quotes.GetQuoteById;
using WiseReminder.Application.Quotes.GetQuotesByAuthorId;
using WiseReminder.Application.Quotes.GetQuotesByCategoryId;
using WiseReminder.Application.Quotes.GetRandomQuote;
using WiseReminder.Application.Quotes.UpdateQuote;

namespace WiseReminder.WebAPI.Controllers.Quotes;

[ApiController]
[Route("api/quotes")]
public class QuotesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateQuote(CreateQuoteRequest request)
    {
        var command = new CreateQuoteCommand(request.Text, request.AuthorId, request.CategoryId, request.QuoteDate);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuoteById(Guid id)
    {
        var query = new GetQuoteByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : BadRequest(result);
    }

    [HttpGet("by-author/{authorId}")]
    public async Task<IActionResult> GetQuotesByAuthorId(Guid authorId)
    {
        var query = new GetQuotesByAuthorIdQuery(authorId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : BadRequest(result);
    }

    [HttpGet("by-category/{categoryId}")]
    public async Task<IActionResult> GetQuotesByCategoryId(Guid categoryId)
    {
        var query = new GetQuotesByCategoryIdQuery(categoryId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : BadRequest(result);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomQuote()
    {
        var query = new GetRandomQuoteQuery();

        var result = await _sender.Send(query);

        return Ok(result.Entity);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateQuote(UpdateQuoteRequest request)
    {
        var command = new UpdateQuoteCommand(request.Id, request.Text, request.AuthorId, request.CategoryId,
            request.QuoteDate);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteQuote(Guid id)
    {
        var command = new DeleteQuoteCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }
}