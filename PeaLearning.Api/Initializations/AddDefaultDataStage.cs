using Microsoft.AspNetCore.Identity;
using PeaLearning.Common;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Infrastructure;
using Shared.Initialization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeaLearning.Api.Initializations
{
    public class AddDefaultDataStage : IInitializationStage
    {
        private readonly PeaDbContext _context;
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;

        public AddDefaultDataStage(PeaDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public int Order => 2;
        public async Task ExecuteAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new Role(Constants.Role.Administrator, Constants.Role.Administrator));
                await _roleManager.CreateAsync(new Role(Constants.Role.Learner, Constants.Role.Administrator));
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new User("admin", "Admin", "Super", "super.admin@gmail.com", string.Empty, true, "Ha noi", true), "123654$");
                var user = await _userManager.FindByNameAsync("admin");
                if (user != null)
                    await _userManager.AddToRoleAsync(user, Constants.Role.Administrator);
            }
            await _context.SaveChangesAsync();
        }
    }
}
