namespace WiseReminder.WebAPI.Controllers.Users;

public sealed class UsersController(ISender sender) : BaseController(sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserRequest request)
    {
        var command = request.ToRegisterUserCommand();
        return await ExecuteCommand(command);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsUser(LoginAsUserRequest request)
    {
        var command = request.ToLoginAsUserCommand();
        return await ExecuteCommand(command);
    }

    [HttpPost("admin-login")]
    public async Task<IActionResult> LoginAsAdmin(LoginAsAdminRequest request)
    {
        var command = request.ToLoginAsAdminCommand();
        return await ExecuteCommand(command);
    }

    [HttpPut("{id:guid}/subscription")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApplySubscription(Guid id, ApplySubscriptionRequest request)
    {
        var command = request.ToApplySubscriptionCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = request.ToUpdateUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteMyUser()
    {
        var command = new DeleteUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyUser()
    {
        var query = new GetUserDtoByIdQuery(UserId);
        return await ExecuteQuery(query);
    }
}