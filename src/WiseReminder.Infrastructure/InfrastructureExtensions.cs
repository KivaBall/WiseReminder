namespace WiseReminder.Infrastructure;

public static class InfrastructureExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbServices(config);

        services.AddRepositories();

        services.AddCacheServices(config);

        services.AddSingleton<IJwtService, JwtService>();

        services.AddSingleton<IEncryptService, EncryptService>();

        services.AddScoped<ITranslationService, TranslationService>();

        services.AddScoped<HttpClient>();
    }

    private static void AddDbServices(this IServiceCollection services, IConfiguration config)
    {
        var dbConnection = config.GetConnectionString("DbConnection")
                           ?? throw new InvalidOperationException(
                               "Database connection string is not configured");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnection));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddKeyedScoped<IQuoteRepository, QuoteRepository>("original-quote-repository");
        services.AddScoped<IQuoteRepository, CachedQuoteRepository>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IReactionRepository, ReactionRepository>();
    }

    private static void AddCacheServices(this IServiceCollection services, IConfiguration config)
    {
        var redisConnection = config.GetConnectionString("RedisConnection") ??
                              throw new InvalidOperationException(
                                  "Redis connection string is not configured");

        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnection);

        services.AddSingleton<QuoteConverter>();

        services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));
    }

    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();
    }

    public static async Task ApplySeeding(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Categories.Any())
        {
            await context.Database.ExecuteSqlRawAsync(CategorySeed.Sql);
        }

        if (!context.Authors.Any())
        {
            await context.Database.ExecuteSqlRawAsync(AuthorSeed.Sql);
        }

        if (!context.Quotes.Any())
        {
            await context.Database.ExecuteSqlRawAsync(QuoteSeed.Sql);
        }
    }
}