using WiseReminder.Infrastructure.Seeding;

namespace WiseReminder.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));
        });

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });
        services.AddSingleton<ICacheService, CacheService>();

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();
        }
    }

    public static void ApplySeeding(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.IsSqlServer())
        {
            if (!context.Categories.Any())
            {
                context.Database.ExecuteSqlRaw(CategorySeed.Sql);
            }

            if (!context.Authors.Any())
            {
                context.Database.ExecuteSqlRaw(AuthorSeed.Sql);
            }

            if (!context.Quotes.Any())
            {
                context.Database.ExecuteSqlRaw(QuoteSeed.Sql);
            }
        }
    }
}