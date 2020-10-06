using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moula.Application.Contracts;

namespace Moula.Application.Infrastructure
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMoulaContext _context;

        public TransactionBehavior(IMoulaContext context) => _context = context;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                await _context.BeginTransactionAsync(cancellationToken);
                var response = await next();
                _context.CommitTransaction();
                return response;
            }
            catch (Exception)
            {
                _context.RollbackTransaction();
                throw;
            }
        }
    }
}
