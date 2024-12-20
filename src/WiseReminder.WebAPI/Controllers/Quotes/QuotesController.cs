﻿namespace WiseReminder.WebAPI.Controllers.Quotes;

[Route("api/quotes")]
public sealed class QuotesController(ISender sender) : GenericController(sender)
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateQuote(BaseQuoteRequest request)
    {
        var command = request.ToCreateQuoteCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuoteById(Guid id)
    {
        var query = new GetQuoteDtoByIdQuery { Id = id };
        return await ExecuteQuery(query);
    }

    [HttpGet("by-author/{authorId}")]
    public async Task<IActionResult> GetQuotesByAuthorId(Guid authorId)
    {
        var query = new GetQuotesByAuthorIdQuery { AuthorId = authorId };
        return await ExecuteQuery(query);
    }

    [HttpGet("by-category/{categoryId}")]
    public async Task<IActionResult> GetQuotesByCategoryId(Guid categoryId)
    {
        var query = new GetQuotesByCategoryIdQuery { CategoryId = categoryId };
        return await ExecuteQuery(query);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomQuote()
    {
        var query = new GetRandomQuoteQuery();
        return await ExecuteQuery(query);
    }

    [HttpGet("random/{amount}")]
    public async Task<IActionResult> GetRandomQuotes(int amount)
    {
        var query = new GetRandomQuotesQuery { Amount = amount };
        return await ExecuteQuery(query);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetQuoteOfTheDay()
    {
        var query = new GetQuoteOfTheDay();
        return await ExecuteQuery(query);
    }

    [HttpGet("recent/{amount}")]
    public async Task<IActionResult> GetRecentAddedQuotes(int amount)
    {
        var query = new GetRecentAddedQuotes { Amount = amount };
        return await ExecuteQuery(query);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateQuote(UpdateQuoteRequest request)
    {
        var command = request.ToUpdateQuoteCommand();
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteQuote(Guid id)
    {
        var command = new DeleteQuoteCommand { Id = id };
        return await ExecuteCommand(command);
    }
}