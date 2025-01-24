namespace WiseReminder.IntegrationTests.Abstractions;

public sealed class WebAppFactory : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:12-alpine")
        .WithName($"test-postgres-{Guid.NewGuid()}")
        .WithUsername("postgres")
        .WithPassword("123456")
        .WithDatabase("wisereminder")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:6-alpine")
        .WithName($"test-redis-{Guid.NewGuid()}")
        .Build();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var redis = _redisContainer.StartAsync();
        var db = _dbContainer.StartAsync();

        Task.WaitAll(redis, db);

        EnsurePostgresConnection();

        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:RedisConnection"] = _redisContainer.GetConnectionString(),
                ["ConnectionStrings:DbConnection"] = _dbContainer.GetConnectionString(),
                ["AllowMigrations"] = "true",
                ["AllowSeeding"] = "false",
                ["AllowScalar"] = "false",
                ["Logging:LogLevel:Default"] = "Warning"
            });
        });

        return base.CreateHost(builder);
    }

    private void EnsurePostgresConnection()
    {
        for (var i = 1; i <= 100; i++)
        {
            try
            {
                using var connection = new NpgsqlConnection(_dbContainer.GetConnectionString());

                connection.Open();

                return;
            }
            catch
            {
                Thread.Sleep(250);
            }
        }

        throw new Exception("No connection established");
    }

    public override async ValueTask DisposeAsync()
    {
        await _redisContainer.StopAsync();
        await _dbContainer.StopAsync();

        await base.DisposeAsync();
    }
}