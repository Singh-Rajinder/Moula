using System;
using MediatR;

namespace Moula.Application.Payments.Events
{
    public class PaymentCreated: INotification
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
    }
}
