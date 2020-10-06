using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Moula.Application.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator: AbstractValidator<CreatePaymentCommand>

    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(i => i.Amount).GreaterThan(0).NotEqual(default(decimal));
            RuleFor(i => i.Date).GreaterThanOrEqualTo(DateTime.Now.Date);
        }
    }
}
