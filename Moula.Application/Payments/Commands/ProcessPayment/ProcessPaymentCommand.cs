using System;
using MediatR;

namespace Moula.Application.Payments.Commands.ProcessPayment
{
    public class ProcessPaymentCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
