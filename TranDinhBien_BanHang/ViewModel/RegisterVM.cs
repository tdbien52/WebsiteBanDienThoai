using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TranDinhBien_BanHang.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Tên không được để trống.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống.")]
        [Compare("Password", ErrorMessage ="Mật khẩu và xác nhận mật khẩu không giống nhau.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Email không được để trống.")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ.")]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

    }
}