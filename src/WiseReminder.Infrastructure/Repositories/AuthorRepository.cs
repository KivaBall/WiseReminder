namespace WiseReminder.Infrastructure.Repositories;

public sealed class AuthorRepository(
    AppDbContext context)
    : IAuthorRepository
{
    public void CreateAuthor(Author author)
    {
        context.Authors.Add(author);
    }

    public void UpdateAuthor(Author author)
    {
        context.Authors.Update(author);
    }

    public void DeleteAuthor(Author author)
    {
        author.Delete();

        context.Authors.Update(author);
    }

    public async Task<Author?> GetAuthorById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Authors
            .FirstOrDefaultAsync(author => author.Id == id, cancellationToken);
    }

    public async Task<AuthorDetails?> GetAuthorDetailsById(Guid id,
        CancellationToken cancellationToken)
    {
        return await context.Authors
            .Where(author => author.Id == id)
            .ConvertToAuthorDetails(context)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Author?> GetAuthorByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Authors
            .FirstOrDefaultAsync(author => author.UserId == userId, cancellationToken);
    }

    public async Task<AuthorDetails?> GetAuthorDetailsByUserId(Guid userId,
        CancellationToken cancellationToken)
    {
        return await context.Authors
            .Where(author => author.UserId == userId)
            .ConvertToAuthorDetails(context)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> HasAuthorByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Authors
            .AnyAsync(author => author.UserId == userId, cancellationToken);
    }

    public async Task<ICollection<AuthorDetails>> GetAllAuthors(CancellationToken cancellationToken)
    {
        return await context.Authors
            .ConvertToAuthorDetails(context)
            .ToListAsync(cancellationToken);
    }

    public async Task<(Date? minQuoteDate, Date? maxQuoteDate)> GetMinAndMaxQuoteDatesById(Guid id,
        CancellationToken cancellationToken)
    {
        var result = await context.Quotes
            .Where(q => q.AuthorId == id)
            .GroupBy(q => q.AuthorId)
            .Select(g => new
            {
                MinQuoteDate = g.Min(q => q.QuoteDate),
                MaxQuoteDate = g.Max(q => q.QuoteDate)
            })
            .FirstOrDefaultAsync(cancellationToken);

        return (result?.MinQuoteDate, result?.MaxQuoteDate);
    }
}