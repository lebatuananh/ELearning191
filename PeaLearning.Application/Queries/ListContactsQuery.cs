using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;

namespace PeaLearning.Application.Queries
{
    public class ListContactsQuery: ListQuery, IRequest<QueryResult<ContactDto>>
    {
        public ListContactsQuery(int skip, int take, string query = null) : base(skip, take, query)
        {
        }
    }
}