namespace WiseReminder.WebAPI.Controllers.Authorization;

[Route("api/auth")]
public sealed class AuthorizationController(ISender sender, IJwtService jwtService)
    : GenericController(sender)
{
    [HttpPost("login-as-admin")]
    public IActionResult Login(SignInRequest request)
    {
        if (request.Login == "DORKVMSJEUHDJFKW" && request.Password == "AIWNFLTISJDNVYWT")
        {
            var token = jwtService.GenerateJwtToken();
            return Ok(token);
        }

        return BadRequest();
    }
}