using System;
using System.Collections.Generic;

namespace PeaLearning.Common.Models
{
    public class ArrangeWordResponse : QuestionResponse
    {
        public IList<ArrangeWordOptionResponse> ArrangeWordOptionResponses { get; set; }
    }

    public class ArrangeWordOptionResponse
    {
        public Guid Id { get; set; }
        public int SortOrder { get; set; }
    }
}
