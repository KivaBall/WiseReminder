namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed class AuthorsController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAuthorByAdmin(AuthorByAdminRequest request)
    {
        var command = request.ToCreateAuthorByAdminCommand();
        return await ExecuteCommand(command);
    }

    [HttpPost("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateMyAuthor(AuthorByUserRequest request)
    {
        var command = request.ToCreateAuthorByUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAuthorByAdmin(Guid id, AuthorByAdminRequest request)
    {
        var command = request.ToUpdateAuthorByAdminCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateMyAuthor(AuthorByUserRequest request)
    {
        var command = request.ToUpdateAuthorByUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAuthorByAdmin(Guid id)
    {
        var command = new DeleteAuthorByAdminCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteMyAuthor()
    {
        var command = new DeleteAuthorByUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyAuthor()
    {
        var query = new GetAuthorDtoByUserIdQuery(UserId);
        return await ExecuteQuery(query);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var query = new GetAuthorDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var query = new GetAuthorDtosQuery();
        return await ExecuteQuery(query);
    }
}