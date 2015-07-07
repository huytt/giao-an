using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.XoneAds.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = " Email rỗng! ")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email không đúng!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Mật khẩu rỗng! ")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 20 kí tự.")]
        public string password { get; set; }
        public string remember { get; set; }
    }
    public class SignUpModel
    {

        [Required(ErrorMessage = " username rỗng! ")]
        public string username { get; set; }

        [Required(ErrorMessage = " Email rỗng! ")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email không đúng!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Mật khẩu rỗng! ")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 20 kí tự.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Mật khẩu rỗng! ")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 đến 20 kí tự.")]
        [Compare("password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string rePassword { get; set; }
        public string Gender { get; set; }
        public string birthDay { get; set; }
    }
    public class forgotModel
    {

        [Required(ErrorMessage = " Email rỗng! ")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email không đúng!")]
        public string email { get; set; }
    }
}