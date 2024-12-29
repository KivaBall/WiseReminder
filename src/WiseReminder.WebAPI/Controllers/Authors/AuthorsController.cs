namespace WiseReminder.WebAPI.Controllers.Authors;

public sealed class AuthorsController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAuthorAsAdmin(BaseAuthorAsAdminRequest request)
    {
        var command = request.ToCreateAuthorAsAdminCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> CreateAuthorAsUser(BaseAuthorAsUserRequest request)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = request.ToCreateAuthorAsUserCommand(userId);
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAuthorAsAdmin(Guid id, BaseAuthorAsAdminRequest request)
    {
        var command = request.ToUpdateAuthorAsAdminCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateAuthorAsUser(BaseAuthorAsUserRequest asAdminRequest)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = asAdminRequest.ToUpdateAuthorAsUserCommand(userId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAuthorAsAdmin(Guid id)
    {
        var command = new DeleteAuthorAsAdminCommand { Id = id };
        return await ExecuteCommand(command);
    }

    [HttpDelete("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteAuthorAsUser()
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = new DeleteAuthorAsUserCommand { UserId = userId };
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var query = new GetAuthorDtoByIdQuery { Id = id };
        return await ExecuteQuery(query);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAuthorByUserId(Guid userId)
    {
        var query = new GetAuthorDtoByUserIdQuery { UserId = userId };
        return await ExecuteQuery(query);
    }


    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnAuthor()
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var query = new GetAuthorDtoByUserIdQuery { UserId = userId };
        return await ExecuteQuery(query);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var query = new GetAuthorDtosQuery();
        return await ExecuteQuery(query);
    }
}