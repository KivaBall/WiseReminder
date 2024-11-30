namespace WiseReminder.WebAPI;

public static class PresentationExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddOpenApi();
        services.AddAuth(configuration);

        return services;
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
                        Encoding.UTF8.GetBytes(configuration["JWTPassword"] ??
                                               throw new Exception("JWTPassword isn't in appsettings.json")))
                };
            });
        services.AddAuthorization();
    }
}