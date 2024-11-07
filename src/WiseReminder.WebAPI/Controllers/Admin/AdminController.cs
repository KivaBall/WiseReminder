using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseReminder.Application.Abstractions.JWT;

namespace WiseReminder.WebAPI.Controllers.Admin;

[ApiController]
[Route("api/admin")]
public class AdminController(IJwtService jwtService) : ControllerBase
{
    private readonly IJwtService _jwtService = jwtService;

    [HttpPost("login")]
    public Task<IActionResult> Login(LoginRequest request)
    {
        if (request.Login == "DORKVMSJEUHDJFKW" && request.Password == "AIWNFLTISJDNVYWT")
        {
            var token = jwtService.GenerateJwtToken();
            Response.Cookies.Append("admin", token);
            return Task.FromResult<IActionResult>(Ok());
        }

        return Task.FromResult<IActionResult>(BadRequest());
    }

    [HttpPost("logout")]
    [Authorize]
    public Task<IActionResult> Logout()
    {
        if (Request.Cookies.ContainsKey("admin"))
        {
            Response.Cookies.Delete("admin");
            return Task.FromResult<IActionResult>(Ok());
        }

        return Task.FromResult<IActionResult>(BadRequest());
    }
}