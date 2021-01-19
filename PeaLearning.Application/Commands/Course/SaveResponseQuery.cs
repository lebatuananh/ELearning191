using MediatR;
using PeaLearning.Common.Models;
using System;
using System.Collections.Generic;

namespace PeaLearning.Application.Commands.Course
{
    public class SaveResponseQuery : IRequest<Guid>
    {
        public Guid CourseId { get; private set; }
        public Guid LessonId { get; private set; }
        public Guid LearnerId { get; private set; }
        public DateTimeOffset SubmittedDate { get; private set; }
        public IList<QuestionResponse> QuestionResponses { get; private set; }
        public int CompletedDuration { get; private set; }
        public SaveResponseQuery(Guid courseId, Guid lessonId, DateTimeOffset submittedDate, IList<QuestionResponse> questionResponses, Guid learnerId, int completedDuration)
        {
            CourseId = courseId;
            LessonId = lessonId;
            SubmittedDate = submittedDate;
            QuestionResponses = questionResponses;
            LearnerId = learnerId;
            CompletedDuration = completedDuration;
        }
    }
}
