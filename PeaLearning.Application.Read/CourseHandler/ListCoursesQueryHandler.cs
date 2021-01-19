using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using Shared.Dto;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class ListCoursesQueryHandler : IRequestHandler<ListCoursesQuery, QueryResult<CourseDto>>
    {
        private readonly IDbConnection _connection;

        public ListCoursesQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<QueryResult<CourseDto>> Handle(ListCoursesQuery request, CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"              
                select crs.*
                    from courses crs 
                    /**where**/
                order by crs.created_date desc
                offset @Skip rows fetch next @Take row only");
            var tmplQueryCount = builder.AddTemplate(@"select count(crs.id) 
                from courses crs
                /**where**/");

            if (!string.IsNullOrEmpty(request.Query))
            {
                builder.Where(@"crs.title like concat('%', @Query, '%' )");
            }

            var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

            using var queryResults = await _connection.QueryMultipleAsync(queryStr, new { request.Skip, request.Take, request.Query });
            var items = queryResults.Read<CourseDto>();
            var count = queryResults.ReadFirst<int>();
            return new QueryResult<CourseDto>(count, items);
        }
    }
}
