using System;
using MediatR;

namespace PeaLearning.Application.Commands.Course
{
    public class UpdateCourseCommand : IRequest
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Thumbnail { get; private set; }
        public Guid Id { get; private set; }
        public bool IsPrice { get; private set; }
        public long? Price { get; private set; }

        public UpdateCourseCommand(Guid id, string title, string description, string thumbnail, bool isPrice, long? price)
        {
            Id = id;
            Title = title;
            Description = description;
            Thumbnail = thumbnail;
            IsPrice = isPrice;
            Price = price;
        }
    }
}
