using System.Threading.Tasks;
using Moula.Application.Payments.Queries.GetPayments;
using Moula.Web.IntegrationTests.Common;
using NUnit.Framework;

namespace Moula.Web.IntegrationTests.Controllers.Payment
{
    public class Get
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
        public async Task ReturnsPaymentsViewModel()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/payment/get");
            response.EnsureSuccessStatusCode();

            var vm = await Helper.GetResponseContent<GetPaymentsVm>(response);
            
            Assert.IsNotEmpty(vm.Payments);
        }

    }
}
