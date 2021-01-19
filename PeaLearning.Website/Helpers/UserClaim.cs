using Microsoft.AspNetCore.Http;
using Shared.Infrastructure;
using System;
using System.Security.Claims;

namespace PeaLearning.Website.Helpers
{
    public class UserClaim : IUserClaim
    {
        public Guid UserId { get; private set; }

        public string UserName { get; private set; }

        public string UserEmail { get; private set; }

        public UserClaim(IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor.HttpContext?.User;
            UserId = Guid.Parse(claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            UserName = claims?.FindFirst(ClaimTypes.Name)?.Value;
            UserEmail = claims?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
