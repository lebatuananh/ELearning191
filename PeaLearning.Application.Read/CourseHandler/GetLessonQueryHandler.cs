using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using PeaLearning.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, LessonDto>
    {
        private readonly IDbConnection _connection;

        public GetLessonQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<LessonDto> Handle(GetLessonQuery request, CancellationToken cancellationToken)
        {
            var lessonDictionary = new Dictionary<Guid, LessonDto>();
            var result = await _connection.QueryAsync<LessonDto, CourseDto, QuestionDto, LessonDto>(@"select les.*, crs.*, q.*, par.content as parent_content, par.example as parent_example
                    from lessons les
                    join courses crs
                    on crs.id = les.course_id
                    left join questions q
                    left join questions par
                    on q.parent_id = par.id
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
                if (question != null && lessonEntry.Questions.FirstOrDefault(i => i.Id == question.Id) == null && question.QuestionContent.QuestionType != QuestionType.Section)
                {
                    lessonEntry.Questions.Add(question);
                }
                return lessonEntry;
            }, new { request.Id});
            return result.FirstOrDefault();
        }
    }
}
