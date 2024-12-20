namespace WiseReminder.IntegrationTests.Abstractions;

public sealed class WebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ICacheService>();
            services.AddSingleton<ICacheService, FakeCacheService>();

            services.RemoveAll<IMemoryCache>();
            services.AddSingleton<IMemoryCache, FakeMemoryCache>();

            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));
        });
    }
}