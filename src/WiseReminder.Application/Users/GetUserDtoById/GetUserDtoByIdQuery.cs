namespace WiseReminder.Application.Users.GetUserDtoById;

public sealed record GetUserDtoByIdQuery : IQuery<UserDto>
{
    public required Guid Id { get; init; }
}