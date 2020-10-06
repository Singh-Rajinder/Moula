using System;
using Moula.Application.Contracts;

namespace Moula.Web.IntegrationTests.Common
{
    public class CurrentUserService: ICurrentUserService
    {
        public Guid UserId { get; } = Guid.Parse("39aff1c2-5530-4112-bd3d-b72b52f4d69d");
        public string Name { get; } = "John Doe";
    }
}
