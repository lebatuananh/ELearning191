using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PeaLearning.Api.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        private readonly UserManager<User> _userManager;

        public CustomClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("email", user.Email),
                new Claim("displayName", user.DisplayName),
                new Claim("avtar", user.Avatar ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });
            return principal;
        }
    }
}
