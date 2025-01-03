﻿namespace WiseReminder.Application.Authors.GetAuthorDtos;

public sealed class GetAuthorDtosHandler(
    IAuthorRepository authorRepository)
    : IQueryHandler<GetAuthorDtosQuery, ICollection<AuthorDto>>
{
    public async Task<Result<ICollection<AuthorDto>>> Handle(
        GetAuthorDtosQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await authorRepository.GetAllAuthors();

        ICollection<AuthorDto> authorDtos = authors
            .Select(a => a.ToAuthorDto())
            .ToList();

        return Result.Ok(authorDtos);
    }
}