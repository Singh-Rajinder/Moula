using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Moula.Application.Infrastructure
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            
            var response = await next();
            
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                //TODO: Create alert for long running requests to monitor/fix issues
            }

            _logger.LogInformation($"Logging Request: {typeof(TRequest).Name} ({_timer.ElapsedMilliseconds} ms)");
            return response;

        }
    }
}
