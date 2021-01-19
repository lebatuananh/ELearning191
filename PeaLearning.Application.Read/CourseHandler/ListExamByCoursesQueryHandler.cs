using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class ListExamByCoursesQueryHandler : IRequestHandler<ListExamByCoursesQuery, QueryResult<LessonDto>>
    {
        private readonly IDbConnection _connection;

        public ListExamByCoursesQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<QueryResult<LessonDto>> Handle(ListExamByCoursesQuery request, CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"              
                select les.*, crs.*
                    from lessons les 
                    join courses crs
                    on crs.id = les.course_id
                    /**where**/
                order by les.created_date desc
                offset @Skip rows fetch next @Take row only");
            var tmplQueryCount = builder.AddTemplate(@"select count(les.id) 
                from lessons les
                join courses crs
                    on crs.id = les.course_id
                /**where**/");
            builder.Where("crs.id = @CourseId");

            if (!string.IsNullOrEmpty(request.Query))
            {
                builder.Where(@"les.title like concat('%', @Query, '%' )");
            }

            var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

            using var queryResults = await _connection.QueryMultipleAsync(queryStr, new { request.Skip, request.Take, request.Query, request.CourseId });
            var lessDic = new Dictionary<Guid, LessonDto>();
            queryResults.Read<LessonDto, CourseDto, LessonDto>((lesson, course) =>
            {
                if (!lessDic.TryGetValue(lesson.Id, out var lessonEntry))
                {
                    lessonEntry = lesson;
                    lessonEntry.Course = course;
                    lessDic.Add(lessonEntry.Id, lessonEntry);
                }
                return lessonEntry;
            });
            var items = lessDic.Values;
            var count = queryResults.ReadFirst<int>();
            return new QueryResult<LessonDto>(count, items);
        }
    }
}
