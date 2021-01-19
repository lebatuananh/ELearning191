using MediatR;

namespace PeaLearning.Application.Commands.Course
{
    public class AddCourseCommand : IRequest
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Thumbnail { get; private set; }
        public bool IsPrice { get; private set; }
        public long? Price { get; private set; }

        public AddCourseCommand(string title, string description, string thumbnail, bool isPrice, long? price)
        {
            Title = title;
            Description = description;
            Thumbnail = thumbnail;
            IsPrice = isPrice;
            Price = price;
        }
    }
}