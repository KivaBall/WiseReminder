namespace WiseReminder.Application.Abstractions.Mapping;

public static class UserToDtoMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username.Value,
            Login = user.Login.Value,
            Subscription = user.Subscription,
            AuthorId = user.AuthorId
        };
    }
}