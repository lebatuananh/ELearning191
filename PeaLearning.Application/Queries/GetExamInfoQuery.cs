using MediatR;
using PeaLearning.Application.Models;
using System;

namespace PeaLearning.Application.Queries
{
    public class GetExamInfoQuery : IRequest<LessonInfoDto>
    {
        public Guid Id { get; private set; }

        public GetExamInfoQuery(Guid id)
        {
            Id = id;
        }
    }
}
