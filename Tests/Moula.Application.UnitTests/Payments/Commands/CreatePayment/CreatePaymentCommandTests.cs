using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using MediatR;
using Moq;
using Moula.Application.Customers.Queries;
using Moula.Application.Customers.Queries.GetCustomer;
using Moula.Application.Payments.Commands.CreatePayment;
using Moula.Application.UnitTests.Common;
using Moula.Domain.Entities;
using Moula.Persistence;
using NUnit.Framework;

namespace Moula.Application.UnitTests.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandTests : BaseTest
    {
        private Mock<IMediator> _mediator;
        private MoulaContext _context;
        
        [OneTimeSetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();
            _mediator
                .Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new CustomerVm
                {
                    Balance = 50000
                }));
            _context = MoulaContextFactory.Create();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            MoulaContextFactory.Destroy(_context);
        }

        [Test]
        public void Handle_GivenValidRequest_ShouldCreatePendingPayment()
        {
           
            var sut = new CreatePaymentCommandHandler(_context, _mediator.Object, CurrentUser);
            var request = new CreatePaymentCommand
            {
                Amount = 200,
                Date = DateTime.Now
            };

            var result = sut.Handle(request, CancellationToken.None).Result;

            Assert.AreNotEqual(Guid.Empty, result);
            var record = _context.Payments.SingleOrDefault(i => i.Id == result);
            Assert.IsNotNull(record);
            Assert.AreEqual(PaymentStatus.Pending, record.Status);
        }


        [Test]
        public void Handle_GivenMoreThanBalanceAmount_ShouldCreateClosedPayment()
        {

            var sut = new CreatePaymentCommandHandler(_context, _mediator.Object, CurrentUser);
            var request = new CreatePaymentCommand
            {
                Amount = 100000,
                Date = DateTime.Now
            };

            var result = sut.Handle(request, CancellationToken.None).Result;

            Assert.AreNotEqual(Guid.Empty, result);
            var record = _context.Payments.SingleOrDefault(i => i.Id == result);
            Assert.IsNotNull(record);
            Assert.AreEqual(PaymentStatus.Closed, record.Status);
        }

        [Test]
        public void Handle_GivenInvalidData_ShouldFailValidation()
        {
            var validator = new CreatePaymentCommandValidator();

            validator.ShouldHaveValidationErrorFor(i => i.Amount, 0);
            validator.ShouldHaveValidationErrorFor(i => i.Date, DateTime.Now.AddDays(-1));
        }

    }
}
