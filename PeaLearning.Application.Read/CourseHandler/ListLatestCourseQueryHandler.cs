using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class ListLatestCourseQueryHandler : IRequestHandler<ListLatestCourseQuery, IList<CourseDto>>
    {
        private readonly IDbConnection _connection;

        public ListLatestCourseQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<CourseDto>> Handle(ListLatestCourseQuery request, CancellationToken cancellationToken)
        {
            string query = "select * from courses order by created_date desc offset 0 rows fetch next @limit row only;";
            return (await _connection.QueryAsync<CourseDto>(query, new { limit = request.Limit })).ToList();
        }
    }
}
