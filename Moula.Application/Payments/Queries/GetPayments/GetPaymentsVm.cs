using System.Collections.Generic;

namespace Moula.Application.Payments.Queries.GetPayments
{
    public class GetPaymentsVm
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public List<PaymentDto> Payments { get; }

        public GetPaymentsVm()
        {
            Payments = new List<PaymentDto>();
        }
    }
}
