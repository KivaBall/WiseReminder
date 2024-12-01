namespace WiseReminder.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("WiseReminderDatabase"));
        });

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }

    public static void ApplySeeding(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Categories.Any())
        {
            var categoryFile = File.ReadAllText("../WiseReminder.Infrastructure/Seeding/CategorySeed.sql");
            var authorFile = File.ReadAllText("../WiseReminder.Infrastructure/Seeding/AuthorSeed.sql");
            var quoteFile = File.ReadAllText("../WiseReminder.Infrastructure/Seeding/QuoteSeed.sql");
            context.Database.ExecuteSqlRaw(categoryFile);
            context.Database.ExecuteSqlRaw(authorFile);
            context.Database.ExecuteSqlRaw(quoteFile);
        }
    }
}