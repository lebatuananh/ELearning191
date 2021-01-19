using MediatR;
using PeaLearning.Application.Models;
using System;

namespace PeaLearning.Application.Queries
{
    public class GetExamQuery : IRequest<LessonDto>
    {
        public Guid Id { get; private set; }
        public GetExamQuery(Guid id)
        {
            Id = id;
        }
    }
}
