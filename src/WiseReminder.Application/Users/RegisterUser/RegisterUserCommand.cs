﻿namespace WiseReminder.Application.Users.RegisterUser;

public sealed record RegisterUserCommand : ICommand<Guid>
{
    public required string Username { get; init; }
    public required string Login { get; init; }
    public required string Password { get; init; }
}