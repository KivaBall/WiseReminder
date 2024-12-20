﻿namespace WiseReminder.Infrastructure;

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

        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisCache");
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
                var categoryFile =
                    File.ReadAllText(
                        @"C:\Users\KievBall\source\repos\WiseReminder\src\WiseReminder.Infrastructure\Seeding\CategorySeed.sql");
                context.Database.ExecuteSqlRaw(categoryFile);
            }

            if (!context.Authors.Any())
            {
                var authorFile =
                    File.ReadAllText(
                        @"C:\Users\KievBall\source\repos\WiseReminder\src\WiseReminder.Infrastructure\Seeding\AuthorSeed.sql");
                context.Database.ExecuteSqlRaw(authorFile);
            }

            if (!context.Quotes.Any())
            {
                var quoteFile =
                    File.ReadAllText(
                        @"C:\Users\KievBall\source\repos\WiseReminder\src\WiseReminder.Infrastructure\Seeding\QuoteSeed.sql");
                context.Database.ExecuteSqlRaw(quoteFile);
            }
        }
    }
}