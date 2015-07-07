using HTTelecom.Domain.Core.DataContext.acs;
using HTTelecom.Domain.Core.Repository.acs;
using HTTelecom.WebUI.MediaSupport.Filters;
using HTTelecom.WebUI.MediaSupport.ViewModels;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MSSRepository.Common;
using HTTelecom.Domain.Core.DataContext.ams;
using System.Text.RegularExpressions;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.mss;
#region INfo
/*
 * ======================================================================================================
 * @File name: ACSController.cs
 * @Author:
 *          1. vinhphu24191
 * @Creation Date: 23/04/2015
 * @Description: Controller of Account in AMS0001 project
 * @List Action Method (key:AM):
 *          1. ActionResult AdsContentIndex(int? page,string search, int? filter);
 *          2. public ActionResult AdsContentEdit(string tabActive, long id)
 *          3. public ActionResult AdsContentEdit(AdsContent _adsContent, HttpPostedFileBase productHBFile)
  
 * @List Common Method (key:CM): 
 *              1. bool ValidateFormAdsContentEdit(AdsContent _adsct)
 *              2. string CreateNewName(string extension, string typeMedia)
 * @Update History:
 * ---------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------
 *   Last Submit Date   ||  Originator          ||  Description
 * ---------------------------------------------------------------------------------------------------
 *   24/04/2015         || vinhphu24191         ||   AM(1,2,3) CM(1,2)
 *                      ||                      ||  
 *                      ||                      ||   
 *                      ||                      ||
 * ===================================================================================================
 */
#endregion
namespace HTTelecom.WebUI.MediaSupport.Controllers
{
    [SessionLoginFilter]
    public class ACSController : Controller
    {
        private const int pageSizeDefault = 10;
        private const int pageNumDefault = 1;
        private const string MallB_MediaTypeCode = "MALL-1";

        #region Action Method:

        #region Mall Ads
        public ActionResult MallAdsIndex(int? page, int? pageSizeSelected, string search, int? filter)
        {
            AdsRepository _iAdsService = new AdsRepository();
            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            ViewBag.page = page;
            ViewBag.pageSizeSelected = pageSize;
            ViewBag.filter = filter;
            ViewBag.search = search ?? "";
            ViewBag.SideBarMenu = "MallAdsIndex";

            IList<Ad> lst_Ads = _iAdsService.GetList_AdsAll().Where(a => Regex.IsMatch(a.ReservationCode, ViewBag.search)).ToList();
            IPagedList<Ad> lst_AdsPaged = new PagedList<Ad>(lst_Ads, pageNum, pageSize);
            return View(lst_AdsPaged);
        }
        #endregion

        #region 3. Action Method: Mall Ads Create
        public ActionResult MallAdsCreate()
        {
            return View(new AdsContent());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MallAdsCreate(AdsContent AdsCollection, HttpPostedFileBase mallAdsBImageFile)
        {
            if (ValidateMallAds(AdsCollection, mallAdsBImageFile))
            {
                AdsContentRepository _iAdsService = new AdsContentRepository();
                Account accOnline = (Account)Session["Account"];
                AdsCollection.ModifiedBy = accOnline.AccountId;

                long adsContentId = _iAdsService.Create(AdsCollection);


                string createMallAdsFolder = Path.Combine(Server.MapPath("~/Media/"), "MallAds");
                if (!System.IO.Directory.Exists(createMallAdsFolder))
                {
                    Directory.CreateDirectory(createMallAdsFolder);
                }
                string createMallAdFolder = Path.Combine(createMallAdsFolder, "Ads" + AdsCollection.AdsContentId);// create folder Product

                if (!System.IO.Directory.Exists(createMallAdFolder))
                {
                    Directory.CreateDirectory(createMallAdFolder);
                }
                if (mallAdsBImageFile != null)
                {
                    MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
                    MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(MallB_MediaTypeCode);
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(mallAdsBImageFile.FileName), mt.MediaTypeCode);
                    string fullPathName = Path.Combine(createMallAdFolder, nameImage); //path full
                    var rs = ImageHelper.UploadImage(mallAdsBImageFile, fullPathName, Convert.ToInt32(mt.RWidth), Convert.ToInt32(mt.RHeight));
                    //===============================================

                    if (rs.Item1 == false)
                    {
                        ModelState.AddModelError("imageAdsFileDimension", mallAdsBImageFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                    }
                    else
                    {
                        string imagePath = "/Media/MallAds/Ads" + AdsCollection.AdsContentId + "/" + nameImage;
                        _iAdsService.Update(AdsCollection.AdsContentId, imagePath);
                    }
                }


                TempData["StatusMessage"] = 1; //1: Success
                return RedirectToAction("MallAdsIndex", "ACS");
            }
            else
            {
                ViewBag.TabIsActive = "tabone";
                return View(AdsCollection);
            }
        }
        #endregion

        #region 2. Action Method: MallAdsEdit
        public ActionResult MallAdsEdit(string tabActive, long id)
        {
            AdsContentRepository _iAdsService = new AdsContentRepository();
            AdsContent AdsPage = _iAdsService.GetById(id);
            ViewBag.SideBarMenu = "MallAdsIndex";

            return View(AdsPage);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MallAdsEdit(AdsContent AdsCollection, HttpPostedFileBase mallAdsBImageFile, int? saveType)
        {
            if (ValidateMallAds(AdsCollection, mallAdsBImageFile))
            {
                AdsContentRepository _iAdsService = new AdsContentRepository();
                Account accOnline = (Account)Session["Account"];
                AdsCollection.ModifiedBy = accOnline.AccountId;

                bool updateStatus = _iAdsService.Update(AdsCollection);


                string createMallAdsFolder = Path.Combine(Server.MapPath("~/Media/"), "MallAds");
                if (!System.IO.Directory.Exists(createMallAdsFolder))
                {
                    Directory.CreateDirectory(createMallAdsFolder);
                }
                string createMallAdFolder = Path.Combine(createMallAdsFolder, "Ads" + AdsCollection.AdsContentId);// create folder Product

                if (!System.IO.Directory.Exists(createMallAdFolder))
                {
                    Directory.CreateDirectory(createMallAdFolder);
                }
                if (mallAdsBImageFile != null)
                {
                    MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
                    MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(MallB_MediaTypeCode);
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(mallAdsBImageFile.FileName), mt.MediaTypeCode);
                    string fullPathName = Path.Combine(createMallAdFolder, nameImage); //path full
                    var rs = ImageHelper.UploadImage(mallAdsBImageFile, fullPathName, Convert.ToInt32(mt.RWidth), Convert.ToInt32(mt.RHeight));
                    //===============================================

                    if (rs.Item1 == false)
                    {
                        ModelState.AddModelError("imageAdsFileDimension", mallAdsBImageFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                    }
                    else
                    {
                        string imagePath = "/Media/MallAds/Ads" + AdsCollection.AdsContentId + "/" + nameImage;
                        _iAdsService.Update(AdsCollection.AdsContentId, imagePath);
                    }
                }


                TempData["StatusMessage"] = 1; //1: Success
                return RedirectToAction("MallAdsIndex", "ACS");
            }
            else
            {
                ViewBag.TabIsActive = "tabone";
                return View(AdsCollection);
            }
        }
        #endregion

        #region ChangeAdsIsActive
        public ActionResult ChangeAdsIsActive(List<long> listAdsId, int sbool, int? page, int? pageSizeSelected)
        {
            AdsRepository _iAdsService = new AdsRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            bool updateStatus = false;
            foreach (var adsId in listAdsId)
            {
                var brandUpdateIsActive = _iAdsService.GetById(adsId);
                if (sbool == -1)
                {
                    updateStatus = _iAdsService.UpdateIsActive(adsId, (brandUpdateIsActive.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iAdsService.UpdateIsActive(adsId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iAdsService.UpdateIsActive(adsId, true);
                }
            }
            if (updateStatus == true)
            {
                IPagedList<Ad> lst_Ads = _iAdsService.GetList_AdsAll(pageNum, pageSize);

                return Json(new { _listAds = lst_Ads, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        

        #endregion

        #region Common Method:
        #region 1. Common Method: ValidateMallAds
        //Description: load mall page
        private bool ValidateMallAds(AdsContent adsCollection, HttpPostedFileBase mallAdsFile)
        {
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            bool valid = true;

            if (mallAdsFile != null)
            {
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };

                MediaType mediaType_MallB = _iMediaTypeService.Get_MediaTypeByCode(MallB_MediaTypeCode);
                if (!AllowedFileExtensions.Contains(mallAdsFile.FileName.Substring(mallAdsFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("mallMediaFilesType", mallAdsFile.FileName + " not image type. Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (mallAdsFile.ContentLength / 1024 > mediaType_MallB.MaxSize)
                {
                    ModelState.AddModelError("mallMediaFilesMaxSize", mallAdsFile.FileName + " is too large, maximum allowed size is : " + mediaType_MallB.MaxSize.ToString() + "KB");
                    valid = false;
                }
                if (mallAdsFile.ContentLength / 1024 < mediaType_MallB.MinSize)
                {
                    ModelState.AddModelError("mallMediaFilesMinSize", mallAdsFile.FileName + " is too small, minimize allowed size is : " + mediaType_MallB.MinSize.ToString() + "KB");
                    valid = false;
                }
            }
            if (adsCollection.AdsHeader == null || adsCollection.AdsHeader == "")
            {
                ModelState.AddModelError("AdsHeader", "Header is empty !!");
                return false;
            }

            if (adsCollection.AdsTitle == null || adsCollection.AdsTitle == "")
            {
                ModelState.AddModelError("AdsTitle", "AdsTitle is empty !!");
                return false;
            }
            if (adsCollection.Description == null || adsCollection.Description == "")
            {
                ModelState.AddModelError("Description", "Description is empty !!");
                return false;
            }
            if (adsCollection.LinkSite == null || adsCollection.LinkSite == "")
            {
                ModelState.AddModelError("LinkSite", "LinkSite is empty !!");
                return false;
            }

            return valid;
        }
        #endregion
        private string CreateNewName(string extension, string typeMedia)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + typeMedia + "_" + Guid.NewGuid().ToString() + extension;
        }
        #endregion

    }
}
