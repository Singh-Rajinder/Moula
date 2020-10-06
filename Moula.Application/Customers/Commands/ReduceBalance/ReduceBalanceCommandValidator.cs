using FluentValidation;

namespace Moula.Application.Customers.Commands.ReduceBalance
{
    public class ReduceBalanceCommandValidator: AbstractValidator<ReduceBalanceCommand>
    {
        public ReduceBalanceCommandValidator()
        {
            RuleFor(i => i.ReduceAmount).GreaterThan(0);
        }
    }
}
