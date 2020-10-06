using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Moula.Application.Customers.Commands.ReduceBalance;
using Moula.Application.Exceptions;
using Moula.Application.Payments.Commands.ProcessPayment;
using Moula.Application.UnitTests.Common;
using Moula.Domain.Entities;
using Moula.Persistence;
using NUnit.Framework;

namespace Moula.Application.UnitTests.Payments.Commands.ProcessPayment
{
    public class ProcessPaymentCommandTests: BaseTest
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
        public void Handle_WhenPendingWithSufficientBalance_ShouldSetToProcessed()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ReduceBalanceCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));
            var sut = new ProcessPaymentCommandHandler(_context,_mediator.Object, CurrentUser);
            var request = new ProcessPaymentCommand
            {
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d744f3")
            };

            var result = sut.Handle(request, CancellationToken.None).Result;

            Assert.AreEqual(Unit.Value, result);
            var record = _context.Payments.Single(i => i.Id == request.Id);
            Assert.AreEqual(PaymentStatus.Processed, record.Status);

        }

        [Test]
        public void Handle_WhenPendingWithLowBalance_ShouldSetToClosed()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ReduceBalanceCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));
            var sut = new ProcessPaymentCommandHandler(_context, _mediator.Object, CurrentUser);
            var request = new ProcessPaymentCommand
            {
                Id = Guid.Parse("b162e88d-a3a6-4341-87da-725658d743f3")
            };

            var result = sut.Handle(request, CancellationToken.None).Result;

            Assert.AreEqual(Unit.Value, result);
            var record = _context.Payments.Single(i => i.Id == request.Id);
            Assert.AreEqual(PaymentStatus.Closed, record.Status);

        }

        [Test]
        public void Handle_WhenClosed_ShouldThrowException()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ReduceBalanceCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));
            var sut = new ProcessPaymentCommandHandler(_context, _mediator.Object, CurrentUser);
            var request = new ProcessPaymentCommand
            {
                Id = Guid.Parse("debf1d88-47ac-4fe4-a0b0-ce42f72ea66e")
            };

            Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));

        }

    }
}
