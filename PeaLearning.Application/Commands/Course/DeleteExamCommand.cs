using MediatR;
using System;

namespace PeaLearning.Application.Commands.Course
{
    public class DeleteExamCommand : IRequest
    {
        public Guid CourseId { get; private set; }
        public Guid LessonId { get; private set; }

        public DeleteExamCommand(Guid courseId, Guid lessonId)
        {
            CourseId = courseId;
            LessonId = lessonId;
        }
    }
}
