namespace PeaLearning.Api.Requests.Users
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Confirmpwd { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get;  set; }
        public string Email { get; set; }
        public string Address { get;  set; }
        public bool IsActive { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleNames { get; set; }
    }
}