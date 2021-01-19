using System.Collections.Generic;
using PeaLearning.Domain.AggregateModels.CourseAggregate;

namespace PeaLearning.Api.ViewModels.User
{
    public class UserVm
    {
        public UserVm(string id, string firstName, string lastName, string displayName, string avatar, int gender, string address, int isActive)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Avatar = avatar;
            Gender = gender;
            Address = address;
            IsActive = isActive;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public int IsActive { get; set; }
        public virtual IList<CourseRegistration> Registrations { get; set; }
    }
}