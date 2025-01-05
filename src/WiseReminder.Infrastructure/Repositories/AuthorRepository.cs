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

    public async Task<Author?> GetAuthorById(Guid id)
    {
        return await context.Authors
            .FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<Author?> GetAuthorByUserId(Guid userId)
    {
        return await context.Authors
            .FirstOrDefaultAsync(author => author.UserId == userId);
    }

    public async Task<ICollection<Author>> GetAllAuthors()
    {
        return await context.Authors
            .ToListAsync();
    }
}