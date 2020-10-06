using System;
using Moq;
using Moula.Application.Contracts;

namespace Moula.Application.UnitTests.Common
{
    public abstract class BaseTest
    {
        protected readonly ICurrentUserService CurrentUser;

        protected BaseTest()
        {
            var mockCurrentUser = new Mock<ICurrentUserService>();
            mockCurrentUser.SetupGet(i => i.UserId).Returns(Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d"));
            CurrentUser = mockCurrentUser.Object;

        }

    }
}
