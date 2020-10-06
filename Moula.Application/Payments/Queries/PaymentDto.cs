using System;

namespace Moula.Application.Payments.Queries
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }
}
