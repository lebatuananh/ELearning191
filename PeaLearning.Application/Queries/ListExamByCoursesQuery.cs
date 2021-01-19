using MediatR;
using PeaLearning.Application.Models;
using Shared.Dto;
using System;

namespace PeaLearning.Application.Queries
{
    public class ListExamByCoursesQuery : ListQuery, IRequest<QueryResult<LessonDto>>
    {
        public Guid CourseId { get; private set; }
        public ListExamByCoursesQuery(
            Guid courseId,
          int skip,
          int take,
          string query = null
          )
          : base(skip, take, query)
        {
            CourseId = courseId;
        }
    }
}
