using System;
using MediatR;

namespace Moula.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerVm>
    {
        public Guid Id { get; set; }
    }
}
