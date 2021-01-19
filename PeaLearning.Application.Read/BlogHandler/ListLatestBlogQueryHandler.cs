using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.BlogHandler
{
    public class ListLatestBlogQueryHandler : IRequestHandler<ListLatestBlogQuery, IList<BlogDto>>
    {
        private readonly IDbConnection _connection;

        public ListLatestBlogQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<BlogDto>> Handle(ListLatestBlogQuery request, CancellationToken cancellationToken)
        {
            string query = "select * from blogs order by created_date desc offset 0 rows fetch next @limit row only;";
            return (await _connection.QueryAsync<BlogDto>(query, new { limit = request.Limit })).ToList();
        }
    }
}
