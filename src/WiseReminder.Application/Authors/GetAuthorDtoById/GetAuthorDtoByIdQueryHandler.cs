﻿namespace WiseReminder.Application.Authors.GetAuthorDtoById;

public sealed class GetAuthorDtoByIdQueryHandler(
    ISender sender)
    : IQueryHandler<GetAuthorDtoByIdQuery, AuthorDto>
{
    public async Task<Result<AuthorDto>> Handle(
        GetAuthorDtoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery { Id = request.Id };

        var author = await sender.Send(query, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var authorDto = author.Value.ToAuthorDto();

        return Result.Ok(authorDto);
    }
}