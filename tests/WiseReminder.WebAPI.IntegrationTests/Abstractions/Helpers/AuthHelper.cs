namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class AuthHelper
{
    public static async Task LoginAsAdminAsync(this HttpClient httpClient)
    {
        var authRequest = new LoginRequest
        {
            Login = "wise-reminder-admin-login",
            Password = "wise-reminder-admin-password"
        };

        var authResponse = await httpClient.PostAsJsonAsync("api/auth/login-as-admin", authRequest);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await authResponse.Content.ReadAsStringAsync());
    }
}