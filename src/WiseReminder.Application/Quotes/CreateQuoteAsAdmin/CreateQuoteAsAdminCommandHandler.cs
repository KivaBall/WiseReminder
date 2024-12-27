﻿namespace WiseReminder.Application.Quotes.CreateQuote;

public sealed class CreateQuoteAsAdminCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteAsAdminCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteAsAdminCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByIdQuery { Id = request.AuthorId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return Result.Fail(author.Errors);
        }

        if (author.Value.UserId != null)
        {
            return Result.Fail(AuthorErrors.AdminCannotChangeAuthorOfUser);
        }
        
        var categoryQuery = new GetCategoryByIdQuery { Id = request.CategoryId };

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return Result.Fail(category.Errors);
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return Result.Fail(quoteDate.Errors);
        }

        var quote = Quote.Create(text, author.Value, category.Value, quoteDate.Value);

        if (quote.IsFailed)
        {
            return Result.Fail(quote.Errors);
        }

        quoteRepository.CreateQuote(quote.Value);

        var isSaved = await unitOfWork.SaveChangesAsync();

        return isSaved.IsFailed ? Result.Fail(isSaved.Errors) : Result.Ok(quote.Value.Id);
    }
}