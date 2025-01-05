namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class JsonHelper
{
    public static async Task<T?> ReadJson<T>(this HttpResponseMessage message)
    {
        return await message.Content.ReadFromJsonAsync<T>();
    }
}