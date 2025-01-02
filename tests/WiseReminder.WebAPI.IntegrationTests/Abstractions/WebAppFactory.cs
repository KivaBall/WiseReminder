namespace WiseReminder.IntegrationTests.Abstractions;

public sealed class WebAppFactory : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithName($"test-postgres-{Guid.NewGuid()}")
        .WithUsername("postgres")
        .WithPassword("123456")
        .WithDatabase("wise_reminder")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:latest")
        .WithName($"test-redis-{Guid.NewGuid()}")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var redisTask = _redisContainer.StartAsync();
        var postgresqlTask = _postgreSqlContainer.StartAsync();

        Task.WaitAll(redisTask, postgresqlTask);

        builder.ConfigureAppConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:RedisConnection"] = _redisContainer.GetConnectionString(),
                ["ConnectionStrings:DbConnection"] = _postgreSqlContainer.GetConnectionString()
            }!);
        });
    }

    public override async ValueTask DisposeAsync()
    {
        await _redisContainer.StopAsync();
        await _postgreSqlContainer.StopAsync();

        await base.DisposeAsync();
    }
}