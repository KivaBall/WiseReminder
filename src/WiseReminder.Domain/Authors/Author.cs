using WiseReminder.Domain.Abstractions;
using WiseReminder.Domain.Categories;
using WiseReminder.Domain.Quotes;

namespace WiseReminder.Domain.Authors;

public sealed class Author : Entity
{
    public Author(AuthorName name, AuthorBiography biography, AuthorDateOfBirth dateOfBirth, AuthorDateOfDeath dateOfDeath)
    {
        Name = name;
        Biography = biography;
        DateOfBirth = dateOfBirth;
        DateOfDeath = dateOfDeath;
    }

    // ReSharper disable once UnusedMember.Local
    private Author()
    {

    }

    public AuthorName Name { get; internal set; }
    public AuthorBiography Biography { get; internal set; }
    public AuthorDateOfBirth DateOfBirth { get; internal set; }
    public AuthorDateOfDeath DateOfDeath { get; internal set; }
    public ICollection<Quote> Quotes { get; } = [];
}