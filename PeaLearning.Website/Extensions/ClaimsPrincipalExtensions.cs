using PeaLearning.Domain.AggregateModels.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PeaLearning.Website.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim != null ? claim.Value : string.Empty;
        }

        public static User CurrentUser(this ClaimsPrincipal user)
        {
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                return new User
                {
                    UserName = user.GetSpecificClaim(ClaimTypes.Name),
                    Email = user.GetSpecificClaim(ClaimTypes.Email),
                    Id = Guid.Parse(user.GetSpecificClaim(ClaimTypes.NameIdentifier))
                };
            }
            return null;
        }

        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity).Claims.Single(x => x.Type == "UserId");
            return Guid.Parse(claim.Value);
        }
    }
}
