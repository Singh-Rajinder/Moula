using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moula.Application.Contracts;
using Moula.Application.Customers.Queries.GetCustomer;
using Moula.Application.Exceptions;
using Moula.Application.Payments.Events;
using Moula.Domain.Entities;

namespace Moula.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IMoulaContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public CreatePaymentCommandHandler(IMoulaContext context, IMediator mediator, ICurrentUserService currentUser)
        {
            _context = context;
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var customer = await _mediator.Send(new GetCustomerQuery { Id = _currentUser.UserId }, cancellationToken);

            if (customer == null) throw new ValidationException("CustomerId", "Invalid customer id.");

            var hasSufficientBalance = customer.Balance >= request.Amount;

            var entity = new Payment
            {
                Amount = request.Amount,
                Comment = hasSufficientBalance ? "" : "Not enough funds",
                CustomerId = _currentUser.UserId,
                Date = request.Date,
                Status = hasSufficientBalance ? PaymentStatus.Pending : PaymentStatus.Closed,
            };

            await _context.Payments.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new PaymentCreated {Amount = request.Amount, PaymentId = entity.Id}, cancellationToken);

            return entity.Id;
        }
    }
}
