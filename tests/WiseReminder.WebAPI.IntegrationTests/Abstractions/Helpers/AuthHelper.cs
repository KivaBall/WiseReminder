namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class AuthHelper
{
    public static async Task AdminLoginAsync(this HttpClient httpClient)
    {
        var request = UserData.DefaultAdminLoginRequest;

        var response = await httpClient.PostAsync("api/users/admin-login", request);

        var token = await response.Content.ReadAsStringAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public static async Task EmptyUserLoginAsync(this HttpClient httpClient)
    {
        var request = UserData.EmptyUserLoginRequest;

        var response = await httpClient.PostAsync("api/users/login", request);

        var token = await response.Content.ReadAsStringAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public static async Task UserWithDataLoginAsync(this HttpClient httpClient)
    {
        var request = UserData.UserWithDataLoginRequest;

        var response = await httpClient.PostAsync("api/users/login", request);

        var token = await response.Content.ReadAsStringAsync();

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public static void Logout(this HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}