using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class GetExamInfoQueryHandler : IRequestHandler<GetExamInfoQuery, LessonInfoDto>
    {
        private readonly IDbConnection _connection;

        public GetExamInfoQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<LessonInfoDto> Handle(GetExamInfoQuery request, CancellationToken cancellationToken)
        {

            return await _connection.QueryFirstOrDefaultAsync<LessonInfoDto>(@"select title, description, course_id, lesson_type, duration, is_active from lessons where id = @Id", new { request.Id });
        }
    }
}
