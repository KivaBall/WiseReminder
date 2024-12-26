namespace WiseReminder.Domain.Authors;

public interface IAuthorRepository
{
    void CreateAuthor(Author author);
    void UpdateAuthor(Author author);
    Task DeleteAuthor(Author author); //TODO: deletion must not be Task!
    Task<Author?> GetAuthorById(Guid id);
    Task<ICollection<Author>> GetAllAuthors();
}