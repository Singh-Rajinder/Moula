using System;
using System.Linq;
using System.Threading;
using MediatR;
using Moq;
using Moula.Application.Exceptions;
using Moula.Application.Payments.Commands.CancelPayment;
using Moula.Application.UnitTests.Common;
using Moula.Domain.Entities;
using Moula.Persistence;
using NUnit.Framework;

namespace Moula.Application.UnitTests.Payments.Commands.CancelPayment
{
    public class CancelPaymentCommandTests: BaseTest
    {
        private Mock<IMediator> _mediator;
        private MoulaContext _context;

        [OneTimeSetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();
            _context = MoulaContextFactory.Create();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            MoulaContextFactory.Destroy(_context);
        }

        [Test]
        public void Handle_WhenPending_ShouldClosePayment()
        {
            var sut = new CancelPaymentCommandHandler(_context, _mediator.Object, CurrentUser);
            var request = new CancelPaymentCommand
            {
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d743f3")
            };

            var result = sut.Handle(request, CancellationToken.None).Result;

            Assert.AreEqual(Unit.Value, result);
            var record = _context.Payments.Single(i => i.Id == request.Id);
            Assert.AreEqual(PaymentStatus.Closed, record.Status);
        }

        [Test]
        public void Handle_WhenProcessed_ShouldThrowException()
        {
            var sut = new CancelPaymentCommandHandler(_context, _mediator.Object, CurrentUser);
            var request = new CancelPaymentCommand
            {
                Id = Guid.Parse("154f10e0-85be-4da7-9499-b05dbcc40b92")
            };

            Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));
            
        }

    }
}
