using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PeaLearning.Website.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        UserManager<User> _userManger;

        public CustomClaimsPrincipalFactory(UserManager<User> userManager,
            RoleManager<Role> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManger = userManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManger.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(SystemConstants.UserClaim.FullName, user.DisplayName ?? string.Empty),
                new Claim(SystemConstants.UserClaim.Avatar, user.Avatar ?? string.Empty),
                new Claim(ClaimTypes.Role, string.Join(",", roles))
            });
            return principal;
        }
    }
}
