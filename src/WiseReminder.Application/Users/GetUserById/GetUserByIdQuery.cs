namespace WiseReminder.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<User>;