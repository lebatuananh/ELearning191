using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using Shared.Dto;

namespace PeaLearning.Application.Read.BlogHandler
{
    public class ListBlogsQueryHandler : IRequestHandler<ListBlogsQuery, QueryResult<BlogDto>>
    {
        private readonly IDbConnection _connection;
        private readonly IBlogRepository _blogRepository;

        public ListBlogsQueryHandler(IDbConnection connection, IBlogRepository _blogRepository)
        {
            _connection = connection;
        }

        public async Task<QueryResult<BlogDto>> Handle(ListBlogsQuery request, CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"              
               select b.id,b.name,b.description,b.status,b.thumbnail 
                from blogs b 
                    /**where**/
                order by b.created_date desc
                offset @Skip rows fetch next @Take row only");
            var tmplQueryCount = builder.AddTemplate(@"select count(b.id) 
                from blogs b
                /**where**/");

            if (!string.IsNullOrEmpty(request.Query))
            {
                builder.Where(@"b.name like concat('%', @Query, '%' )");
            }

            var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

            using var queryResults =
                await _connection.QueryMultipleAsync(queryStr, new {request.Skip, request.Take, request.Query});
            var items = queryResults.Read<BlogDto>();
            var count = queryResults.ReadFirst<int>();
            return new QueryResult<BlogDto>(count, items);
        }
    }
}