using System;
using MediatR;

namespace Moula.Application.Payments.Events
{
    public class PaymentCancelled: INotification
    {
        public Guid PaymentId { get; set; }
        public string Reason { get; set; }
    }
}
