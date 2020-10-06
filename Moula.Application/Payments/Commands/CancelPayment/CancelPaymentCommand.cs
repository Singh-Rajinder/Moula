using System;
using MediatR;

namespace Moula.Application.Payments.Commands.CancelPayment
{
    public class CancelPaymentCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Reason { get; set; }
    }
}
