namespace WiseReminder.IntegrationTests.Abstractions.Controllers;

public abstract class BaseControllerTests : IAsyncLifetime
{
    private readonly WebAppFactory _factory;

    protected BaseControllerTests()
    {
        _factory = new WebAppFactory();
        Client = _factory.CreateClient();
    }

    protected HttpClient Client { get; }
    protected AdminIdsFixture AdminIds { get; private set; } = null!;
    protected UserIdsFixture UserIds { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        AdminIds = await Client.SeedAdminDataAsync();
        UserIds = await Client.SeedUserWithDataAsync(AdminIds.CategoryId);
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        await _factory.DisposeAsync();
    }
}