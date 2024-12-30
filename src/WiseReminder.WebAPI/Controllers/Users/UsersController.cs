namespace WiseReminder.WebAPI.Controllers.Users;

public sealed class UsersController(ISender sender) : BaseController(sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserRequest request)
    {
        var command = request.ToCreateUserCommand();
        return await ExecuteCommandWithEntity(command);
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

    [HttpPut("own/username")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateUsername(UpdateUsernameRequest request)
    {
        var command = request.ToChangeUsernameCommand(UserId);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/password")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
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

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnUser()
    {
        var query = new GetUserDtoByIdQuery(UserId);
        return await ExecuteQuery(query);
    }
}