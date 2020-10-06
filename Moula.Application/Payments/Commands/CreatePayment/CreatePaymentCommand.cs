using System;
using MediatR;

namespace Moula.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand: IRequest<Guid>
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
