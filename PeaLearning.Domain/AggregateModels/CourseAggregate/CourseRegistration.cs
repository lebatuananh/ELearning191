using PeaLearning.Domain.AggregateModels.UserAggregate;
using Shared.EF;
using System;

namespace PeaLearning.Domain.AggregateModels.CourseAggregate
{
    public class CourseRegistration: ModifierTrackingEntity
    {
        public Guid CourseId { get; private set; }
        public Guid LearnerId { get; private set; }
        public virtual Course Course { get; private set; }
        public virtual User Learner { get; private set; }
    }
}
