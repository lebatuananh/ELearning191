using System;
using MediatR;

namespace PeaLearning.Application.Commands.Course
{
    public class DeleteCourseCommand : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteCourseCommand(Guid id)
        {
            Id = id;
        }
    }
}
