namespace WiseReminder.Domain.Authors;

public interface IAuthorService
{
    Author CreateAuthor(AuthorName name, AuthorBiography biography, AuthorDateOfBirth dateOfBirth,
        AuthorDateOfDeath dateOfDeath);

    Author UpdateAuthor(Author author, AuthorName name, AuthorBiography biography, AuthorDateOfBirth dateOfBirth,
        AuthorDateOfDeath dateOfDeath);

    Author DeleteAuthor(Author author);
}