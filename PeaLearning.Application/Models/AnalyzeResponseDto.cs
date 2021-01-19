using System;

namespace PeaLearning.Application.Models
{
    public class AnalyzeResponseDto
    {
        public int NumOfCorrectAnswer { get; set; }
        public int TotalPoint { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public int CompletedDuration { get; set; }
        public LessonDto Lesson { get; set; }
    }
}
