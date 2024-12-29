namespace WiseReminder.WebAPI.Controllers.Users;

[Route("api/users")]
public sealed class UsersController(ISender sender) : GenericController(sender)
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(BaseUserRequest request)
    {
        var command = request.ToCreateUserCommand();
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAsUserRequest request)
    {
        var command = new LoginAsUserCommand { Login = request.Login, Password = request.Password };
        return await ExecuteCommandWithEntity(command);
    }

    [HttpPost("admin-login")]
    public async Task<IActionResult> Login(LoginAsAdminRequest request)
    {
        var command = new LoginAsAdminCommand
        {
            FirstPassword = request.FirstPassword,
            SecondPassword = request.SecondPassword,
            ThirdPassword = request.ThirdPassword
        };

        return await ExecuteCommandWithEntity(command);
    }

    [HttpPut("own/username")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateUsername(UpdateUsernameRequest request)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = request.ToChangeUsernameCommand(userId);
        return await ExecuteCommand(command);
    }

    [HttpPut("own/password")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = request.ToChangePasswordCommand(userId);
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserAsAdmin(Guid id)
    {
        var command = new DeleteUserCommand { Id = id };
        return await ExecuteCommand(command);
    }

    [HttpDelete("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteMyOwnUser()
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var command = new DeleteUserCommand { Id = userId };
        return await ExecuteCommand(command);
    }

    [HttpGet("own")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetMyOwnUser()
    {
        var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
        var query = new GetUserDtoByIdQuery { Id = userId };
        return await ExecuteQuery(query);
    }
}