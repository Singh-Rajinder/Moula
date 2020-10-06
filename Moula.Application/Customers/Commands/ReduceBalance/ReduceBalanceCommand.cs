using MediatR;

namespace Moula.Application.Customers.Commands.ReduceBalance
{
    public class ReduceBalanceCommand: IRequest<bool>
    {
        public decimal ReduceAmount { get; set; }
    }
}
