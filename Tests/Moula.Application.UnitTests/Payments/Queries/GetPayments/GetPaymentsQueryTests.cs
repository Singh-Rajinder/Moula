using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Moula.Application.Customers.Queries;
using Moula.Application.Customers.Queries.GetCustomer;
using Moula.Application.Payments.Queries.GetPayments;
using Moula.Application.UnitTests.Common;
using Moula.Persistence;
using NUnit.Framework;

namespace Moula.Application.UnitTests.Payments.Queries.GetPayments
{
    public class GetPaymentsQueryTests : BaseTest
    {
        private MoulaContext _context;
        private Mock<IMediator> _mediator;

        [OneTimeSetUp]
        public void Init()
        {
            _context = MoulaContextFactory.Create();
            _mediator = new Mock<IMediator>();

            _mediator.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new CustomerVm
                {
                    Id = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d")
                }));
        }

        [OneTimeTearDown]
        public void Cleanup() => MoulaContextFactory.Destroy(_context);

        [Test]
        public void Handle_WhenCalled_ShouldReturnPaymentsForCustomer()
        {
            var sut = new GetPaymentsQueryHandler(_context, CurrentUser, _mediator.Object);

            var result = sut.Handle(new GetPaymentsQuery(), CancellationToken.None).Result;

            Assert.IsNotEmpty(result.Payments);

        }

    }
}
