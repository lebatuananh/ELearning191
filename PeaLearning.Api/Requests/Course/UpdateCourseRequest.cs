namespace PeaLearning.Api.Requests.Course
{
    public class UpdateCourseRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public bool IsPrice { get; set; }
        public long? Price { get; set; }
    }
}
