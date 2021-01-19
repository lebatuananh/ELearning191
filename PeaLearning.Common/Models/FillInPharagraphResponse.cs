using System;
using System.Collections.Generic;

namespace PeaLearning.Common.Models
{
    public class FillInPharagraphResponse : QuestionResponse
    {
        public IList<FillInPharagraphContentResponse> FillInPharagraphContentResponses { get; set; }
    }

    public class FillInPharagraphContentResponse
    {
        public Guid FillOptionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}
