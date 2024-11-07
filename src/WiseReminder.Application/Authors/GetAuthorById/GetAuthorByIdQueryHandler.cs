using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Authors.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    : IQueryHandler<GetAuthorByIdQuery, AuthorVm>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<AuthorVm>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAuthorById(request.Id);

        if (author == null) return Result<AuthorVm>.Failure<AuthorVm>(null, AuthorErrors.AuthorNotFound);

        return Result<AuthorVm>.Success(_mapper.Map<AuthorVm>(author));
    }
}