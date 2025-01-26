global using System.Net;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using FluentAssertions;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Hosting;
global using Npgsql;
global using Testcontainers.PostgreSql;
global using Testcontainers.Redis;
global using WiseReminder.Application.Authors;
global using WiseReminder.Application.Categories;
global using WiseReminder.Application.Quotes;
global using WiseReminder.Application.Users;
global using WiseReminder.IntegrationTests.Abstractions.Controllers;
global using WiseReminder.IntegrationTests.Abstractions.Data;
global using WiseReminder.IntegrationTests.Abstractions.Fixtures;
global using WiseReminder.IntegrationTests.Abstractions.Helpers;
global using WiseReminder.WebAPI.Controllers.Authors;
global using WiseReminder.WebAPI.Controllers.Categories;
global using WiseReminder.WebAPI.Controllers.Quotes;
global using WiseReminder.WebAPI.Controllers.Users;