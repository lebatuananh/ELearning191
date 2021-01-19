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
    public class ListLatestLessonQueryHandler : IRequestHandler<ListLatestLessonQuery, IList<LessonInfoDto>>
    {
        private readonly IDbConnection _connection;

        public ListLatestLessonQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<LessonInfoDto>> Handle(ListLatestLessonQuery request, CancellationToken cancellationToken)
        {
            string query = "select * from lessons order by created_date desc offset 0 rows fetch next @limit row only;";
            return (await _connection.QueryAsync<LessonInfoDto>(query, new { limit = request.Limit })).ToList();
        }
    }
}
