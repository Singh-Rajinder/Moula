using System.Linq;
using System.Threading;
using FluentValidation.TestHelper;
using Moula.Application.Customers.Commands.ReduceBalance;
using Moula.Application.UnitTests.Common;
using Moula.Persistence;
using NUnit.Framework;

namespace Moula.Application.UnitTests.Customers.Commands.ReduceBalance
{
    public class ReduceBalanceCommandTests : BaseTest
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
        public void Handle_GivenValidRequest_ShouldReduceBalance()
        {
            var sut = new ReduceBalanceCommandHandler(_context, CurrentUser);
            var reduceAmount = 1000;

            var result = sut.Handle(new ReduceBalanceCommand { ReduceAmount = reduceAmount }, CancellationToken.None);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual(49000, _context.Customers.Single(i => i.Id == CurrentUser.UserId).Balance);
        }

        [Test]
        public void Handle_GivenInvalidAmount_ShouldReturnFalse()
        {
            var sut = new ReduceBalanceCommandHandler(_context, CurrentUser);
            var reduceAmount = 100000;

            var result = sut.Handle(new ReduceBalanceCommand { ReduceAmount = reduceAmount }, CancellationToken.None);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(50000, _context.Customers.Single(i => i.Id == CurrentUser.UserId).Balance);
        }

        [Test]
        public void Handle_GivenInvalidData_ShouldFailValidation()
        {
            var validator = new ReduceBalanceCommandValidator();

            validator.ShouldHaveValidationErrorFor(i => i.ReduceAmount, 0);
        }
    }
}
