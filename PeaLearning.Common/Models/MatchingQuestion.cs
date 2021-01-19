using System;
using System.Collections.Generic;

namespace PeaLearning.Common.Models
{
    public class MatchingQuestion : QuestionContent
    {
        public IList<ChoiceAnswer> Choices { get; set; }
        public Guid CorrectAnswerId { get; set; }
    }
}