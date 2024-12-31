namespace WiseReminder.IntegrationTests.Abstractions.Helpers;

public static class AuthHelper
{
    public static async Task AdminLoginAsync(this HttpClient httpClient)
    {
        var authRequest = new AdminLoginRequest
        {
            FirstPassword = "first_secret_admin_password",
            SecondPassword = "second_secret_admin_password",
            ThirdPassword = "third_secret_admin_password"
        };

        var authResponse = await httpClient.PostAsJsonAsync("api/users/admin-login", authRequest);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await authResponse.Content.ReadAsStringAsync());
    }

    public static async Task UserLoginAsync(this HttpClient httpClient)
    {
        var authRequest = new UserLoginRequest
        {
            Login = "DefaultLogin",
            Password = "DefaultPassword"
        };

        var authResponse = await httpClient.PostAsJsonAsync("api/users/login", authRequest);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await authResponse.Content.ReadAsStringAsync());
    }

    public static void Logout(this HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}