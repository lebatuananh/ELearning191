using System;
using System.Collections.Generic;

namespace PeaLearning.Common.Models
{
    public class FillInPharagraphQuestion : QuestionContent
    {
        public IList<FillInPharagraphContent> FillInPharagraphContents { get; set; }
    }

    public class FillInPharagraphContent
    {
        public Guid FillOptionId { get; set; }
        public string FillOption { get; set; }
        public IList<ChoiceAnswer> Choices { get; set; }
        public Guid CorrectAnswerId { get; set; }
    }
}
