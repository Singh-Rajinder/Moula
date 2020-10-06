using System;
using System.Collections.Generic;

namespace Moula.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Payments = new HashSet<Payment>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Payment> Payments { get; }

    }
}
