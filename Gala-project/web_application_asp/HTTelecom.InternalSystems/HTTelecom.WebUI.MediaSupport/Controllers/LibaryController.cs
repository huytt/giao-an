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
using ImageResizer;
using HTTelecom.WebUI.MediaSupport.Common;
using System.Drawing;


namespace HTTelecom.WebUI.MediaSupport.Controllers
{

    /// <summary>
    /// Permission:
    /// 
    /// Index[Pai Login account MediaSupport] co Add, Deleted [quyen User,Writer "show" ADD], [quyen Mod trở lên "show" ADD & DELETED]
    /// ListImage [Pai Login account MediaSupport]
    /// DeleteImage [Pai Login account MediaSupport], [Quyen Mod tro len]
    /// </summary>



    public class LibaryController : Controller
    {
        private string MsgError = "Folder not exist, please input agian.";
        private string MsgNotFound = "Folder not contant Image.";
        public ActionResult Index()
        {
            ViewBag.ListFileName = TempData["ListFileName"] == null ? new List<string>() : TempData["ListFileName"];
            ViewBag.FolderName = TempData["FolderName"];
            string pos = '\\' + ViewBag.FolderName + '\\';
            var path = Server.MapPath("~/Media/Source/" + pos);
            if (!Directory.Exists(path))
            {
                ViewBag.Error = MsgError;
                ViewBag.Images = null;
            }
            else
            {
                var accept_permision = new List<string>() { ".PNG", ".JPG" };
                ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Media/Source/" + pos))
                    .Where(n =>accept_permision.Contains(Path.GetExtension(n).ToUpper()))
                                          .Select(fn => "" + Path.GetFileName(fn));
                ViewBag.MsgNotFound = MsgNotFound;
            }
            ViewBag.path = path;
            return View();
        }
        public ActionResult ListImage()
        {
            ViewBag.ListFileName = TempData["ListFileName"] == null ? new List<string>() : TempData["ListFileName"];
            ViewBag.FolderName = TempData["FolderName"];
            string pos = '\\' + ViewBag.FolderName + '\\';
            var path = Server.MapPath("~/Media/Source/" + pos);
            if (!Directory.Exists(path))
            {
                ViewBag.Error = MsgError;
                ViewBag.Images = null;
            }
            else
            {
                var accept_permision = new List<string>() { ".PNG", ".JPG" };
                ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Media/Source/" + pos))
                    .Where(n => accept_permision.Contains(Path.GetExtension(n).ToUpper()))
                                          .Select(fn => "" + Path.GetFileName(fn));
                ViewBag.MsgNotFound = MsgNotFound;
            }
            ViewBag.path = path;
            return View();

        }
        /// <summary>
        /// Uplaod images
        /// </summary>
        /// <param name="files"></param>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files, FormCollection formCollection)
        {
            var lstFileName = new List<string>();
            foreach (var item in files)
            {
                if (item != null)
                    lstFileName.Add(item.FileName);
            }
            TempData.Add("ListFileName", lstFileName);
            try
            {

                if (1 == 1) { }

                var pos = formCollection["position"];
                pos = pos != null && pos.Length > 0 && pos[pos.Length - 1] != '\\' ? pos + "\\" : pos;

                TempData.Add("FolderName", formCollection["position"]);
                if (files[0] != null)
                {
                    foreach (var image in files)
                    {
                        // Get extension
                        string extensionFile = Path.GetExtension(image.FileName);
                        string subFileName = image.FileName.Substring(0, image.FileName.Length - 4);
                        //Declare a new dictionary to store the parameters for the image versions.
                        var versions = new Dictionary<string, string>();

                        var path = Server.MapPath("~/Media/Source/" + pos);
                        if (!Directory.Exists(path))
                        {
                            ViewBag.Error = MsgError;
                            return RedirectToAction("Index");
                        }
                        //Define the versions to generate
                        versions.Add("", string.Format("maxwidth=5000&maxheight=5000&format={0}", extensionFile));

                        //Generate each version
                        foreach (var suffix in versions.Keys)
                        {
                            image.InputStream.Seek(0, SeekOrigin.Begin);

                            //Let the image builder add the correct extension based on the output file type
                            ImageBuilder.Current.Build(
                              new ImageJob(
                                image.InputStream,
                                path + subFileName + suffix,
                                new Instructions(versions[suffix]),
                                false,
                                true));
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
                throw new Exception("");
            }
        }
        /// <summary>
        /// Delete Images
        /// </summary>
        /// <param name="imagesFileName"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteImage(string imagesFileName, string folderName)
        {
            try
            {
                bool deleted = false;
                var pos = TempData["FolderName"];
                if (imagesFileName != null)
                {

                    foreach (var imageFileName in imagesFileName.Split(','))
                    {
                        var imageName = "";
                        imageName = imageFileName;
                        string fullPath = Request.MapPath("~/Media/Source/" + folderName + "/" + imageName);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    deleted = true;
                }
                return Json(new { success = deleted, image = imagesFileName, folderName = folderName });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message, imagesFileName = imagesFileName });
            }
        }
        private string CreateNewName(string extension, string typeMedia)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + typeMedia + "_" + Guid.NewGuid().ToString() + extension;
        }

    }
}
