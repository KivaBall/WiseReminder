namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed class AuthorsController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminCreateAuthor(AdminAuthorRequest request)
    {
        var command = request.ToAdminCreateAuthorCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserCreateAuthor(UserAuthorRequest request)
    {
        var command = request.ToUserCreateAuthorCommand(UserId);
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminUpdateAuthor(Guid id, AdminAuthorRequest request)
    {
        var command = request.ToAdminUpdateAuthorCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserUpdateAuthor(UserAuthorRequest request)
    {
        var command = request.ToUserUpdateAuthorCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminDeleteAuthor(Guid id)
    {
        var command = new AdminDeleteAuthorCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UserDeleteAuthor()
    {
        var command = new UserDeleteAuthorCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var query = new GetAuthorDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAuthorByUserId(Guid userId)
    {
        var query = new GetAuthorDtoByUserIdQuery(userId);
        return await ExecuteQuery(query);
    }


    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnAuthor()
    {
        var query = new GetAuthorDtoByUserIdQuery(UserId);
        return await ExecuteQuery(query);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var query = new GetAuthorDtosQuery();
        return await ExecuteQuery(query);
    }
}