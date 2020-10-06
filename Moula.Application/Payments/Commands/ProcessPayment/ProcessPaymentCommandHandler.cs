using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moula.Application.Contracts;
using Moula.Application.Customers.Commands.ReduceBalance;
using Moula.Application.Exceptions;
using Moula.Application.Payments.Events;
using Moula.Domain.Entities;

namespace Moula.Application.Payments.Commands.ProcessPayment
{
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand>
    {
        private readonly IMoulaContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public ProcessPaymentCommandHandler(IMoulaContext context, IMediator mediator, ICurrentUserService currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Payments
                .SingleOrDefaultAsync(i => i.Id == request.Id && i.CustomerId == _currentUser.UserId, cancellationToken);

            if (entity == null) throw new ValidationException("Id", "Invalid payment id.");

            if (entity.Status == PaymentStatus.Closed) throw new ValidationException("Status", "Can't process the closed payment.");

            if (entity.Status == PaymentStatus.Processed) return Unit.Value;

            var balanceReduced = await _mediator.Send(new ReduceBalanceCommand { ReduceAmount = entity.Amount }, cancellationToken);

            if (!balanceReduced)
            {
                entity.Status = PaymentStatus.Closed;
                entity.Comment = "Not enough funds";
            }
            else
            {
                entity.Status = PaymentStatus.Processed;
            }

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new PaymentProcessed { PaymentId = entity.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
