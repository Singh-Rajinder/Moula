using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Moula.Application.Payments.Commands.CreatePayment;
using Moula.Web.IntegrationTests.Common;
using NUnit.Framework;

namespace Moula.Web.IntegrationTests.Controllers.Payment
{
    public class Create
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
        public async Task GivenCreatePaymentCommand_ReturnsSuccessCode()
        {
            var client = _factory.CreateClient();
            var command = new CreatePaymentCommand
            {
                Date = DateTime.Now,
                Amount = 300
            };

            var content = Helper.GetRequestContent(command);

            var response = await client.PostAsync("/payment/create", content);

            response.EnsureSuccessStatusCode();

        }

        [Test]
        public async Task Handle_WhenInvalidData_ReturnValidationException()
        {
            var client = _factory.CreateClient();
            var command = new CreatePaymentCommand
            {
                Date = DateTime.Now.AddDays(-1),
                Amount = 0
            };

            var content = Helper.GetRequestContent(command);
            
            var response = await client.PostAsync("/payment/create", content);

            var errors = await Helper.GetResponseContent<IDictionary<string, string[]>>(response);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.That(errors, Contains.Key("Amount"));
            Assert.That(errors, Contains.Key("Date"));

        }

    }
}
