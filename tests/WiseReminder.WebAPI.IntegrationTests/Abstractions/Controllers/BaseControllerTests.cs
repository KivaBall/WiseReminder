namespace WiseReminder.IntegrationTests.Abstractions.Controllers;

public abstract class BaseControllerTests : IDisposable
{
    protected BaseControllerTests()
    {
        Client = new WebAppFactory().CreateClient();
        Ids = Client.SeedDataAsync().Result;
    }

    protected HttpClient Client { get; }
    protected IdsFixture Ids { get; }

    public void Dispose()
    {
        Client.Dispose();
    }
}