using System;
using System.Threading.Tasks;
using Moula.Application.Payments.Commands.ProcessPayment;
using Moula.Web.IntegrationTests.Common;
using NUnit.Framework;

namespace Moula.Web.IntegrationTests.Controllers.Payment
{
    public class Process
    {
        private CustomWebApplicationFactory _factory;

        [OneTimeSetUp]
        public void Init()
        {
            _factory = new CustomWebApplicationFactory();
        }

        [OneTimeTearDown]
        public void Cleanup() => _factory.Dispose();

        [Test]
        public async Task GivenProcessPaymentCommand_ReturnsSuccessCode()
        {
            var client = _factory.CreateClient();
            var id = "154f10e0-85be-4da7-9499-b05dbcc40b92";
            var content = Helper.GetRequestContent(new ProcessPaymentCommand { Id = Guid.Parse(id) });


            var response = await client.PostAsync($"/payment/process", content);

            response.EnsureSuccessStatusCode();

        }

       
    }
}
