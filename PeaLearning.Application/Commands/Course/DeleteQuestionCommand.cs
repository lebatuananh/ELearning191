using MediatR;
using System;

namespace PeaLearning.Application.Commands.Course
{
    public class DeleteQuestionCommand : IRequest
    {
        public Guid CourseId { get; private set; }
        public Guid LessonId { get; private set; }
        public Guid QuestionId { get; private set; }

        public DeleteQuestionCommand(Guid courseId, Guid lessonId, Guid questionId)
        {
            CourseId = courseId;
            LessonId = lessonId;
            QuestionId = questionId;
        }
    }
}
