using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using Shared.Dto;

namespace PeaLearning.Application.Read.ContactHandler
{
    public class ListContactsQueryHandler: IRequestHandler<ListContactsQuery, QueryResult<ContactDto>>
    {
        private readonly IDbConnection _connection;

        public ListContactsQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<QueryResult<ContactDto>> Handle(ListContactsQuery request, CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"              
                select c.*
                    from contacts c 
                    /**where**/
                order by c.created_date desc
                offset @Skip rows fetch next @Take row only");
            var tmplQueryCount = builder.AddTemplate(@"select count(c.id) 
                from contacts c
                /**where**/");

            if (!string.IsNullOrEmpty(request.Query))
            {
                builder.Where(@"c.title like concat('%', @Query, '%' )");
            }

            var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

            using var queryResults = await _connection.QueryMultipleAsync(queryStr, new { request.Skip, request.Take, request.Query });
            var items = queryResults.Read<ContactDto>();
            var count = queryResults.ReadFirst<int>();
            return new QueryResult<ContactDto>(count, items);
        }
    }
}