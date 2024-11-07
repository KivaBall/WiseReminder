using AutoMapper;
using WiseReminder.Application.Abstractions.MediatR;
using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Authors;

namespace WiseReminder.Application.Authors.GetAllAuthors;

public sealed class GetAllAuthorsQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    : IQueryHandler<GetAllAuthorsQuery, ICollection<AuthorVm>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ICollection<AuthorVm>>> Handle(GetAllAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _authorRepository.GetAllAuthors();

        return Result<ICollection<AuthorVm>>.Success(_mapper.Map<ICollection<AuthorVm>>(categories));
    }
}