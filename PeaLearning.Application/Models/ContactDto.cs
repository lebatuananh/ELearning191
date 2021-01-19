using Shared.Dto;

namespace PeaLearning.Application.Models
{
    public class ContactDto: ModifierTrackingDto
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Title { get;  set; }
        public string Content { get;  set; }
    }
}