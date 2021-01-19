using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Domain.DomainServices.Interfaces;
using PeaLearning.Domain.DomainServices.Models;
using Shared.Dto;

namespace PeaLearning.Domain.DomainServices.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(User user, string password, string roleName)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded || string.IsNullOrEmpty(roleName)) return result;
            var appUser = await _userManager.FindByNameAsync(user.UserName);
            if (appUser != null)
                await _userManager.AddToRolesAsync(appUser, roleName.Split(','));

            return result;
        }

        public async Task<IdentityResult> UpdateUser(string id, UserUpdateModel user)
        {
            var userById = await _userManager.FindByIdAsync(id);
            if (userById == null)
            {
                return null;
            }

            userById.Update(user.FirstName, user.LastName, user.Avatar, user.Gender, user.Address, user.IsActive);
            var result = await _userManager.UpdateAsync(userById);
            return result;
        }

        public async Task<QueryResult<User>> GetUserPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x =>
                    x.UserName.Contains(filter) || x.FirstName.Contains(filter) || x.LastName.Contains(filter));
            }

            var result = await query.ToQueryResultAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<User> GetById(string id)
        {
            var userById = await _userManager.FindByIdAsync(id);
            return userById ?? null;
        }

        public async Task<IdentityResult> AddToRoles(string userId, IEnumerable<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            var result = await _userManager.AddToRolesAsync(user, roleNames);
            return result;
        }

        public async Task<IdentityResult> ChangeUserPassword(string id, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }
    }
}