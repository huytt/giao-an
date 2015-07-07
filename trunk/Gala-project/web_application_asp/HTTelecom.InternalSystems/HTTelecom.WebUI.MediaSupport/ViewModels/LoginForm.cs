using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.MediaSupport.ViewModels
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Email is empty!!")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email Invalid!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is empty!!")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}