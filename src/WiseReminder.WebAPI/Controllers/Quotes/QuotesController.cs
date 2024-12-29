namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed class QuotesController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateQuoteAsAdmin(BaseQuoteAsAdminRequest asAdminRequest)
    {
        var command = asAdminRequest.ToCreateQuoteAsAdminCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateQuoteAsUser(BaseQuoteAsUserRequest asAdminRequest)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = asAdminRequest.ToCreateQuoteAsUserCommand(userId);
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateQuoteAsAdmin(Guid id,
        BaseQuoteAsAdminRequest asAdminRequest)
    {
        var command = asAdminRequest.ToUpdateQuoteAsAdminCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateQuoteAsUser(Guid id,
        BaseQuoteAsUserRequest asAdminRequest)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = asAdminRequest.ToUpdateQuoteAsUserCommand(id, userId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteQuoteAsAdmin(Guid id)
    {
        var command = new DeleteQuoteAsAdminCommand { Id = id };
        return await ExecuteCommand(command);
    }

    [HttpDelete("own/{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteQuoteAsUser(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = new DeleteQuoteAsUserCommand { Id = id, UserId = userId };
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuoteById(Guid id)
    {
        var query = new GetQuoteDtoByIdQuery { Id = id };
        return await ExecuteQuery(query);
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetQuotesByAuthorId(Guid authorId)
    {
        var query = new GetQuoteDtosByAuthorIdQuery { AuthorId = authorId };
        return await ExecuteQuery(query);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetQuotesByCategoryId(Guid categoryId)
    {
        var query = new GetQuoteDtosByCategoryIdQuery { CategoryId = categoryId };
        return await ExecuteQuery(query);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomQuote()
    {
        var query = new GetRandomQuoteDtoQuery();
        return await ExecuteQuery(query);
    }

    [HttpGet("random/{amount}")]
    public async Task<IActionResult> GetRandomQuotes(int amount)
    {
        var query = new GetRandomQuoteDtosQuery { Amount = amount };
        return await ExecuteQuery(query);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetQuoteOfTheDay()
    {
        var query = new GetDailyQuoteDto();
        return await ExecuteQuery(query);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentAddedQuote()
    {
        var query = new GetRecentAddedQuoteDto();
        return await ExecuteQuery(query);
    }

    [HttpGet("recent/{amount}")]
    public async Task<IActionResult> GetRecentAddedQuotes(int amount)
    {
        var query = new GetRecentAddedQuoteDtos { Amount = amount };
        return await ExecuteQuery(query);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnQuotes()
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var query = new GetQuoteDtosByUserIdQuery { UserId = userId };
        return await ExecuteQuery(query);
    }
}