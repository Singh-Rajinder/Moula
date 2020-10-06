using System;
using MediatR;

namespace Moula.Application.Payments.Events
{
    public class PaymentProcessed: INotification
    {
        public Guid PaymentId { get; set; }

    }
}
