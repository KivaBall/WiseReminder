namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed class QuotesController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminCreateQuote(AdminQuoteRequest request)
    {
        var command = request.ToAdminCreateQuoteCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserCreateQuote(UserQuoteRequest request)
    {
        var command = request.ToUserCreateQuoteCommand(UserId);
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminUpdateQuote(Guid id, AdminQuoteRequest request)
    {
        var command = request.ToAdminUpdateQuoteCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserUpdateQuote(Guid id, UserQuoteRequest request)
    {
        var command = request.ToUserUpdateQuoteCommand(id, UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminDeleteQuote(Guid id)
    {
        var command = new AdminDeleteQuoteCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("own/{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserDeleteQuote(Guid id)
    {
        var command = new UserDeleteQuoteCommand(id, UserId);
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuoteById(Guid id)
    {
        var query = new GetQuoteDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet("author/{authorId}")]
    public async Task<IActionResult> GetQuotesByAuthorId(Guid authorId)
    {
        var query = new GetQuoteDtosByAuthorIdQuery(authorId);
        return await ExecuteQuery(query);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetQuotesByCategoryId(Guid categoryId)
    {
        var query = new GetQuoteDtosByCategoryIdQuery(categoryId);
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
        var query = new GetRandomQuoteDtosQuery(amount);
        return await ExecuteQuery(query);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyQuote()
    {
        var query = new GetDailyQuoteDtoQuery();
        return await ExecuteQuery(query);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentAddedQuote()
    {
        var query = new GetRecentAddedQuoteDtoQuery();
        return await ExecuteQuery(query);
    }

    [HttpGet("recent/{amount}")]
    public async Task<IActionResult> GetRecentAddedQuotes(int amount)
    {
        var query = new GetRecentAddedQuoteDtosQuery(amount);
        return await ExecuteQuery(query);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnQuotes()
    {
        var query = new GetQuoteDtosByUserIdQuery(UserId);
        return await ExecuteQuery(query);
    }
}