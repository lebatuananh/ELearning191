using Newtonsoft.Json;
using PeaLearning.Common.Models;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace PeaLearning.Application.Models
{
    public class ResponseDto
    {
        public string Content { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public virtual ICollection<QuestionResponse> QuestionResponses
        {
            get => Content.TryDeserialize<ICollection<QuestionResponse>>();
            private set => Content = JsonConvert.SerializeObject(value);
        }
        public Guid LessonId { get; set; }
        public int CompletedDuration { get; set; }
    }
}
