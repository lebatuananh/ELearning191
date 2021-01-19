using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Domain.DomainServices.Models;
using Shared.Dto;

namespace PeaLearning.Domain.DomainServices.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUser(User user,string password,string roleName);
        Task<IdentityResult> UpdateUser(string id,UserUpdateModel user);
        Task<QueryResult<User>> GetUserPaging(string filter, int pageIndex, int pageSize);
        Task<User> GetById(string id);
        Task<IdentityResult> AddToRoles(string userId, IEnumerable<string> roleNames);
        Task<IdentityResult> ChangeUserPassword(string id, string currentPassword,string newPassword);
        
    }
}