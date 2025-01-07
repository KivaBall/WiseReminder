namespace WiseReminder.WebAPI.Controllers.Users;

public sealed class UsersController(ISender sender) : BaseController(sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserRequest request)
    {
        var command = request.ToCreateUserCommand();
        return await ExecuteCommandWithEntity(command); //TODO: Dont return guid!
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLogin(UserLoginRequest request)
    {
        var command = request.ToUserLoginCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("admin-login")]
    public async Task<IActionResult> AdminLogin(AdminLoginRequest request)
    {
        var command = request.ToAdminLoginCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("{id}/apply-subscription")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApplySubscription(Guid id, ApplySubscriptionRequest request)
    {
        var command = request.ToApplySubscriptionCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/username")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ChangeUsername(ChangeUsernameRequest request)
    {
        var command = request.ToChangeUsernameCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/password")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var command = request.ToChangePasswordCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);
        return await ExecuteCommand(command);
    }

    [HttpDelete("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteMyOwnUser()
    {
        var command = new DeleteUserCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnUser()
    {
        var query = new GetUserDtoByIdQuery(UserId);
        return await ExecuteQuery(query);
    }
}