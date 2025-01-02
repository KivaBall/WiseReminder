namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class HttpHelper
{
    public static async Task<HttpResponseMessage> PostAsync<T>(
        this HttpClient client, 
        string url,
        T request)
    {
        return await client.PostAsJsonAsync(url, request);
    }

    public static async Task<HttpResponseMessage> PutAsync<T>(
        this HttpClient client,
        string url,
        T request)
    {
        return await client.PutAsJsonAsync(url, request);
    }
}