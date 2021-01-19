using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;

namespace PeaLearning.Application.Queries
{
    public class ListBlogsQuery : ListQuery, IRequest<QueryResult<BlogDto>>
    {
        public ListBlogsQuery(int skip, int take, string query = null) : base(skip, take, query)
        {
        }
    }
}