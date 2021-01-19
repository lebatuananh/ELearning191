using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeaLearning.Website.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên", AllowEmptyStrings = false)]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập họ", AllowEmptyStrings = false)]
        public string LastName { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [RegularExpression(@"^[\w+][\w\.\-]+@[\w\-]+(\.\w{2,4})+$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải từ 6 - 100 kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { set; get; }

    }
}
