using WiseReminder.Domain.Quotes;

namespace WiseReminder.IntegrationTests.Controllers;

public sealed class QuotesControllerTests : BaseControllerTests
{
    [Fact]
    public async Task AdminCreateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/quotes/{id}");

        var quoteDto = await getResponse.ReadJson<QuoteDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDto!.Id.Should().Be(id);
        quoteDto.Text.Should().Be(QuoteData.DefaultText);
        quoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        quoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        quoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(Guid.NewGuid(), Guid.NewGuid());

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(Guid.NewGuid(), Guid.NewGuid());

        //Act
        var response = await Client.PostAsync("api/quotes", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidAdminQuoteRequest;

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenQuotesForAuthorOfUser_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(UserIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(Guid.NewGuid(), AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminCreateQuote_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateAdminQuoteRequest(AdminIds.AuthorId, Guid.NewGuid());

        //Act
        await Client.AdminLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes", request);

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();

        var createResponse = await Client.PostAsync("api/quotes/own", request);

        var id = await createResponse.ReadJson<Guid>();

        var getResponse = await Client.GetAsync($"api/quotes/own/{id}");

        var quoteDto = await getResponse.ReadJson<QuoteDto>();

        //Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        quoteDto!.Id.Should().Be(id);
        quoteDto.Text.Should().Be(QuoteData.DefaultText);
        quoteDto.AuthorId.Should().Be(UserIds.AuthorId);
        quoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        quoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task UserCreateQuote_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UserCreateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UserCreateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidUserQuoteRequest;

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserCreateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.CreateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.EmptyUserLoginAsync();

        var response = await Client.PostAsync("api/quotes/own", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        var preQuoteDto = await preGetResponse.ReadJson<QuoteDto>();

        var updateResponse = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        var postGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");
        
        var postQuoteDto = await postGetResponse.ReadJson<QuoteDto>();

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        preQuoteDto!.Id.Should().Be(AdminIds.QuoteId);
        preQuoteDto.Text.Should().Be(QuoteData.DefaultText);
        preQuoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        preQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        preQuoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
        
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postQuoteDto!.Id.Should().Be(AdminIds.QuoteId);
        postQuoteDto.Text.Should().Be(QuoteData.UpdatedText);
        postQuoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        postQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        postQuoteDto.QuoteDate.Should().Be(QuoteData.UpdatedQuoteDate);
    }
    
    [Fact]
    public async Task AdminUpdateQuote_WhenUser_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.EmptyUserLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task AdminUpdateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task AdminUpdateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidAdminQuoteRequest;

        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task AdminUpdateQuote_WhenQuoteBelongsToUser_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(UserIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/{UserIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task AdminUpdateQuote_WhenQuoteNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId, AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/{Guid.NewGuid()}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminUpdateQuote_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(AdminIds.AuthorId,Guid.NewGuid());

        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task AdminUpdateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateAdminQuoteRequest(Guid.NewGuid(), AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/{AdminIds.QuoteId}", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserUpdateQuote_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();

        var preGetResponse = await Client.GetAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        var preQuoteDto = await preGetResponse.ReadJson<QuoteDto>();
        
        var updateResponse = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);
        
        var postGetResponse = await Client.GetAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        var postQuoteDto = await postGetResponse.ReadJson<QuoteDto>();
        
        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        preQuoteDto!.Id.Should().Be(UserIds.QuoteId);
        preQuoteDto.Text.Should().Be(QuoteData.DefaultText);
        preQuoteDto.AuthorId.Should().Be(UserIds.AuthorId);
        preQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        preQuoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
        
        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postQuoteDto!.Id.Should().Be(UserIds.QuoteId);
        postQuoteDto.Text.Should().Be(QuoteData.UpdatedText);
        postQuoteDto.AuthorId.Should().Be(UserIds.AuthorId);
        postQuoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        postQuoteDto.QuoteDate.Should().Be(QuoteData.UpdatedQuoteDate);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenAdmin_ReturnsForbidden()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.AdminLoginAsync();

        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenInvalid_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.InvalidUserQuoteRequest;

        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenNotUserQuote_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/own/{AdminIds.QuoteId}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenQuoteNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/own/{Guid.NewGuid()}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenAuthorNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(AdminIds.CategoryId);

        //Act
        await Client.EmptyUserLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UserUpdateQuote_WhenCategoryNotExists_ReturnsBadRequest()
    {
        //Arrange
        var request = QuoteData.UpdateUserQuoteRequest(Guid.NewGuid());

        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.PutAsync($"api/quotes/own/{UserIds.QuoteId}", request);
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task AdminDeleteQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.AdminLoginAsync();
        
        var preGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");
        
        var deleteResponse = await Client.DeleteAsync($"api/quotes/{AdminIds.QuoteId}");
        
        var postGetResponse = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenUser_ReturnsForbidden()
    {
        //Act
        await Client.EmptyUserLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/quotes/{AdminIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task AdminDeleteQuote_WhenQuoteBelongsToUser_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/{UserIds.QuoteId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AdminDeleteQuote_WhenQuoteNotExists_ReturnsBadRequest()
    {
        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UserDeleteQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();
        
        var preGetResponse = await Client.GetAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        var deleteResponse = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        var postGetResponse = await Client.GetAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        //Assert
        preGetResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        postGetResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task UserDeleteQuote_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task UserDeleteQuote_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task UserDeleteQuote_WhenQuoteBelongsToAdmin_ReturnsBadRequest()
    {
        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/own/{AdminIds.QuoteId}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UserDeleteQuote_WhenQuoteNotExists_ReturnnBadRequest()
    {
        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/own/{Guid.NewGuid()}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UserDeleteQuote_WhenAuthorNotExists_ReturnnBadRequest()
    {
        //Act
        await Client.EmptyUserLoginAsync();
        
        var response = await Client.DeleteAsync($"api/quotes/own/{UserIds.QuoteId}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetQuoteById_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/{AdminIds.QuoteId}");
        
        var quoteDto = await response.ReadJson<QuoteDto>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        quoteDto!.Id.Should().Be(AdminIds.QuoteId);
        quoteDto.Text.Should().Be(QuoteData.DefaultText);
        quoteDto.AuthorId.Should().Be(AdminIds.AuthorId);
        quoteDto.CategoryId.Should().Be(AdminIds.CategoryId);
        quoteDto.QuoteDate.Should().Be(QuoteData.DefaultQuoteDate);
    }

    [Fact]
    public async Task GetQuoteById_WhenQuoteNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetQuotesByAuthorId_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/author/{AdminIds.AuthorId}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetQuotesByAuthorId_WhenAuthorNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/author/{Guid.NewGuid()}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetQuotesByCategoryId_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/category/{AdminIds.CategoryId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetQuotesByCategoryId_WhenCategoryNotExists_ReturnsNotFound()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/category/{Guid.NewGuid()}");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetRandomQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/random");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRandomQuotes_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var amount = 2;

        //Act
        var response = await Client.GetAsync($"api/quotes/random/{amount}");
        
        var quoteDtos = await response.ReadJson<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        quoteDtos.Should().HaveCount(amount);
    }

    [Fact]
    public async Task GetDailyQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/daily");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetRecentQuote_WhenAllOk_ReturnsOk()
    {
        //Act
        var response = await Client.GetAsync($"api/quotes/recent");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetRecentQuotes_WhenAllOk_ReturnsOk()
    {
        //Arrange
        var amount = 2;

        //Act
        var response = await Client.GetAsync($"api/quotes/recent/{amount}");
        
        var quoteDtos = await response.ReadJson<List<QuoteDto>>();

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        quoteDtos.Should().HaveCount(amount);
    }

    [Fact]
    public async Task GetMyOwnQuotes_WhenAllOk_ReturnsOk()
    {
        //Act
        await Client.UserWithDataLoginAsync();
        
        var response = await Client.GetAsync("api/quotes/own");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetMyOwnQuotes_WhenAdmin_ReturnsForbidden()
    {
        //Act
        await Client.AdminLoginAsync();
        
        var response = await Client.GetAsync("api/quotes/own");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task GetMyOwnQuotes_WhenUnauthorized_ReturnsUnauthorized()
    {
        //Act
        var response = await Client.GetAsync("api/quotes/own");
        
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}