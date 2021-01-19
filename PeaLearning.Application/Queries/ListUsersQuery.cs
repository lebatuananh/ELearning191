using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;

namespace PeaLearning.Application.Queries
{
    public class ListUsersQuery: ListQuery, IRequest<QueryResult<UserDto>>
    {
        public ListUsersQuery(int skip, int take, string query = null) : base(skip, take, query)
        {
        }
    }
}