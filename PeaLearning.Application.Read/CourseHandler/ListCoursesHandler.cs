using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class ListCoursesHandler : IRequestHandler<ListCourseQuery, List<CourseDto>>
    {
        private readonly IDbConnection _connection;

        public ListCoursesHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<CourseDto>> Handle(ListCourseQuery request, CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"select crs.* from courses crs ");
            var result = await _connection.QueryAsync<CourseDto>(tmplQueryItems.RawSql);
            return result.ToList();
        }
    }
}