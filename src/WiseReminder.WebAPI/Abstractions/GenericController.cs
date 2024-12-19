namespace WiseReminder.WebAPI.Abstractions;

[ApiController]
public abstract class GenericController(ISender sender) : ControllerBase
{
    protected async Task<IActionResult> ExecuteCommand(ICommand command)
    {
        var result = await sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : BadRequest(result.Errors.Select(e => e.Message).ToList());
    }

    protected async Task<IActionResult> ExecuteCommandWithEntity<TResponse>(
        ICommand<TResponse> command)
    {
        var result = await sender.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors.Select(e => e.Message).ToList());
    }

    protected async Task<IActionResult> ExecuteQuery<TResponse>(IQuery<TResponse> query)
    {
        var result = await sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errors.Select(e => e.Message).ToList());
    }
}