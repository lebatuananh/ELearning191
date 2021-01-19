using System;
using System.Collections.Generic;

namespace PeaLearning.Common.Models
{
    public class ArrangeWordQuestion : QuestionContent
    {
        public IList<ArrangeWordOptionQuestion> ArrangeWordOptionQuestions { get; set; }
    }

    public class ArrangeWordOptionQuestion
    {
        public Guid Id { get; set; }
        public string Word { get; set; }
        public int SortOrder { get; set; }
    }
}