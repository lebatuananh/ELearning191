using Shared.EF;

namespace PeaLearning.Domain.AggregateModels.Contact
{
    public class Contact: ModifierTrackingEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        public Contact(string name, string email, string title, string content)
        {
            Name = name;
            Email = email;
            Title = title;
            Content = content;
        }
    }
}