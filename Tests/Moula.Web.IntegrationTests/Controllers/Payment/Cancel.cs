using System;
using System.Threading.Tasks;
using Moula.Application.Payments.Commands.CancelPayment;
using Moula.Web.IntegrationTests.Common;
using NUnit.Framework;

namespace Moula.Web.IntegrationTests.Controllers.Payment
{
    public class Cancel
    {
        private CustomWebApplicationFactory _factory;

        [SetUp]
        public void Init()
        {
            _factory = new CustomWebApplicationFactory();
        }

        [TearDown]
        public void Cleanup() => _factory.Dispose();

        [Test]
        public async Task GivenCancelPaymentCommand_ReturnsSuccessCode()
        {
            var client = _factory.CreateClient();
            var id = "b162e88d-a3a6-4341-87da-725658d743f3";
            var command = new CancelPaymentCommand
            {
                Id = Guid.Parse(id),
                Reason = "Random Reason"
            };
            var content = Helper.GetRequestContent(command);

            Console.Write(content);
            var response = await client.PostAsync($"/payment/cancel", content);

            response.EnsureSuccessStatusCode();

        }


    }
}
