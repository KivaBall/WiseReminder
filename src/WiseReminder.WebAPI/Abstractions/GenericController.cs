namespace WiseReminder.WebAPI.Abstractions;

[ApiController]
public abstract class GenericController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    protected async Task<IActionResult> ExecuteCommand(ICommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result);
    }

    protected async Task<IActionResult> ExecuteCommandWithEntity<TResponse>(ICommand<TResponse> command)
        where TResponse : class
    {
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result.Entity) : BadRequest(result);
    }

    protected async Task<IActionResult> ExecuteQuery<TResponse>(IQuery<TResponse> query)
        where TResponse : class
    {
        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Entity) : NotFound(result);
    }
}