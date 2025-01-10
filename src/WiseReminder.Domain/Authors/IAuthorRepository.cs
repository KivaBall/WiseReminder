namespace WiseReminder.Domain.Authors;

public interface IAuthorRepository
{
    void CreateAuthor(Author author);

    void UpdateAuthor(Author author);

    void DeleteAuthor(Author author);

    Task<Author?> GetAuthorById(Guid id, CancellationToken cancellationToken);

    Task<AuthorDetails?> GetAuthorDetailsById(Guid id, CancellationToken cancellationToken);

    Task<Author?> GetAuthorByUserId(Guid userId, CancellationToken cancellationToken);

    Task<AuthorDetails?> GetAuthorDetailsByUserId(Guid userId, CancellationToken cancellationToken);

    Task<bool> HasAuthorByUserId(Guid userId, CancellationToken cancellationToken);

    Task<ICollection<AuthorDetails>> GetAllAuthors(CancellationToken cancellationToken);

    Task<(Date? minQuoteDate, Date? maxQuoteDate)> GetMinAndMaxQuoteDatesById(Guid id,
        CancellationToken cancellationToken);
}