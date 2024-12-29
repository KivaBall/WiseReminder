﻿namespace WiseReminder.Application.Authors.CreateAuthorAsUser;

public sealed class CreateAuthorAsUserCommandHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateAuthorAsUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAuthorAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var name = new AuthorName(request.Name);

        var biography = new Biography(request.Biography);

        var birthDate = Date.Create(request.BirthDate);

        if (birthDate.IsFailed)
        {
            return birthDate.ToResult();
        }

        var query = new GetUserByIdQuery { Id = request.UserId };

        var user = await sender.Send(query, cancellationToken);

        if (user.IsFailed)
        {
            return user.ToResult();
        }

        if (user.Value.AuthorId != null)
        {
            return Result.Fail(AuthorErrors.AuthorExistsForUser);
        }

        var author = Author.Create(name, biography, birthDate.Value, null, user.Value);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        authorRepository.CreateAuthor(author.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => author.Value.Id);
    }
}