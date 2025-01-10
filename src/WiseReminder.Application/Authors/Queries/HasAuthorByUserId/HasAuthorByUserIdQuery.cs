namespace WiseReminder.Application.Authors.Queries.HasAuthorByUserId;

public sealed record HasAuthorByUserIdQuery(Guid UserId) : IQuery<bool>;