using System;
using Microsoft.EntityFrameworkCore;
using Moula.Domain.Entities;
using Moula.Persistence;

namespace Moula.Application.UnitTests.Common
{
    public class MoulaContextFactory
    {
        public static MoulaContext Create()
        {
            var options = new DbContextOptionsBuilder<MoulaContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
            var context = new MoulaContext(options);
            context.Database.EnsureCreated();

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
                Status = PaymentStatus.Pending,
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d744f3")
            }, new Payment
            {
                Amount = 100,
                CustomerId = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d"),
                Date = DateTime.Now,
                Status = PaymentStatus.Processed,
                Id = Guid.Parse("154f10e0-85be-4da7-9499-b05dbcc40b92")
            });

            context.SaveChanges();
            return context;
        }

        public static void Destroy(MoulaContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
