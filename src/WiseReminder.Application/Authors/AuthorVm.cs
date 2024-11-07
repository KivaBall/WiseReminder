namespace WiseReminder.Application.Authors;

public sealed record AuthorVm(Guid Id, string Name, string Biography, DateOnly DateOfBirth, DateOnly DateOfDeath);