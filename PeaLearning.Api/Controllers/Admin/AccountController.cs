using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeaLearning.Api.Filters;
using PeaLearning.Api.Requests.Login;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using Shared.Constants;

namespace PeaLearning.Api.Controllers.Admin
{
    public class AccountController : AdminV1Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [Route("login")]
        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return NotFound($"Không tìm thấy tài khoản {request.UserName}");
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
            if (!result.Succeeded)
                return BadRequest("Mật khẩu không đúng");
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(SystemConstants.UserClaim.Avatar, user.Avatar ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(SystemConstants.UserClaim.FullName, user.DisplayName ?? string.Empty),
                new Claim(ClaimTypes.Role, string.Join(",", roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

        }
    }
}