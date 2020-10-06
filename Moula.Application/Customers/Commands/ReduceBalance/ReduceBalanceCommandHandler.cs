using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moula.Application.Contracts;
using Moula.Application.Exceptions;

namespace Moula.Application.Customers.Commands.ReduceBalance
{
    public class ReduceBalanceCommandHandler : IRequestHandler<ReduceBalanceCommand, bool>
    {
        private readonly IMoulaContext _context;
        private readonly ICurrentUserService _currentUser;

        public ReduceBalanceCommandHandler(IMoulaContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(ReduceBalanceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers.SingleOrDefaultAsync(i => i.Id == _currentUser.UserId, cancellationToken);

            if (entity == null)
                throw new ValidationException("Id", "Invalid customer id.");

            if (entity.Balance < request.ReduceAmount)
                return false;

            entity.Balance -= request.ReduceAmount;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
