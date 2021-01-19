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
    public class ListLessonByCourseQueryHandler : IRequestHandler<ListLessonByCourseQuery, IList<LessonInfoDto>>
    {
        private readonly IDbConnection _connection;

        public ListLessonByCourseQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<LessonInfoDto>> Handle(ListLessonByCourseQuery request, CancellationToken cancellationToken)
        {
            string query = @"select l.* from lessons l
                             join courses crs 
                             on l.course_id = crs.id 
                             where crs.code = @code and is_active = @isActive
                             order by l.created_date desc";
            return (await _connection.QueryAsync<LessonInfoDto>(query, new { code = request.Code, isActive = true })).ToList();
        }
    }
}
