namespace WiseReminder.IntegrationTests.Abstractions;

public sealed class WebAppFactory : WebApplicationFactory<Program>, IDisposable
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:alpine")
        .WithName($"test-postgres-{Guid.NewGuid()}")
        .WithUsername("postgres")
        .WithPassword("123456")
        .WithDatabase("wisereminder")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:alpine")
        .WithName($"test-redis-{Guid.NewGuid()}")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var redisTask = _redisContainer.StartAsync();
        var postgresqlTask = _postgreSqlContainer.StartAsync();

        Task.WaitAll(redisTask, postgresqlTask);

        EnsurePostgresConnection();

        builder.ConfigureAppConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:RedisConnection"] = _redisContainer.GetConnectionString(),
                ["ConnectionStrings:DbConnection"] = _postgreSqlContainer.GetConnectionString(),
                ["InitialSeeding"] = "false",
                ["AllowScalar"] = "false"
            }!);
        });
    }

    private void EnsurePostgresConnection()
    {
        for (var i = 1; i <= 40; i++)
        {
            try
            {
                using var connection =
                    new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());

                connection.Open();

                return;
            }
            catch (Exception ex)
            {
                Thread.Sleep(250);
            }
        }

        throw new Exception("No connection established");
    }

    public override async ValueTask DisposeAsync()
    {
        await _redisContainer.StopAsync();
        await _postgreSqlContainer.StopAsync();

        await base.DisposeAsync();
    }
}