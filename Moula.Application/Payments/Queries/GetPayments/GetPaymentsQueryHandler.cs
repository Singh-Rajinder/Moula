using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moula.Application.Contracts;
using Moula.Application.Customers.Queries.GetCustomer;
using Moula.Domain.Entities;

namespace Moula.Application.Payments.Queries.GetPayments
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, GetPaymentsVm>
    {
        private readonly IMoulaContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMediator _mediator;

        public GetPaymentsQueryHandler(IMoulaContext context, ICurrentUserService currentUser, IMediator mediator)
        {
            _context = context;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public async Task<GetPaymentsVm> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetPaymentsVm();
            var customer = await _mediator.Send(new GetCustomerQuery { Id = _currentUser.UserId }, cancellationToken);
            if (customer == null) return response;

            response.Name = customer.Name;
            response.Balance = customer.Balance;

            var entities = _context.Payments.Where(i => i.CustomerId == _currentUser.UserId)
                .OrderByDescending(i => i.Date).ToList();

            entities.ForEach(payment => response.Payments.Add(
                new PaymentDto
                {
                    Id = payment.Id,
                    Amount = payment.Amount,
                    Comment = payment.Comment ?? "",
                    Date = payment.Date,
                    Status = Enum.GetName(typeof(PaymentStatus), payment.Status)
                }));

            return response;
        }
    }
}
