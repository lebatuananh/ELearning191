using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.Dto;
using System;
using System.Collections.Generic;

namespace PeaLearning.Application.Models
{
    public class LessonDto : DateTrackingDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public int Duration { get; set; } // in minutes
        public bool IsActive { get; set; }
        public CourseDto Course { get; set; }
        public IList<QuestionDto> Questions { get; set; }
    }
}
