using System;
using MediatR;

namespace Moula.Application.Payments.Events
{
    public class PaymentClosed : INotification
    {
        public Guid PaymentId { get; set; }
    }
}
