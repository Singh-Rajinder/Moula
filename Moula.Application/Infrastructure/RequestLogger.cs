using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Moula.Application.Infrastructure
{
    /// <summary>
    /// Log every request for auditing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestLogger<T> : IRequestPreProcessor<T>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<T> logger) => _logger = logger;

        public Task Process(T request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request: {Name}, {@Request}", typeof(T).Name, request);
            return Task.CompletedTask;
        }
    }
}
