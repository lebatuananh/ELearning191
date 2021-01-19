using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
    {
        private readonly IDbConnection _connection;

        public GetCourseQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _connection.QueryFirstOrDefaultAsync<CourseDto>("select * from courses where id = @Id", new { request.Id });
            return course;
        }
    }
}
