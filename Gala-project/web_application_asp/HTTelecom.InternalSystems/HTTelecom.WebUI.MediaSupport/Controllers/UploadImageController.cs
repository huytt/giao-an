using MSSRepository.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.DataContext.cis;
using Newtonsoft;
using System.Web.Script.Serialization;

namespace HTTelecom.WebUI.MediaSupport.Controllers
{
    public class UploadImageController : Controller
    {
        //
        // GET: /UploadImage/

        public ActionResult Index()
        {
            return View();
        }

        [WebMethod]
        public JsonResult UploadImage(string entities, string id, string token, string extension)
        {
            try
            {
                
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase ImageFile = Request.Files["FileUpload"];

                    //code here
                    //xử lý thêm ảnh
                    string createFolders = Path.Combine(Server.MapPath("~/Media"), entities);
                    if (!System.IO.Directory.Exists(createFolders))
                    {
                        Directory.CreateDirectory(createFolders);
                    }
                    string createFolder = Path.Combine(createFolders, id);

                    if (!System.IO.Directory.Exists(createFolder))
                    {
                        Directory.CreateDirectory(createFolder);
                    }
     
                    if (ImageFile != null)
                    {
                        switch (entities)
                        { 
                            case "Vendor":
                                #region Vendor
                                try
                                {
                                    int MaxSize = 500;//set tĩnh, trong khi đợi DB thay đổi và nâng cấp
                                    if (extension == "BusinessLicense")
                                    {
                                        MaxSize = 2000;
                                    }
                                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
                                    bool valid = true;
                                    if (ImageFile != null)
                                    {
                                        if (!AllowedFileExtensions.Contains(ImageFile.FileName.Substring(ImageFile.FileName.LastIndexOf('.')).ToLower())
                                            || ImageFile.ContentLength / 1024 > MaxSize)
                                        {
                                            valid = false;
                                        }
                                    }
                                    if (valid == true)
                                    {

                                        //===============================================
                                        String nameImage = CreateNewName(Path.GetExtension(ImageFile.FileName), entities);
                                        if (extension == "BusinessLicense")
                                        {
                                            createFolder = createFolder + "\\BusinessLicense\\";
                                            if (!System.IO.Directory.Exists(createFolder))
                                            {
                                                Directory.CreateDirectory(createFolder);
                                            }
                                        }
                                        string fullPathName = Path.Combine(createFolder, nameImage); //path full
                                        ImageHelper.UploadImage(ImageFile, fullPathName);
                                        //===============================================
                                        Vendor ven = new Vendor();
                                        ven.VendorId = long.Parse(id);
                                        string link = "";
                                        if (extension == "BusinessLicense")
                                        {
                                            ven.BusinessLicenseFile = "Media/" + entities + "/" + id + "/BusinessLicense/" + nameImage;
                                            link = ven.BusinessLicenseFile;
                                        }
                                        else
                                        {
                                            ven.LogoFile = "Media/" + entities + "/" + id + "/" + nameImage;
                                            link = ven.LogoFile;
                                        }

                                        VendorRepository _iVendorService = new VendorRepository();
                                        _iVendorService.Update(ven);
                                        return Json(new { success = true,link= link}, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return Json(new { success = false,Error= ex.Message }, JsonRequestBehavior.AllowGet);
                                    
                                }
                                   
                                #endregion
                                break;
                            case "Customer":
                                #region Customer
                                try
                                {
                                    CustomerRepository _iCustomerService = new CustomerRepository();
                                    Customer cus = new Customer();
                                    cus.CustomerId = long.Parse(id);
                                    if (token == _iCustomerService.GetById(long.Parse(id)).SecurityToken)
                                    {
                                        //xóa các avatar đã có trước đó.
                                        List<string> picList = Directory.GetFiles(createFolder, "*.jpg").ToList();
                                        picList.AddRange(Directory.GetFiles(createFolder, "*.png").ToList());
                                        foreach (string f in picList)
                                        {
                                            System.IO.File.Delete(f);
                                        }
                                        //===============================================   
                                        String nameImage = CreateNewName(Path.GetExtension(ImageFile.FileName), entities);
                                        string fullPathName = Path.Combine(createFolder, nameImage); //path full
                                        ImageHelper.UploadImage(ImageFile, fullPathName);
                                        //===============================================
                                        //Update Customer                                
                                        cus.AvatarPhotoUrl = "Media/" + entities + "/" + id + "/" + nameImage;
                                        _iCustomerService.UpdateCustomer(cus);

                                    }
                                    _iCustomerService.RemoveSecurityToken(cus.CustomerId);// thành công hay không cũng phải xóa access token
                                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                                }
                                catch (Exception ex)
                                {
                                    return Json(new { success = false, Error=ex.Message }, JsonRequestBehavior.AllowGet);
                                }                               
                                #endregion                             
                            default:
                                break;
                        }
                    
                   
                    }
                } 
            }
            catch 
            {
              
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);  
        }
        private string CreateNewName(string extension, string typeMedia)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + typeMedia + "_" + Guid.NewGuid().ToString() + extension;
        }
        


    }
}
