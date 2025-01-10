namespace WiseReminder.Application.Abstractions.Logging;

public sealed class SerilogPipeline<TRequest, TResponse>(
    ILogger logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommon
    where TResponse : ResultBase
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.Information(
            "Handling request {RequestName} with payload: {@Request}",
            typeof(TRequest).Name,
            request);

        var stopwatch = Stopwatch.StartNew();

        var response = await next();

        stopwatch.Stop();

        if (response.IsSuccess)
        {
            logger.Information(
                "Handled request {RequestName} successfully in {ElapsedTicks} ticks. Response: {@Response}",
                typeof(TRequest).Name,
                stopwatch.ElapsedTicks,
                response);
        }
        else
        {
            logger.Warning(
                "Handled request {RequestName} with errors in {ElapsedTicks} ticks. Errors: {@Errors}",
                typeof(TRequest).Name,
                stopwatch.ElapsedTicks,
                response.Errors.Select(x => x.Message).ToArray());
        }

        return response;
    }
}