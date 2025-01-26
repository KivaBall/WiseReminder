namespace WiseReminder.Infrastructure.Repositories;

public static class RepositoryExtensions
{
    public static IQueryable<AuthorDetails> ConvertToAuthorDetails(
        this IQueryable<Author> query,
        AppDbContext context)
    {
        return query
            .Select(a => new AuthorDetails
            {
                Author = a,
                Quotes = context.Quotes
                    .Count(quote => quote.AuthorId == a.Id),
                MinQuoteDate = context.Quotes
                    .Where(q => q.AuthorId == a.Id)
                    .Min(q => q.QuoteDate),
                MaxQuoteDate = context.Quotes
                    .Where(q => q.AuthorId == a.Id)
                    .Max(q => q.QuoteDate)
            });
    }

    public static IQueryable<CategoryDetails> ConvertToCategoryDetails(
        this IQueryable<Category> query,
        AppDbContext context)
    {
        return query
            .Select(c => new CategoryDetails
            {
                Category = c,
                Quotes = context.Quotes
                    .Count(quote => quote.CategoryId == c.Id)
            });
    }

    public static IQueryable<QuoteDetails> ConvertToQuoteDetails(
        this IQueryable<Quote> query,
        AppDbContext context)
    {
        return query
            .Select(q => new QuoteDetails
            {
                Quote = q,
                Likes = context.Reactions
                    .Where(r => r.QuoteId == q.Id)
                    .Count(r => r.IsLike == new IsLike(true)),
                Dislikes = context.Reactions
                    .Where(r => r.QuoteId == q.Id)
                    .Count(r => r.IsLike == new IsLike(false))
            });
    }

    public static IQueryable<Quote> GetTopQuote(
        this IQueryable<Quote> query,
        AppDbContext context,
        TimeSpan period)
    {
        var cutoffTime = DateTime.UtcNow - period;

        return query
            .Where(q => q.AddedAt >= cutoffTime)
            .Select(q => new
            {
                Quote = q,
                LikeCount = context.Reactions
                    .Count(r => r.IsLike == new IsLike(true) && r.QuoteId == q.Id)
            })
            .OrderByDescending(t => t.LikeCount)
            .Select(t => t.Quote);
    }
}