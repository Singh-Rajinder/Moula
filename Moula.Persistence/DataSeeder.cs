using System;
using System.Linq;
using Moula.Application.Contracts;
using Moula.Domain.Entities;

namespace Moula.Persistence
{
    public class DataSeeder
    {

        public static void CreateData(IMoulaContext context)
        {
            if(context.Customers.Any()) return;

            var customer = new Customer
            {
                Id = Guid.Parse("ad2aad60-5363-4554-b1a7-32ae5f22942f"),
                Name = "Rajinder Singh",
                Balance = 10000
            };
            context.Customers.Add(customer);

            context.Payments.AddRange(new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("ad2aad60-5363-4554-b1a7-32ae5f22942f"),
                Date = DateTime.Now,
                Status = PaymentStatus.Closed,
                Id = Guid.Parse("debf1d88-47ac-4fe4-a0b0-ce42f72ea66e")
            }, new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("ad2aad60-5363-4554-b1a7-32ae5f22942f"),
                Date = DateTime.Now,
                Status = PaymentStatus.Pending,
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d743f3")
            }, new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("ad2aad60-5363-4554-b1a7-32ae5f22942f"),
                Date = DateTime.Now,
                Status = PaymentStatus.Pending,
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d744f3")
            }, new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("ad2aad60-5363-4554-b1a7-32ae5f22942f"),
                Date = DateTime.Now,
                Status = PaymentStatus.Processed,
                Id = Guid.Parse("154f10e0-85be-4da7-9499-b05dbcc40b92")
            });
            context.SaveChanges();
        }
    }
}
