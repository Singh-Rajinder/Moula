using System;
using System.Threading;
using Moula.Application.Customers.Queries.GetCustomer;
using Moula.Application.Exceptions;
using Moula.Application.UnitTests.Common;
using Moula.Persistence;
using NUnit.Framework;

namespace Moula.Application.UnitTests.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryTests
    {
        private MoulaContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            _context = MoulaContextFactory.Create();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            MoulaContextFactory.Destroy(_context);
        }

        [Test]
        public void Handle_WhenValidRequest_ShouldReturnCustomer()
        {
            var sut = new GetCustomerQueryHandler(_context);
            var request = new GetCustomerQuery { Id = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d") };

            var result = sut.Handle(request, CancellationToken.None).Result;

            Assert.AreEqual(request.Id, result.Id);

        }

        [Test]
        public void Handle_WhenInValidRequest_ShouldThrowException()
        {
            var sut = new GetCustomerQueryHandler(_context);
            var request = new GetCustomerQuery { Id = Guid.NewGuid() };

            Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));

        }
    }
}
