using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;

namespace PeaLearning.Application.Queries
{
    public class ListBannersQuery: ListQuery, IRequest<QueryResult<BannerDto>>
    {
        public ListBannersQuery(int skip, int take, string query = null) : base(skip, take, query)
        {
        }
    }
}