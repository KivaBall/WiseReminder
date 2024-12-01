namespace WiseReminder.WebAPI.Controllers.Authorization;

[Route("api/auth")]
public sealed class AuthorizationController(ISender sender, IJwtService jwtService) : GenericController(sender)
{
    private readonly IJwtService _jwtService = jwtService;

    [HttpPost("login-as-admin")]
    public IActionResult Login(SignInRequest request)
    {
        if (request.Login == "DORKVMSJEUHDJFKW" && request.Password == "AIWNFLTISJDNVYWT")
        {
            var token = _jwtService.GenerateJwtToken();
            return Ok(token);
        }

        return BadRequest();
    }
}