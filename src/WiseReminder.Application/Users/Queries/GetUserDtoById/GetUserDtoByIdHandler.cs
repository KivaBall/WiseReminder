namespace WiseReminder.Application.Users.Queries.GetUserDtoById;

public sealed class GetUserDtoByIdHandler(
    ISender sender)
    : IQueryHandler<GetUserDtoByIdQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(
        GetUserDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.Id);

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        var userDto = user.Value.ToUserDto();

        return Result.Ok(userDto);
    }
}