namespace WiseReminder.Domain.Authors;

public interface IAuthorRepository
{
    void CreateAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(Author author);
    Task<Author?> GetAuthorById(Guid id);
    Task<Author?> GetAuthorByUserId(Guid userId);
    Task<ICollection<Author>> GetAllAuthors();
}