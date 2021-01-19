using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class GetExamQueryHandler : IRequestHandler<GetExamQuery, LessonDto>
    {
        private readonly IDbConnection _connection;

        public GetExamQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<LessonDto> Handle(GetExamQuery request, CancellationToken cancellationToken)
        {
            var lessonDictionary = new Dictionary<Guid, LessonDto>();
            var result = await _connection.QueryAsync<LessonDto, CourseDto, QuestionDto, LessonDto>(@"select les.*, crs.*, q.*
                    from lessons les
                    join courses crs
                    on crs.id = les.course_id
                    left join questions q
                    on les.id = q.lesson_id
                    where les.id = @Id
                    order by q.created_date", (lesson, course, question) =>
                    {
                        if (!lessonDictionary.TryGetValue(lesson.Id, out var lessonEntry))
                        {
                            lessonEntry = lesson;
                            lessonEntry.Questions = new List<QuestionDto>();
                            lessonEntry.Course = course;
                            lessonDictionary.Add(lessonEntry.Id, lessonEntry);
                        }
                        if (question != null && lessonEntry.Questions.FirstOrDefault(i => i.Id == question.Id) == null)
                        {
                            lessonEntry.Questions.Add(question);
                        }
                        return lessonEntry;
                    }, new { request.Id });
            return result.FirstOrDefault();
        }
    }
}
