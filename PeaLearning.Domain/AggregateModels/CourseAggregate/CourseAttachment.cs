using Shared.EF;
using System;

namespace PeaLearning.Domain.AggregateModels.CourseAggregate
{
    public class CourseAttachment : DateTrackingEntity
    {
        public Guid CourseId { get; private set; }
        public string FileName { get; private set; }
        public long FileSize { get; private set; }
        public string FileUrl { get; private set; }
    }
}
