using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using Shared.Dto;

namespace PeaLearning.Application.Read.BannerHandler
{
    public class ListBannersQueryHandler : IRequestHandler<ListBannersQuery, QueryResult<BannerDto>>
    {
        private readonly IDbConnection _dbConnection;

        public ListBannersQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<QueryResult<BannerDto>> Handle(ListBannersQuery request, CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"              
               select b.name,b.link,b.banner_position,b.id
                from banners b
                    /**where**/
                order by b.created_date desc
                offset @Skip rows fetch next @Take row only");
            var tmplQueryCount = builder.AddTemplate(@"select count(b.id) 
                from banners b
                /**where**/");

            if (!string.IsNullOrEmpty(request.Query))
            {
                builder.Where(@"b.name like concat('%', @Query, '%' )");
            }

            var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

            using var queryResults =
                await _dbConnection.QueryMultipleAsync(queryStr, new {request.Skip, request.Take, request.Query});
            var items = queryResults.Read<BannerDto>();
            var count = queryResults.ReadFirst<int>();
            return new QueryResult<BannerDto>(count, items);
        }
    }
}