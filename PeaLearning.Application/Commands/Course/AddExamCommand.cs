using MediatR;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using System;

namespace PeaLearning.Application.Commands.Course
{
    public class AddExamCommand : IRequest<Guid>
    {
        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public LessonType LessonType { get; private set; }
        public int Duration { get; private set; } // in minutes
        public bool IsActive { get; private set; }

        public AddExamCommand(Guid courseId, string title, string description, LessonType lessonType, int duration, bool isActive)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            LessonType = lessonType;
            Duration = duration;
            IsActive = isActive;
        }
    }
}
