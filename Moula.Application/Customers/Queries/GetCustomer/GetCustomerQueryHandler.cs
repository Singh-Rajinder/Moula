using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moula.Application.Contracts;
using Moula.Application.Exceptions;

namespace Moula.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerVm>
    {
        private readonly IMoulaContext _context;

        public GetCustomerQueryHandler(IMoulaContext context) => _context = context;

        public async Task<CustomerVm> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers.SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (entity == null) throw new ValidationException(nameof(request.Id), "Invalid");

            return new CustomerVm
            {
                Id = entity.Id,
                Name = entity.Name,
                Balance = entity.Balance
            };
        }
    }
}
