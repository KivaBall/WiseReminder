namespace WiseReminder.Application.Users.Queries.HasUserById;

public sealed record HasUserByIdQuery(Guid Id) : IQuery<bool>;