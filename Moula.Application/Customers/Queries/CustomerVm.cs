using System;

namespace Moula.Application.Customers.Queries
{
    public class CustomerVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
