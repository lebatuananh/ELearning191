using MediatR;
using PeaLearning.Application.Models;
using System;

namespace PeaLearning.Application.Queries
{
    public class GetLessonQuery : IRequest<LessonDto>
    {
        public Guid Id { get; private set; }
        public GetLessonQuery(Guid id)
        {
            Id = id;
        }
    }
}
