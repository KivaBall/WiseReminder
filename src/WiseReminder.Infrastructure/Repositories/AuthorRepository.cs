namespace WiseReminder.Infrastructure.Repositories;

public sealed class AuthorRepository(
    AppDbContext context,
    IAuthorService authorService,
    IQuoteRepository quoteRepository) : IAuthorRepository
{
    private readonly IAuthorService _authorService = authorService;
    private readonly AppDbContext _context = context;
    private readonly IQuoteRepository _quoteRepository = quoteRepository;

    public void CreateAuthor(Author author)
    {
        _context.Authors.Add(author);
    }

    public void UpdateAuthor(Author author)
    {
        _context.Authors.Update(author);
    }

    public async Task DeleteAuthor(Author author)
    {
        _authorService.DeleteAuthor(author);

        var quotes = await _quoteRepository.GetQuotesByAuthorId(author.Id) ??
                     throw new InvalidOperationException("AuthorId was not found");
        foreach (var quote in quotes)
        {
            _quoteRepository.DeleteQuote(quote);
        }

        _context.Authors.Update(author);
    }

    public async Task<Author?> GetAuthorById(Guid id)
    {
        return await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<ICollection<Author>> GetAllAuthors()
    {
        return await _context.Authors.ToListAsync();
    }
}