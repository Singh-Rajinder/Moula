using System;

namespace Moula.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public PaymentStatus Status { get; set; }

        public Customer Customer { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Processed,
        Closed
    }
}
