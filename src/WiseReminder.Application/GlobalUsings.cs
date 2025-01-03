global using System.Diagnostics;
global using System.Reflection;
global using FluentResults;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Serilog;
global using WiseReminder.Application.Abstractions.Encryption;
global using WiseReminder.Application.Abstractions.JWT;
global using WiseReminder.Application.Abstractions.Logging;
global using WiseReminder.Application.Abstractions.Mapping;
global using WiseReminder.Application.Abstractions.MediatR;
global using WiseReminder.Application.Abstractions.Repository;
global using WiseReminder.Application.Authors;
global using WiseReminder.Application.Authors.AdminDeleteAuthor;
global using WiseReminder.Application.Authors.GetAuthorById;
global using WiseReminder.Application.Authors.GetAuthorByUserId;
global using WiseReminder.Application.Categories;
global using WiseReminder.Application.Categories.GetCategoryById;
global using WiseReminder.Application.Quotes;
global using WiseReminder.Application.Quotes.DeleteQuotesByAuthorId;
global using WiseReminder.Application.Quotes.DeleteQuotesByCategoryId;
global using WiseReminder.Application.Quotes.GetQuoteById;
global using WiseReminder.Application.Quotes.GetQuoteDtosByAuthorId;
global using WiseReminder.Application.Quotes.GetQuotesByAuthorId;
global using WiseReminder.Application.Quotes.GetQuotesByCategoryId;
global using WiseReminder.Application.Users;
global using WiseReminder.Application.Users.GetUserById;
global using WiseReminder.Domain.Authors;
global using WiseReminder.Domain.Categories;
global using WiseReminder.Domain.Quotes;
global using WiseReminder.Domain.Shared;
global using WiseReminder.Domain.Users;