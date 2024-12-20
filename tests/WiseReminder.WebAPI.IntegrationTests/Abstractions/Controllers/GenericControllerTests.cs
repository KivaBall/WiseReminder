namespace WiseReminder.IntegrationTests.Abstractions.Controllers;

public abstract class GenericControllerTests : IDisposable
{
    protected readonly HttpClient Client = new WebAppFactory().CreateClient();

    public void Dispose()
    {
        Client.Dispose();
    }
}