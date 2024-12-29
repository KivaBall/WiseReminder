namespace WiseReminder.Application.Users.GetUserById;

public sealed record GetUserByIdQuery : IQuery<User>
{
    public required Guid Id { get; init; }
}