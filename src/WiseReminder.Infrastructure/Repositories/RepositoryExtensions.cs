namespace WiseReminder.Infrastructure.Repositories;

public static class RepositoryExtensions
{
    public static IQueryable<AuthorDetails> ConvertToAuthorDetails(
        this IQueryable<Author> query,
        AppDbContext context)
    {
        return query
            .GroupJoin(
                context.Quotes,
                a => a.Id,
                q => q.AuthorId,
                (a, q) => new { a, q })
            .Select(t => new AuthorDetails
            {
                Author = t.a,
                Quotes = t.q.Count(),
                MinQuoteDate = t.q.Min(q => q.QuoteDate),
                MaxQuoteDate = t.q.Max(q => q.QuoteDate)
            });
    }

    public static IQueryable<CategoryDetails> ConvertToCategoryDetails(
        this IQueryable<Category> query,
        AppDbContext context)
    {
        return query
            .Select(category => new CategoryDetails
            {
                Category = category,
                Quotes = context.Quotes.Count(quote => quote.CategoryId == category.Id)
            });
    }

    public static IQueryable<QuoteDetails> ConvertToQuoteDetails(
        this IQueryable<Quote> query,
        AppDbContext context)
    {
        return query
            .GroupJoin(
                context.Reactions,
                q => q.Id,
                r => r.QuoteId,
                (q, e) => new { q, e })
            .Select(t => new QuoteDetails
            {
                Quote = t.q,
                Likes = t.e.Count(r => r.IsLike == new IsLike(true)),
                Dislikes = t.e.Count(r => r.IsLike == new IsLike(false))
            });
    }

    public static IQueryable<Quote> GetTopQuote(
        this IQueryable<Quote> query,
        AppDbContext context,
        TimeSpan period)
    {
        return context.Quotes
            .Where(q => DateTime.UtcNow - q.AddedAt < period)
            .GroupJoin(
                context.Reactions.Where(r => r.IsLike.Value),
                q => q.Id,
                r => r.QuoteId,
                (quote, reactions) => new
                {
                    Quote = quote,
                    LikeCount = reactions.Count()
                })
            .OrderByDescending(t => t.LikeCount)
            .Select(t => t.Quote);
    }
}