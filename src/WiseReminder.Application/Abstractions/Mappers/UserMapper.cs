namespace WiseReminder.Application.Abstractions.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username.Value,
            Login = user.Login.Value,
            Subscription = user.Subscription.ToString()
        };
    }
}