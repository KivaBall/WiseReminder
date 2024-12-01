﻿namespace WiseReminder.Application.Abstractions.Mapping;

public static class QuoteToDtoMapper
{
    public static QuoteDto ToQuoteDto(this Quote quote)
    {
        return new QuoteDto
        {
            Id = quote.Id,
            Text = quote.Text.Value,
            AuthorId = quote.AuthorId,
            CategoryId = quote.CategoryId,
            QuoteDate = quote.QuoteDate.Value
        };
    }
}