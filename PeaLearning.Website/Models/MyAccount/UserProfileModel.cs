using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeaLearning.Website.Models.MyAccount
{
    public class UserProfileModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên", AllowEmptyStrings = false)]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập họ", AllowEmptyStrings = false)]
        public string LastName { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [RegularExpression(@"^[\w+][\w\.\-]+@[\w\-]+(\.\w{2,4})+$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { set; get; }
    }

    public class UserStatisticModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DisplayName { get; set; }
        public double Score { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
