﻿namespace WiseReminder.Application.Quotes.CreateQuoteAsUser;

public sealed class CreateQuoteAsUserCommandHandler(
    IQuoteRepository quoteRepository,
    IUnitOfWork unitOfWork,
    ISender sender)
    : ICommandHandler<CreateQuoteAsUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateQuoteAsUserCommand request,
        CancellationToken cancellationToken)
    {
        var authorQuery = new GetAuthorByUserIdQuery { Id = request.UserId };

        var author = await sender.Send(authorQuery, cancellationToken);

        if (author.IsFailed)
        {
            return author.ToResult();
        }

        var categoryQuery = new GetCategoryByIdQuery { Id = request.CategoryId };

        var category = await sender.Send(categoryQuery, cancellationToken);

        if (category.IsFailed)
        {
            return category.ToResult();
        }

        var text = new Text(request.Text);

        var quoteDate = Date.Create(request.QuoteDate);

        if (quoteDate.IsFailed)
        {
            return quoteDate.ToResult();
        }

        var quote = Quote.Create(text, author.Value, category.Value, quoteDate.Value);

        if (quote.IsFailed)
        {
            return quote.ToResult();
        }

        quoteRepository.CreateQuote(quote.Value);

        return await unitOfWork.SaveChangesAsyncWithResult(() => quote.Value.Id);
    }
}