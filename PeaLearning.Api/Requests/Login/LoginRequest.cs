using System.ComponentModel.DataAnnotations;

namespace PeaLearning.Api.Requests.Login
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}