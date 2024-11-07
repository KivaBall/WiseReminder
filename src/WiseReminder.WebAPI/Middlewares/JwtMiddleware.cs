namespace WiseReminder.WebAPI.Middlewares;

public sealed class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("admin", out var token))
            context.Request.Headers.Add("Authorization", $"Bearer {token}");

        await _next(context);
    }
}