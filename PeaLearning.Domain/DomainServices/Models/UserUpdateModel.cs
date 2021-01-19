namespace PeaLearning.Domain.DomainServices.Models
{
    public class UserUpdateModel
    {
        public string Avatar { get; set; }
        public bool Gender { get;  set; }
        public string Address { get;  set; }
        public bool IsActive { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}