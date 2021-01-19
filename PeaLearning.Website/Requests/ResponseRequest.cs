using PeaLearning.Common.Models;
using System;
using System.Collections.Generic;

namespace PeaLearning.Website.Requests
{
    public class ResponseRequest
    {
        public DateTimeOffset SubmittedDate { get; set; }
        public IList<QuestionResponse> QuestionResponses { get; set; }
        public int CompletedDuration { get; set; }
    }
}
