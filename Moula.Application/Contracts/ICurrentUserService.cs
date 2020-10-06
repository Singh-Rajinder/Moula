using System;

namespace Moula.Application.Contracts
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Name { get; }
    }
}
