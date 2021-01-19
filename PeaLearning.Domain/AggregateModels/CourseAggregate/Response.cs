using Newtonsoft.Json;
using PeaLearning.Common.Models;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using Shared.EF;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeaLearning.Domain.AggregateModels.CourseAggregate
{
    public class Response : ModifierTrackingEntity
    {
        [JsonIgnore]
        public string Content { get; private set; }
        public DateTimeOffset? SubmittedDate { get; private set; }
        public int CompletedDuration { get; private set; } // in seconds
        [NotMapped]
        public virtual ICollection<QuestionResponse> QuestionResponses
        {
            get => Content.TryDeserialize<ICollection<QuestionResponse>>();
            private set => Content = JsonConvert.SerializeObject(value);
        }
        public Guid LessonId { get; private set; }
        public virtual Lesson Lesson { get; private set; }
        public Guid LearnerId { get; private set; }
        public virtual User User { get; private set; }

        public Response()
        {
        }
        public Response(DateTimeOffset? submittedDate, ICollection<QuestionResponse> questionResponses, Guid learnerId, int completedDuration) : this()
        {
            SubmittedDate = submittedDate;
            QuestionResponses = questionResponses;
            LearnerId = learnerId;
            CompletedDuration = completedDuration;
        }
    }
}
