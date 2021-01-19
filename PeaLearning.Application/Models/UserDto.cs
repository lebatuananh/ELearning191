using System;

namespace PeaLearning.Application.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
    }
}