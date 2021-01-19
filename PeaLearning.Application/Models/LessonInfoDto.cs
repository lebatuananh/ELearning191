using PeaLearning.Common.Utils;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using System;

namespace PeaLearning.Application.Models
{
    public class LessonInfoDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public int Duration { get; set; } // in minutes
        public bool IsActive { get; set; }
        public string Link => string.Format("/lesson/{0}-lid{1}", StringUtils.UnicodeToUnsignCharAndDash(Title), Id);
    }
}
