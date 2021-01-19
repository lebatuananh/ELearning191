using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using Shared.Dto;

namespace PeaLearning.Application.Read.UserHandler
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, QueryResult<UserDto>>
    {
        private readonly IDbConnection _connection;

        public ListUsersQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<QueryResult<UserDto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var builder = new SqlBuilder();
                var tmplQueryItems = builder.AddTemplate(@"              
               select * from users u 
               order by u.is_active
                    /**where**/
                offset @Skip rows fetch next @Take row only");
                var tmplQueryCount = builder.AddTemplate(@"select count(u.id) 
                from users u
                /**where**/");

                if (!string.IsNullOrEmpty(request.Query))
                {
                    builder.Where(@"u.user_name like concat('%', @Query, '%' )");
                }

                var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

                using var queryResults =
                    await _connection.QueryMultipleAsync(queryStr, new {request.Skip, request.Take, request.Query});
                var items = queryResults.Read<UserDto>();
                var count = queryResults.ReadFirst<int>();
                return new QueryResult<UserDto>(count, items);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}