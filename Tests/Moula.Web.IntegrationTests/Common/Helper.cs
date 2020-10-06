using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moula.Domain.Entities;
using Moula.Persistence;
using Newtonsoft.Json;

namespace Moula.Web.IntegrationTests.Common
{
    public class Helper
    {
        public static StringContent GetRequestContent(object obj) => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public static void InitDatabaseForTest(MoulaContext context)
        {
            context.Customers.Add(new Customer
            {
                Balance = 50000,
                Id = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d"),
                Name = "John Doe"
            });

            context.Payments.AddRange(new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d"),
                Date = DateTime.Now,
                Status = PaymentStatus.Closed,
                Id = Guid.Parse("debf1d88-47ac-4fe4-a0b0-ce42f72ea66e")
            }, new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d"),
                Date = DateTime.Now,
                Status = PaymentStatus.Pending,
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d743f3")
            }, new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d"),
                Date = DateTime.Now,
                Status = PaymentStatus.Processed,
                Id = Guid.Parse("154f10e0-85be-4da7-9499-b05dbcc40b92")
            });

            context.SaveChanges();
        }
    }
}
