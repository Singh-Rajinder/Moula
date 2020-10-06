using System;
using Microsoft.AspNetCore.Http;
using Moula.Application.Contracts;

namespace Moula.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; }
        public string Name { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            // TODO: Populate properties from JWT token for authenticated users

            UserId = Guid.Parse("ad2aad60-5363-4554-b1a7-32ae5f22942f");
            Name = "Rajinder Singh";
        }
    }
}
