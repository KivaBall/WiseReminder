namespace WiseReminder.Infrastructure.Repositories;

public sealed class AuthorRepository(
    AppDbContext context,
    IQuoteRepository quoteRepository,
    IMemoryCache memoryCache) : IAuthorRepository
{
    public void CreateAuthor(Author author)
    {
        context.Authors.Add(author);
    }

    public void UpdateAuthor(Author author)
    {
        context.Authors.Update(author);
    }

    public async Task DeleteAuthor(Author author)
    {
        author.Delete();

        var quotes = await quoteRepository.GetQuotesByAuthorId(author.Id);

        foreach (var quote in quotes)
        {
            quoteRepository.DeleteQuote(quote);
        }

        context.Authors.Update(author);
    }

    public async Task<Author?> GetAuthorById(Guid id)
    {
        var key = $"author-{id}";

        return await memoryCache.GetOrCreateAsync(key, async factory =>
        {
            factory.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return await context.Authors.FirstOrDefaultAsync(author => author.Id == id);
        });
    }

    public async Task<ICollection<Author>> GetAllAuthors()
    {
        var key = "author-all";

        return await memoryCache.GetOrCreateAsync(key, async factory =>
        {
            factory.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            return await context.Authors.ToListAsync();
        }) ?? [];
    }
}