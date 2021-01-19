using Microsoft.AspNetCore.Identity;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PeaLearning.Domain.AggregateModels.UserAggregate
{
    public class User : IdentityUser<Guid>, IAggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped] public string DisplayName => $"{FirstName} {LastName}";
        public string Avatar { get; private set; }
        public bool Gender { get; private set; }
        public string Address { get; set; }
        public bool IsActive { get; private set; }
        public virtual IList<CourseRegistration> Registrations { get; private set; }
        public virtual IList<Response> Responses { get; private set; }

        public User()
        {
        }

        public User(string userName, string firstName, string lastName, string email, string avatar, bool gender,
            string address, bool isActive) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Avatar = avatar;
            Gender = gender;
            Address = address;
            IsActive = isActive;
        }

        public void Update(string firstName, string lastName, string avatar, bool gender,
            string address, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            Avatar = avatar;
            Gender = gender;
            Address = address;
            IsActive = isActive;
        }
    }
}