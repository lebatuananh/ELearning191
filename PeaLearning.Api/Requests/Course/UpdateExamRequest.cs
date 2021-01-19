using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Api.Requests.Course
{
    public class UpdateExamRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public int Duration { get; set; } // in minutes
        public bool IsActive { get; set; }
    }
}
