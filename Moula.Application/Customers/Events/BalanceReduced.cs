using System;
using MediatR;

namespace Moula.Application.Customers.Events
{
    public class BalanceReduced : INotification
    {
        public Guid PaymentId { get; set; }
        public decimal AmountReduced { get; set; }
    }
}
