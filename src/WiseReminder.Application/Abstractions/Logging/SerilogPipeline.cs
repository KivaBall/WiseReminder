namespace WiseReminder.Application.Abstractions.Logging;

public sealed class SerilogPipeline<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandQuery
    where TResponse : ResultBase
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("SerilogPipeline | Handling request {RequestName} with payload: {@Request}",
            typeof(TRequest).Name, request);

        var stopwatch = Stopwatch.StartNew();

        var response = await next();

        stopwatch.Stop();

        if (response.IsSuccess)
        {
            logger.LogInformation(
                "SerilogPipeline | Handled request {RequestName} successfully in {ElapsedTicks} ticks. Response: {@Response}",
                typeof(TRequest).Name, stopwatch.ElapsedTicks, response);
        }
        else
        {
            logger.LogWarning(
                "SerilogPipeline | Handled request {RequestName} with errors in {ElapsedTicks} ticks. Errors: {@Errors}",
                typeof(TRequest).Name, stopwatch.ElapsedTicks, response.Errors.Select(x => x.Message).ToArray());
        }

        return response;
    }
}