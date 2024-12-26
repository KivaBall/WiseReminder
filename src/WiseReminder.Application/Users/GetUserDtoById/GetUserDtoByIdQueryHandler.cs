namespace WiseReminder.Application.Users.GetUserDtoById;

public sealed class GetUserDtoByIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetUserDtoByIdQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(
        GetUserDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Id = request.Id };

        var user = await sender.Send(query, cancellationToken);

        return user.IsFailed ? Result.Fail(user.Errors) : Result.Ok(user.Value.ToUserDto());
    }
}