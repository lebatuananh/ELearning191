using MediatR;
using PeaLearning.Application.Models;
using System;

namespace PeaLearning.Application.Queries
{
    public class GetCourseQuery : IRequest<CourseDto>
    {
        public Guid Id { get; private set; }

        public GetCourseQuery(Guid id)
        {
            Id = id;
        }
    }
}
