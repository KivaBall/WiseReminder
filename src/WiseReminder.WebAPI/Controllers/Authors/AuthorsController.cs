namespace WiseReminder.WebAPI.Controllers.Authors;

[Route("api/authors")]
public sealed class AuthorsController(ISender sender) : GenericController(sender)
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAuthor(BaseAuthorRequest request)
    {
        var command = request.ToCreateAuthorCommand();
        return await ExecuteCommand(command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(Guid id)
    {
        var query = new GetAuthorDtoByIdQuery(id);
        return await ExecuteQuery(query);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var query = new GetAllAuthorsQuery();
        return await ExecuteQuery(query);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest request)
    {
        var command = request.ToUpdateAuthorCommand();
        return await ExecuteCommand(command);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var command = new DeleteAuthorCommand(id);
        return await ExecuteCommand(command);
    }
}