using HTTelecom.WebUI.eCommerce.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.eCommerce.Models
{
    public class SignInModel
    {
        //[Required(ErrorMessage = " EMAIL RỖNG! ")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "EMAIL NOT SYNTAX!")]
        public string email { get; set; }

        //[Required(ErrorMessage = "MẬT KHẨU RỖNG! ")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "MẬT KHẨU TỪ 6 ĐẾN 20 KÍ TỰ.")]
        public string password { get; set; }
        public string remember { get; set; }
    }
    public class SignUpModel
    {

        //[Required(ErrorMessage = "YÊU CẦU NHẬP HỌ")]
        //[Required(ErrorMessageResourceName = "FieldRequired",
        //     ErrorMessageResourceType = typeof(Lang))]
        public string phone { get; set; }

        public string address { get; set; }
        public string firstname { get; set; }
        //[Required(ErrorMessage = "YÊU CẦU NHẬP TÊN")]
        public string lastname { get; set; }

        //[Required(ErrorMessage = "YÊU CẦU NHẬP EMAIL")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "EMAIL NOT SYNTAX!")]
        public string email { get; set; }

        //[Required(ErrorMessage = "YÊU CẦU NHẬP MẬT KHẨU")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "MẬT KHẨU TỪ 6 ĐẾN 20 KÍ TỰ.")]
        public string password { get; set; }

        //[Required(ErrorMessage = "YÊU CẦU NHẬP LẠI MẬT KHẨU SO SÁNH")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "MẬT KHẨU TỪ 6 ĐẾN 20 KÍ TỰ.")]
        //[Compare("password", ErrorMessage = "THE NEW PASSWORD AND CONFIRMATION PASSWORD DO NOT MATCH.")]
        public string rePassword { get; set; }
        public string CaptchaCode { get; set; }
        public string Gender { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
    }
    public class ForgotModel
    {
        //[Required(ErrorMessage = " Email rỗng! ")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "EMAIL NOT SYNTAX!")]
        public string email { get; set; }

        public string CaptchaCode { get; set; }
    }

    public class ChangeForgotPasswordModel
    {
        public long cId { get; set; }

        //[Required(ErrorMessage = "YÊU CẦU NHẬP MÃ XÁC NHẬN.")]
        public string key{get;set;}

        //[Required(ErrorMessage = "YÊU CẦU NHẬP MẬT KHẨU")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "MẬT KHẨU TỪ 6 ĐẾN 20 KÍ TỰ.")]
        public string password { get; set; }

        //[Required(ErrorMessage = "YÊU CẦU NHẬP LẠI MẬT KHẨU SO SÁNH")]
        [DataType(DataType.Password)]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "MẬT KHẨU TỪ 6 ĐẾN 20 KÍ TỰ.")]
        //[Compare("password", ErrorMessage = "THE NEW PASSWORD AND CONFIRMATION PASSWORD DO NOT MATCH.")]
        public string rePassword { get; set; }
    }
}