using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Domain.DomainServices.Interfaces;
using Shared.Dto;

namespace PeaLearning.Domain.DomainServices.Impl
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRole(Role role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> UpdateRole(Role role)
        {
            var roleById = await _roleManager.FindByIdAsync(role.Id.ToString());
            if (roleById == null)
            {
                return null;
            }

            var result = await _roleManager.UpdateAsync(role);
            return result;

        }

        public async Task<QueryResult<Role>> GetRolePaging(string filter, int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }

            var result = await query.ToQueryResultAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<Role> GetById(string id)
        {
            var roleById = await _roleManager.FindByIdAsync(id);
            return roleById ?? null;
        }
    }
}