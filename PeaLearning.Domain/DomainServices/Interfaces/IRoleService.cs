using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using Shared.Dto;

namespace PeaLearning.Domain.DomainServices.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRole(Role role);
        Task<IdentityResult> UpdateRole(Role role);
        Task<QueryResult<Role>> GetRolePaging(string filter, int pageIndex, int pageSize);
        Task<Role> GetById(string id);
    }
}