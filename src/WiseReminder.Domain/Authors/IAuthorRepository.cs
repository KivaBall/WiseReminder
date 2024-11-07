namespace WiseReminder.Domain.Authors;

public interface IAuthorRepository
{
    void CreateAuthor(Author author);
    void UpdateAuthor(Author author);
    Task DeleteAuthor(Author author);
    Task<Author?> GetAuthorById(Guid id);
    Task<ICollection<Author>> GetAllAuthors();
}