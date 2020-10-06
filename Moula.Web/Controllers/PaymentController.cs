using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moula.Application.Payments.Commands.CancelPayment;
using Moula.Application.Payments.Commands.CreatePayment;
using Moula.Application.Payments.Commands.ProcessPayment;
using Moula.Application.Payments.Queries.GetPayments;

namespace Moula.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class PaymentController : BaseController
    {
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command) => Ok(await Mediator.Send(command ?? new CreatePaymentCommand()));

        public async Task<GetPaymentsVm> Get() => await Mediator.Send(new GetPaymentsQuery());

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Cancel([FromBody] CancelPaymentCommand command) => Ok(await Mediator.Send(command));

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Process([FromBody] ProcessPaymentCommand command) => Ok(await Mediator.Send(command));

    }
}
