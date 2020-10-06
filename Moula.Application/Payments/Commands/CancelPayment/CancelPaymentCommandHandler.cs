using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moula.Application.Contracts;
using Moula.Application.Exceptions;
using Moula.Application.Payments.Events;
using Moula.Domain.Entities;

namespace Moula.Application.Payments.Commands.CancelPayment
{
    public class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand>
    {
        private readonly IMoulaContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public CancelPaymentCommandHandler(IMoulaContext context, IMediator mediator, ICurrentUserService currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Payments
                    .SingleOrDefaultAsync(i => i.Id == request.Id && i.CustomerId == _currentUser.UserId, cancellationToken);

            if (entity == null) throw new ValidationException("Id", "Invalid payment id.");

            if (entity.Status == PaymentStatus.Processed) throw new ValidationException("Status", "Payment has been processed already.");

            if (entity.Status == PaymentStatus.Closed) return Unit.Value;

            entity.Status = PaymentStatus.Closed;
            entity.Comment = request.Reason;
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new PaymentCancelled
            {
                PaymentId = entity.Id,
                Reason = request.Reason
            }, cancellationToken);

            return Unit.Value;
        }
    }
}
