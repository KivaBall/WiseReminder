namespace WiseReminder.Application.Users.GetUserDtoById;

public sealed record GetUserDtoByIdQuery(Guid Id) : IQuery<UserDto>;