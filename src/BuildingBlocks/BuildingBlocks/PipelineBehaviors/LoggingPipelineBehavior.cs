using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.PipelineBehaviors;
public class LoggingPipelineBehavior<TRequest, TResponse>(ILogger<LoggingPipelineBehavior<TRequest,TResponse>> logger): IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var nameOfRequest = GetRequestName(request);
        logger.LogInformation($"Startin request : {nameOfRequest}...");
        var watch = Stopwatch.StartNew();

        var result = await next();

        watch.Stop();
        var timeExcution = watch.ElapsedMilliseconds;
        logger.LogInformation($"End request : {nameOfRequest} in {timeExcution}ms");
        return result;
    }

    private string GetRequestName(TRequest request)
    {
        return request.GetType().Name;
    }
}
