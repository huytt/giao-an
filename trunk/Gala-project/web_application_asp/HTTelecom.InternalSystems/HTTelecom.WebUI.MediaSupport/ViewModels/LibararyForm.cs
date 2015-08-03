using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.MediaSupport.ViewModels
{
    public class LibraryForm
    {
        [Required(ErrorMessage = "Please input folder.")]
        [Display(Name = "Folder")]
        public string Folder { get; set; }
    }
}