namespace WiseReminder.Domain.Authors;

public sealed class AuthorService : IAuthorService
{
    public Author CreateAuthor(AuthorName name, AuthorBiography biography, AuthorDateOfBirth dateOfBirth,
        AuthorDateOfDeath dateOfDeath)
    {
        return new Author(name, biography, dateOfBirth, dateOfDeath);
    }

    public Author UpdateAuthor(Author author, AuthorName name, AuthorBiography biography, AuthorDateOfBirth dateOfBirth,
        AuthorDateOfDeath dateOfDeath)
    {
        author.Name = name;
        author.Biography = biography;
        author.DateOfBirth = dateOfBirth;
        author.DateOfDeath = dateOfDeath;
        return author;
    }

    public Author DeleteAuthor(Author author)
    {
        return (Author)author.DeleteEntity();
    }
}