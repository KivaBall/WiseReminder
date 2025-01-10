namespace WiseReminder.WebAPI;

public static class PresentationExtensions
{
    public static void AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRouting(opt => opt.LowercaseUrls = true);

        services.AddControllers();

        services.AddValidation();

        services.AddAuth(configuration);

        services.AddOpenApi();
    }

    private static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "WiseReminder.com",
                    ValidAudience = "WiseReminder.com",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWTPassword"]!))
                };
            });

        services.AddAuthorization();
    }
}