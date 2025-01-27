namespace WiseReminder.WebAPI.Controllers.Quotes;

public sealed class QuotesController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateQuoteByAdmin(QuoteByAdminRequest request)
    {
        var command = request.ToCreateQuoteByAdminCommand();
        return await ExecuteCommand(command);
    }

    [HttpPost("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateMyQuote(QuoteByUserRequest request)
    {
        var command = request.ToCreateQuoteByUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpPut("{id:guid}/reaction")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> PutReaction(Guid id, bool isLike)
    {
        var command = new PutReactionCommand(id, UserId, isLike);
        return await ExecuteCommand(command);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateQuoteByAdmin(Guid id, QuoteByAdminRequest request)
    {
        var command = request.ToUpdateQuoteByAdminCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/{id:guid}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateMyQuote(Guid id, QuoteByUserRequest request)
    {
        var command = request.ToUpdateQuoteByUserCommand(id, UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteQuoteByAdmin(Guid id)
    {
        var command = new DeleteQuoteByAdminCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("own/{id:guid}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteMyQuote(Guid id)
    {
        var command = new DeleteQuoteByUserCommand(id, UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id:guid}/reaction")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteReaction(Guid id)
    {
        var command = new DeleteReactionCommand(id, UserId);
        return await ExecuteCommand(command);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyQuotes()
    {
        var query = new GetQuoteDtosByUserIdQuery(UserId);
        return await ExecuteQuery(query);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetQuoteById(Guid id, [FromQuery] string? desiredLanguage)
    {
        var query = new GetQuoteDtoByIdQuery(id, desiredLanguage);
        return await ExecuteQuery(query);
    }

    [HttpGet]
    public async Task<IActionResult> GetQuotes([FromQuery] GetQuotesRequest request)
    {
        var query = request.ToGetQuoteDtosQuery();
        return await ExecuteQuery(query);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomQuotes([FromQuery] int? amount,
        [FromQuery] string? desiredLanguage)
    {
        var query = new GetRandomQuoteDtosQuery(amount ?? 1, desiredLanguage);
        return await ExecuteQuery(query);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentQuotes([FromQuery] int? amount,
        [FromQuery] string? desiredLanguage)
    {
        var query = new GetRecentAddedQuoteDtosQuery(amount ?? 1, desiredLanguage);
        return await ExecuteQuery(query);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyQuote([FromQuery] string? desiredLanguage)
    {
        var query = new GetDailyQuoteDtoQuery(desiredLanguage);
        return await ExecuteQuery(query);
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeeklyQuote([FromQuery] string? desiredLanguage)
    {
        var query = new GetWeeklyQuoteDtoQuery(desiredLanguage);
        return await ExecuteQuery(query);
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthlyQuote([FromQuery] string? desiredLanguage)
    {
        var query = new GetMonthlyQuoteDtoQuery(desiredLanguage);
        return await ExecuteQuery(query);
    }
}