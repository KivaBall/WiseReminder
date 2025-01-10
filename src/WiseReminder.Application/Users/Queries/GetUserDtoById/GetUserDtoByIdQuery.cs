namespace WiseReminder.Application.Users.Queries.GetUserDtoById;

public sealed record GetUserDtoByIdQuery(Guid Id) : IQuery<UserDto>;