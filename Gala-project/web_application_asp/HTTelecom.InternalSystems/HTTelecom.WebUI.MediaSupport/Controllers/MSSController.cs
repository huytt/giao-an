using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.MediaSupport.Filters;
using HTTelecom.WebUI.MediaSupport.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PagedList;
using PagedList.Mvc;
using System.IO;
using MSSRepository.Common;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.AccessControl;
using System.Globalization;
using System.Text;
using HTTelecom.Domain.Core.IRepository.mss;
using System.Net.Mail;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using HTTelecom.Domain.Core.Repository.ams;
using System.Text.RegularExpressions;
#region INfo
/*
 * ======================================================================================================
 * @File name: MSSController.cs
 * @Author:
 *          1. Thientb
 * @Creation Date: 27/03/2015
 * @Description: Controller of Account in AMS0001 project
 * @List Action Method (key:AM):
 *          1. ActionResult Index();
 *          2. Action Method: Add
 *              2.1 ActionResult Add();
 *              2.2 ActionResult Add(AccountForm createForm);
 *          3. Action Method: Edit
 *              3.1 ActionResult Edit(string sidebarMenu, long accountId);
 *              3.2 ActionResult Edit(AccountForm editForm);
 *          4. JsonResult LoadDataListSystemPermission(long systemId, long accountId);
 *          5. JsonResult ChangeActiveAccountRole(List<long> listAccountRoleId);
 *          6. JsonResult ChangeActiveSystemPermission(List<long> listAccountRoleId, long systemId, long accountId);
 *          7. Action Method: Login
 *              7.1 ActionResult Login();
 *              7.2 ActionResult Login(LoginForm loginForm);
 *          8. ActionResult Logout();
 *          9. ActionResult GiftIndex(int ? page);
 *         10. ActionResult GiftCreate(Gift GiftCollection);
 *         11. ActionResult GiftCreate();
 *         12. ActionResult GiftEdit(int id);
 *         13. ActionResult GiftEdit(Gift GiftColecttion)
 *         14. ActionResult CategoryCreate();
 *         15. ActionResult CategoryIndex();
 *         16. ActionResult CategoryEdit();
 * @List Common Method (key:CM): 
 *              1. void GetList_DepartmentDropDownList(long selected, bool isDelete);
 *              2. void GetList_DepartmentGroupDropDownList(long selected, bool isDelete);
 *              3. void GetList_OrgRoleDropDownList(long selected, bool isDelete);
 *              4. IList<SystemPermissionFull> CastListSystemPermission(long systemId, long accountId);
 *              5. IList<AccountFull> CastListAccount();
 *              6. IList<SystemPermissionFull> CastSystemPermission(IList<SystemPermission> lst_SystemPermission);
 *              7. ActionResult GetProductPage(string keyword);
 *              8. string ReplaceAll(string input);
 *              9. bool ValidateCategoryForm(Category CateCollection);
 *             10. void LoadCategoryFromPage(long _cateId);
 * @Update History:
 * ---------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------
 *   Last Submit Date   ||  Originator          ||  Description
 * ---------------------------------------------------------------------------------------------------
 *   13/03/2015         ||  Thientb             ||  create AM(1,2,3,4,5,6,7,8) CM(1,2,3,4,5) 
 *   24/04/2015         ||  vinhphu24191        ||  create AM(9,10,11,12,13) CM(7,8,9,10)
 *                      ||                      ||   
 *                      ||                      ||
 * ===================================================================================================
 */
#endregion
namespace HTTelecom.WebUI.MediaSupport.Controllers
{
    [SessionLoginFilter]
    public class MSSController : Controller
    {
        private int pageNumDefault = 1;
        private int pageSizeDefault = 50;

        private const string STOREHIGHLIGHT_MEDIATYPECODE = "MALL-2";
        private const string STORECOMPLEXNAME_MEDIATYPECODE = "STORE-1";
        private const string STOREBANNER_MEDIATYPECODE = "STORE-2";
        private const string PRODUCTHIGHLIGHT_MEDIATYPECODE = "STORE-3";
        private const string PRODUCT_COLOUR_MEDIATYPECODE = "PRODUCT_COLOUR";
        private const string PRODUCTBANNER_MEDIATYPECODE = "PRODUCT-2";
        private const string PRODUCTVIDEO_MEDIATYPECODE = "PRODUCT-3";
        private const string PRODUCTEMBED_MEDIATYPECODE = "PRODUCT-4";
        private const string BRANDLOGO_MEDIATYPECODE = "BRAND-1";
        private const string BRANDBANNER_MEDIATYPECODE = "BRAND-2";
        private const string CATEGORYLOGO_MEDIATYPECODE = "CATEGORY-1";
        private const string CATEGORYBANNER_MEDIATYPECODE = "CATEGORY-2";
        private const string GIFTBANNER_MEDIATYPECODE = "GIFT-1";
        public List<Category> _listCategorySortForParentID;
        private string ServerFolder
        {
            get
            {
                return Path.Combine(Server.MapPath("/Media/Store"));
            }
        }

        #region Action Method (key:AM):

        #region Dashboard
        #region 1. Action Method: Dashboard
        //Description: display Index
        public ActionResult Dashboard()
        {
            CustomerRepository _iCustomerService = new CustomerRepository();
            ViewBag.RegisteredCount = _iCustomerService.GetAllCustomer().Where(a => a.IsActive == true & a.IsDeleted == false).ToList().Count;
            return View();
        }


        #endregion
        #endregion

        #region Store

        #region 1. Action Method: StoreIndex
        //Description: display Index
        public ActionResult StoreIndex(int? page, int? pageSizeSelected)
        {
            StoreRepository _iStoreService = new StoreRepository();
            VendorRepository _iVendorService = new VendorRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            ViewBag.page = page;
            ViewBag.pageSizeSelected = pageSize;

            var lst_storePage = _iStoreService.GetList_StoreAll(pageNum, pageSize);

            this.LoadStoreFormPage(0);

            ViewBag.SideBarMenu = "StoreIndex";
            return View(lst_storePage);
        }

        //Description: display Index
        public ActionResult StoreIndexConfirm(int? page, int? pageSizeSelected)
        {
            StoreRepository _iStoreService = new StoreRepository();
            VendorRepository _iVendorService = new VendorRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            ViewBag.page = page;
            ViewBag.pageSizeSelected = pageSize;

            var lst_storePage = _iStoreService.GetList_StoreAll(pageNum, pageSize);

            this.LoadStoreFormPage(0);

            ViewBag.SideBarMenu = "StoreIndexConfirm";
            return View(lst_storePage);
        }
        #endregion

        #region 2. Action Method: StoreCreate
        //Description: display Index
        public ActionResult StoreCreate()
        {
            //Contract
            this.LoadStoreFormPage(0);
            ViewBag.SideBarMenu = "StoreIndex";
            return View(new Store());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult StoreCreate(Store StoreCollection)
        {
            StoreRepository _iStoreService = new StoreRepository();
            Account accOnline = (Account)Session["Account"];
            if (ValidateStoreForm(StoreCollection, null, null, null, 1) == true)
            {
                StoreCollection.CreatedBy = accOnline.AccountId;
                long storeId = _iStoreService.Insert(StoreCollection);
                if (storeId != -1) //Insert success
                {
                    TempData["StatusMessage"] = 2; //1: Success
                    TempData["storeId"] = StoreCollection.StoreCode;
                    TempData["storeName"] = StoreCollection.StoreName;
                    ViewBag.SideBarMenu = "StoreIndex";
                    return RedirectToAction("StoreIndex", "MSS");
                }
            }
            this.LoadStoreFormPage(0);
            ViewBag.SideBarMenu = "StoreIndex";
            TempData["StatusMessage"] = 0; //0: Error
            return View(StoreCollection);
        }

        #endregion

        #region 3. Action Method: StoreEdit
        //Description: display Index
        public ActionResult StoreEdit(long id, int? page, int? filter, string ptype, string tabActive, int? pageSizeSelected)
        {
            StoreRepository _iStoreService = new StoreRepository();
            ProductRepository _iProductService = new ProductRepository();
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            ViewBag.page = page;
            ViewBag.filter = filter;
            ViewBag.ptype = ptype;

            Store Store = _iStoreService.Get_StoreById(id);
            AccountRepository _AccountRepository = new AccountRepository();
            if (Store.CreatedBy != null)
            {
                var acc = _AccountRepository.Get_AccountById(Convert.ToInt64(Store.CreatedBy));
                ViewBag.CreateBy = Store.CreatedBy;
                ViewBag.CreateByName = acc != null ? acc.FullName : "";
            }
            IList<Product> lst_ProductByStore = _iProductService.GetList_Product_StoreId(id);

            LoadProductFormPage(0);
            

            if (ptype == "All")
            {
                lst_ProductByStore = _iProductService.GetList_Product_StoreId(id);
            }
            else
            {
                if (ptype == null)
                {
                    lst_ProductByStore = _iProductService.GetList_Product_StoreId(id);
                }
                else
                {
                    lst_ProductByStore = _iProductService.GetList_Product_StoreId_ProductTypeCode(id, ptype);
                }
            }

            if (Store.VendorId != null)
            {
                ViewBag.list_VendorAddress = _iVendorAddressService.GetList_VendorAddress_VendorId(Store.VendorId).ToList();
            }
            else
            {
                ViewBag.list_VendorAddress = new List<VendorAddress>();
            }


            ViewBag.ProductPager = lst_ProductByStore.ToPagedList(pageNum, pageSize);

            LoadStoreFormPage(id);
            ViewBag.SideBarMenu = "StoreIndex";
            if (tabActive != null)
            {
                TempData["tabActive"] = tabActive;
            }
            return View((Store == null ? new Store() : Store));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult StoreEdit(int? page, Store StoreCollection, HttpPostedFileBase storeHBFile, HttpPostedFileBase storeCNBFile, List<HttpPostedFileBase> storeBFiles, int? saveType, FormCollection formData)
        {
            StoreRepository _iStoreService = new StoreRepository();
            StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
            ProductRepository _iProductService = new ProductRepository();
            ViewBag.CreateBy = Convert.ToInt64(formData["CreateBy"]);
            //Directory.get(".");
            Account accOnline = (Account)Session["Account"];
            int pageNum = (page ?? 1);
            ViewBag.page = page;
            IPagedList<Product> lst_ProductByStore = _iProductService.GetList_Product_StoreId(StoreCollection.StoreId, pageNum, 50);

            if (ValidateStoreForm(StoreCollection, storeHBFile, storeCNBFile, storeBFiles, 2) == true)
            {
                List<Media> lst_MediaInsert = new List<Media>();

                StoreCollection.ModifiedBy = accOnline.AccountId;


                bool kq = _iStoreService.Update(StoreCollection);
                if (kq == true) //Insert success
                {
                    IList<StoreInMedia> lst_StoreInMedia = _iStoreInMediaService.GetList_StoreInMedia_StoreId(StoreCollection.StoreId).ToList();
                    IList<Media> lst_MediaSHB = new List<Media>();
                    IList<Media> lst_MediaSB = new List<Media>();
                    IList<Media> lst_MediaSCNB = new List<Media>();
                    bool ValidImage = true;

                    foreach (var item in lst_StoreInMedia)
                    {
                        Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                        if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREHIGHLIGHT_MEDIATYPECODE).MediaTypeId)
                        {
                            lst_MediaSHB.Add(media);
                        }
                        if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREBANNER_MEDIATYPECODE).MediaTypeId)
                        {
                            lst_MediaSB.Add(media);
                        }
                        if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STORECOMPLEXNAME_MEDIATYPECODE).MediaTypeId)
                        {
                            lst_MediaSCNB.Add(media);
                        }
                    }

                    string ImageUploadsFolder = Path.Combine(Server.MapPath("~/Media/Store/"));
                    string createFolder = Path.Combine(ImageUploadsFolder, "S" + StoreCollection.StoreCode);// create folder store

                    if (!System.IO.Directory.Exists(createFolder))
                    {
                        Directory.CreateDirectory(createFolder);
                    }

                    if (storeHBFile != null)
                    {
                        MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(STOREHIGHLIGHT_MEDIATYPECODE);
                        //===============================================
                        String nameImage = CreateNewName(Path.GetExtension(storeHBFile.FileName), mt.MediaTypeCode);
                        string fullPathName = Path.Combine(createFolder, nameImage); //path full
                        var rs = ImageHelper.UploadImage(storeHBFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                        //===============================================
                        if (rs.Item1 == false)
                        {
                            ModelState.AddModelError("storeHBFileDimension", storeHBFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                            ValidImage = false;
                        }
                        else
                        {
                            Media mediaCurrent = lst_MediaSHB.FirstOrDefault();

                            if (mediaCurrent == null)
                            {
                                Media media = new Media();

                                media.MediaTypeId = mt.MediaTypeId;
                                media.MediaName = nameImage;
                                media.Url = "/Media/Store/S" + StoreCollection.StoreCode + "/";
                                lst_MediaInsert.Add(media);
                            }
                            else
                            {
                                mediaCurrent.MediaName = nameImage;

                                _iMediaService.Update(mediaCurrent);
                            }
                        }
                    }
                    if (storeCNBFile != null)
                    {
                        MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(STORECOMPLEXNAME_MEDIATYPECODE);
                        //===============================================
                        String nameImage = CreateNewName(Path.GetExtension(storeCNBFile.FileName), mt.MediaTypeCode);
                        string fullPathName = Path.Combine(createFolder, nameImage); //path full
                        var rs = ImageHelper.UploadImage(storeCNBFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                        //===============================================

                        if (rs.Item1 == false)
                        {
                            ModelState.AddModelError("storeCNBFileDimension", storeCNBFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                            ValidImage = false;
                        }
                        else
                        {
                            Media mediaCurrent = lst_MediaSCNB.FirstOrDefault();

                            if (mediaCurrent == null)
                            {
                                Media media = new Media();

                                media.MediaTypeId = mt.MediaTypeId;
                                media.MediaName = nameImage;
                                media.Url = "/Media/Store/S" + StoreCollection.StoreCode + "/";
                                lst_MediaInsert.Add(media);
                            }
                            else
                            {
                                mediaCurrent.MediaName = nameImage;

                                _iMediaService.Update(mediaCurrent);
                            }
                        }


                    }
                    if (storeBFiles != null && storeBFiles[0] != null)
                    {
                        MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(STOREBANNER_MEDIATYPECODE);
                        foreach (HttpPostedFileBase file in storeBFiles)
                        {
                            //===============================================
                            String nameImage = CreateNewName(Path.GetExtension(file.FileName), mt.MediaTypeCode);
                            string fullPathName = Path.Combine(createFolder, nameImage); //path full
                            var rs = ImageHelper.UploadImage(file, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                            //===============================================
                            if (rs.Item1 == false)
                            {
                                ModelState.AddModelError("storeBFileDimension", file.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                                ValidImage = true;
                            }
                            else
                            {
                                List<Media> lst_mediaCurrent = lst_MediaSB.Where(a => a.IsActive == false).Take(storeBFiles.Count()).ToList();

                                if (lst_mediaCurrent.Count == 0)
                                {
                                    Media media = new Media();

                                    media.MediaTypeId = mt.MediaTypeId;
                                    media.MediaName = nameImage;
                                    media.Url = "/Media/Store/S" + StoreCollection.StoreCode + "/";

                                    lst_MediaInsert.Add(media);
                                }
                                else
                                {
                                    foreach (var item in lst_mediaCurrent)
                                    {
                                        item.MediaName = nameImage;
                                        item.IsActive = true;

                                        _iMediaService.Update(item);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    foreach (var item in lst_MediaInsert)
                    {
                        long mediaId = _iMediaService.Insert(item);

                        if (mediaId != 0)
                        {
                            StoreInMedia sim = new StoreInMedia();
                            sim.StoreId = StoreCollection.StoreId;
                            sim.MediaId = mediaId;
                            _iStoreInMediaService.Insert(sim);
                        }
                    }

                    if (saveType != 1)
                    {
                        this.ShowStoreWarning(StoreCollection);
                        TempData["StatusMessage"] = 2; //2: Success

                        TempData["storeId"] = StoreCollection.StoreCode;
                        ViewBag.SideBarMenu = "StoreIndex";
                        return RedirectToAction("StoreIndex", "MSS");
                    }
                    else
                    {
                        if (ValidImage == true)
                        {
                            TempData["StatusMessage"] = 1; //1: Warning
                            ViewBag.ProductPager = lst_ProductByStore;
                            ViewBag.list_VendorAddress = _iVendorAddressService.GetList_VendorAddress_VendorId(StoreCollection.VendorId).ToList();
                            LoadStoreFormPage(StoreCollection.StoreId);
                            LoadProductFormPage(0);
                            ViewBag.SuccessAlert = "SuccessAlert";
                            if (storeHBFile != null || storeCNBFile != null)
                            {
                                TempData["tabActive"] = "tabthree";
                            }
                            this.ShowStoreWarning(StoreCollection);
                            ViewBag.SideBarMenu = "StoreIndex";
                            return View(StoreCollection);
                        }
                        else
                        {
                            ViewBag.ProductPager = lst_ProductByStore;
                            LoadProductFormPage(0);
                            ViewBag.list_VendorAddress = _iVendorAddressService.GetList_VendorAddress_VendorId(StoreCollection.VendorId).ToList();
                            LoadStoreFormPage(StoreCollection.StoreId);
                            TempData["StatusMessage"] = 0; //0: Error
                            ViewBag.SideBarMenu = "StoreIndex";
                            if (storeHBFile != null || storeCNBFile != null)
                            {
                                TempData["tabActive"] = "tabthree";
                            }
                            return View(StoreCollection);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("UpdalteStatus", "There was an error occurs !!");
                    ViewBag.ProductPager = lst_ProductByStore;
                    LoadProductFormPage(0);
                    ViewBag.list_VendorAddress = _iVendorAddressService.GetList_VendorAddress_VendorId(StoreCollection.VendorId).ToList();
                    LoadStoreFormPage(StoreCollection.StoreId);
                    TempData["StatusMessage"] = 0; //0: Error
                    ViewBag.SideBarMenu = "StoreIndex";
                    if (storeHBFile != null || storeCNBFile != null)
                    {
                        TempData["tabActive"] = "tabthree";
                    }
                    return View(StoreCollection);
                }
            }

            ViewBag.ProductPager = lst_ProductByStore;
            LoadProductFormPage(0);
            ViewBag.list_VendorAddress = _iVendorAddressService.GetList_VendorAddress_VendorId(StoreCollection.VendorId).ToList();
            LoadStoreFormPage(StoreCollection.StoreId);
            TempData["StatusMessage"] = 0; //0: Error
            ViewBag.SideBarMenu = "StoreIndex";
            if (storeHBFile != null || storeCNBFile != null)
            {
                TempData["tabActive"] = "tabthree";
            }

            this.GetAccountByName(ViewBag.CreateBy == null ? null : Convert.ToInt64(ViewBag.CreateBy));
            return View(StoreCollection);
        }

        private string GetAccountByName(long accountId)
        {
            AccountRepository _AccountRepository = new AccountRepository();

            var acc = _AccountRepository.Get_AccountById(Convert.ToInt64(accountId));

            return acc != null ? acc.FullName : "N/A";
        }


        #endregion

        #region 1. Json Method: ChangeStoreVerified
        //Description: display Index
        public JsonResult ChangeStoreVerified(List<long> listStoreId, int sbool, int? page, int? pageSizeSelected)
        {
            StoreRepository _iStoreService = new StoreRepository();
            bool updateStatus = false;
            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            foreach (var storeId in listStoreId)
            {
                var storeUpdateVerified = _iStoreService.Get_StoreById(storeId);
                if (sbool == -1)
                {
                    updateStatus = _iStoreService.UpdateVerified(storeId, (storeUpdateVerified.IsVerified == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iStoreService.UpdateVerified(storeId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iStoreService.UpdateVerified(storeId, true);
                }
            }

            if (updateStatus == true)
            {
                IPagedList<Store> lst_Store = _iStoreService.GetList_StoreAll(pageNum, pageSize);

                return Json(new { _listStore = lst_Store, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 1. Json Method: ChangeStoreActive
        //Description: display Index
        public JsonResult ChangeStoreActive(List<long> listStoreId, int sbool, int? page, int? pageSizeSelected)
        {
            StoreRepository _iStoreService = new StoreRepository();
            VendorRepository _iVendorService = new VendorRepository();
            int pageNum = (page ?? 1);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            Tuple<bool, string> updateStatus = null;
            foreach (var storeId in listStoreId)
            {
                var productUpdateVerified = _iStoreService.Get_StoreById(storeId);
                if (sbool == -1)
                {
                    updateStatus = _iStoreService.UpdateActive(storeId, (productUpdateVerified.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iStoreService.UpdateActive(storeId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iStoreService.UpdateActive(storeId, true);
                }
            }
            if (updateStatus.Item1 == true)
            {

                IPagedList<Store> lst_Store = _iStoreService.GetList_StoreAll(pageNum, pageSize);
                //string storeCode = _iStoreService.Get_StoreById(storeId);

                return Json(new
                {
                    _listStore = lst_Store,
                    Success = true,
                    Message = "OK!",
                    code = updateStatus.Item2
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 1. Json Method: LoadListVendorAddress
        //Description: display Index
        public JsonResult LoadListVendorAddress(long vendorId)
        {
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
            VendorRepository _iVendorService = new VendorRepository();

            IList<VendorAddress> lst_VendorAddress = _iVendorAddressService.GetList_VendorAddress_VendorId(vendorId).ToList();


            return Json(new { _listVendorAddress = lst_VendorAddress, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 1. Json Method: LoadVendorAddress
        //Description: display Index
        public JsonResult LoadVendorAddress(long vendorAddressId)
        {
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
            VendorAddress vendorAddress = _iVendorAddressService.Get_VendorAddressById(vendorAddressId);
            return Json(new { _vendorAddress = vendorAddress, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region Product
        #region 1. Action Method: ProductIndex
        //Description: display Index

        public ActionResult ProductIndex(int? page, int? filter, string ptype, int? pageSizeSelected)
        {
            ProductRepository _iProductService = new ProductRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);

            ViewBag.pageNum = page;
            ViewBag.pageSizeSelected = pageSize;
            ViewBag.filter = filter;
            ViewBag.ptype = ptype;
            LoadProductFormPage(0);

            if (ptype == null) ptype = "All";
            IPagedList<Product> lst_ProductPager = new PagedList<Product>(new List<Product>(), 1, pageSize);

            if (ptype.Equals("All"))
            {
                lst_ProductPager = _iProductService.GetList_ProductAll(pageNum, pageSize);
            }
            else
            {
                lst_ProductPager = _iProductService.GetList_Product_ProductTypeCode(ptype, pageNum, pageSize);
            }

            ViewBag.SideBarMenu = "ProductIndex";
            return View(lst_ProductPager);
        }
        // add by Vannl 31/07/2015.
        public ActionResult ProductIndexConfirm(int? page, int? filter, string ptype, int? pageSizeSelected)
        {
            ProductRepository _iProductService = new ProductRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);

            ViewBag.pageNum = page;
            ViewBag.pageSizeSelected = pageSize;
            ViewBag.filter = filter;
            ViewBag.ptype = ptype;
            LoadProductFormPage(0);

            if (ptype == null) ptype = "All";
            IPagedList<Product> lst_ProductPager = new PagedList<Product>(new List<Product>(), 1, pageSize);

            if (ptype.Equals("All"))
            {
                lst_ProductPager = _iProductService.GetList_ProductAll(pageNum, pageSize);
            }
            else
            {
                lst_ProductPager = _iProductService.GetList_Product_ProductTypeCode(ptype, pageNum, pageSize);
            }

            ViewBag.SideBarMenu = "ProductIndex";
            return View(lst_ProductPager);
        }
        #endregion

        #region 2. Action Method: ProductCreate
        //Description: display Index
        public ActionResult ProductCreate()
        {
            LoadProductFormPage(0);
            ViewBag.SideBarMenu = "ProductIndex";
            return View(new Product());

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductCreate(Product ProductCollection)
        {
            ProductRepository _iProductService = new ProductRepository();
            ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();

            if (ValidateProductForm(ProductCollection, null, null, null, null, null, null, null, null) == true)
            {
                Account accOnline = (Account)Session["Account"];
                ProductCollection.CreatedBy = accOnline.AccountId;

                long productId = _iProductService.Insert(ProductCollection);
              TempData["productComplexName"] = _iProductService.Get_ProductById(productId).ProductComplexName;
                if (productId != -1)
                {
                    TempData["StatusMessage"] = 2; //1: Success
                    return RedirectToAction("ProductIndex", "MSS");
                }
                else
                {
                    ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
                    LoadProductFormPage(0);
                    ViewBag.SideBarMenu = "ProductIndex";
                    return View(ProductCollection);
                }
            }
            else
            {
                LoadProductFormPage(0);
                ViewBag.SideBarMenu = "ProductIndex";
                return View(ProductCollection);
            }
        }
        #endregion

        #region 2. Action Method: ProductEdit
        //Description: display Index
        public ActionResult ProductEdit(string tabActive, long id)
        {
            ViewBag.u = HttpContext.Request.Url.AbsoluteUri;
            ProductRepository _iProductService = new ProductRepository();
            Product productPage = _iProductService.Get_ProductById(id);
            LoadProductFormPage(id);
            ViewBag.CreateBy = productPage.CreatedBy;
            ViewBag.SideBarMenu = "ProductIndex";
            if (tabActive != null)
            {
                TempData["tabActive"] = tabActive;
            }

            return View((productPage == null ? new Product() : productPage));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ProductEdit(Product ProductCollection, List<long> CategoryId, HttpPostedFileBase productHBFile, HttpPostedFileBase productColourFile, List<HttpPostedFileBase> productBFiles, HttpPostedFileBase VideoFile, string txtBrochureVideo, int? saveType, FormCollection formData, string newColour)
        {
            SizeGlobalRepository _iSizeGlobalService = new SizeGlobalRepository();
            SizeRepository _iSizeService = new SizeRepository();

            ProductCollection.Size = formData["Size"];
            string sizeForm = ProductCollection.Size.Split(',').Select(sValue => sValue.Trim()).FirstOrDefault();
            HTTelecom.Domain.Core.DataContext.mss.Size size = _iSizeService.Get_Size_SizeId(Convert.ToInt64(sizeForm));
            ProductCollection.SizeGlobal = _iSizeGlobalService.Get_SizeGlobal_SizeGlobalId((long)size.SizeGlobalId).SizeGlobalId.ToString();
            ViewBag.u = HttpContext.Request.Url.AbsoluteUri;
            ProductRepository _iProductService = new ProductRepository();
            ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();
            StoreRepository _iStoreService = new StoreRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            if (ValidateProductForm(ProductCollection, CategoryId, productHBFile, productColourFile, productBFiles, VideoFile, txtBrochureVideo, newColour, saveType) == true)
            {
                Account accOnline = (Account)Session["Account"];
                Store store = _iStoreService.Get_StoreById((long)ProductCollection.StoreId);
                ProductCollection.ModifiedBy = accOnline.AccountId;

                if (CategoryId != null)
                {
                    foreach (var item in CategoryId)
                    {
                        ProductInCategory pic = new ProductInCategory();
                        pic.ProductId = ProductCollection.ProductId;
                        pic.CategoryId = item;
                        long producInCategoryId = _iProductInCategoryService.Insert(pic);
                    }
                }
                bool ValidImage = true;
                string createProductsFolder = Path.Combine(Server.MapPath("~/Media/Store/S" + store.StoreCode), "Product");
                if (!Directory.Exists(createProductsFolder))
                    Directory.CreateDirectory(createProductsFolder);
                string createProductFolder = Path.Combine(createProductsFolder, "P" + ProductCollection.ProductCode);// create folder Product
                if (!Directory.Exists(createProductFolder))
                    Directory.CreateDirectory(createProductFolder);
                List<Media> lst_MediaInsert = new List<Media>();
                IList<ProductInMedia> lst_ProductInMedia = _iProductInMediaService.Get_ProductInMedia_ProductId(ProductCollection.ProductId).ToList();
                IList<Media> lst_MediaPHB = new List<Media>();
                IList<Media> lst_MediaPB = new List<Media>();
                IList<Media> lst_MediaPColour = new List<Media>();
                IList<Media> lst_MediaVideo = new List<Media>();
                IList<Media> lst_MediaEmbed = new List<Media>();
                foreach (var item in lst_ProductInMedia)
                {
                    Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                        lst_MediaPHB.Add(media);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTBANNER_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                        lst_MediaPB.Add(media);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                        lst_MediaPColour.Add(media);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTVIDEO_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                        lst_MediaVideo.Add(media);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTEMBED_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                        lst_MediaEmbed.Add(media);
                }
                if (ProductCollection.ProductTypeCode == "PT1")
                {
                    if (productHBFile != null)
                    {
                        MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE);
                        //===============================================
                        String nameImage = CreateNewName(Path.GetExtension(productHBFile.FileName), mt.MediaTypeCode);
                        string fullPathName = Path.Combine(createProductFolder, nameImage); //path full
                        var rs = ImageHelper.UploadImage(productHBFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                        //===============================================
                        if (rs.Item1 == false)
                        {
                            ModelState.AddModelError("productHBFileDimension", productHBFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                            ValidImage = false;
                        }
                        else
                        {
                            Media mediaCurrent = lst_MediaPHB.FirstOrDefault();
                            if (mediaCurrent == null)
                            {
                                Media media = new Media();
                                media.MediaTypeId = mt.MediaTypeId;
                                media.MediaName = nameImage;
                                media.Url = "/Media/Store/S" + store.StoreCode + "/Product/P" + ProductCollection.ProductCode + "/";
                                lst_MediaInsert.Add(media);
                            }
                            else
                            {
                                mediaCurrent.MediaName = nameImage;
                                _iMediaService.Update(mediaCurrent);
                            }
                        }
                    }
                    if (productColourFile != null && saveType != 2)
                    {
                        MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE);
                        //===============================================
                        String nameImage = CreateNewName(Path.GetExtension(productColourFile.FileName), mt.MediaTypeCode);
                        string fullPathName = Path.Combine(createProductFolder, nameImage); //path full
                        var rs = ImageHelper.UploadImage(productColourFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                        //===============================================
                        if (rs.Item1 == false)
                        {
                            ModelState.AddModelError("productColourFileDimension", productColourFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                            ValidImage = false;
                        }
                        else
                        {
                            Media mediaCurrent = lst_MediaPColour.FirstOrDefault();
                            if (mediaCurrent == null)
                            {
                                Media media = new Media();
                                media.MediaTypeId = mt.MediaTypeId;
                                media.MediaName = nameImage;
                                media.Url = "/Media/Store/S" + store.StoreCode + "/Product/P" + ProductCollection.ProductCode + "/";
                                long mediaId = _iMediaService.Insert(media);
                                if (mediaId != 0)
                                {
                                    ProductInMedia sim = new ProductInMedia();
                                    sim.ProductId = ProductCollection.ProductId;
                                    sim.MediaId = mediaId;
                                    _iProductInMediaService.Insert(sim);
                                }
                            }
                            else
                            {
                                mediaCurrent.MediaName = nameImage;
                                _iMediaService.Update(mediaCurrent);
                            }
                        }
                    }
                    if (productBFiles[0] != null)
                    {
                        MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTBANNER_MEDIATYPECODE);
                        int t = 1;
                        foreach (HttpPostedFileBase file in productBFiles)
                        {
                            //===============================================
                            String nameImage = CreateNewName(Path.GetExtension(file.FileName), mt.MediaTypeCode);
                            string fullPathName = Path.Combine(createProductFolder, nameImage); //path full
                            var rs = ImageHelper.UploadImage(file, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                            //===============================================
                            if (rs.Item1 == false)
                            {
                                ModelState.AddModelError("productBFilesDimension" + t, file.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                                ValidImage = false;
                            }
                            else
                            {
                                List<Media> lst_mediaCurrent = lst_MediaPB.Where(a => a.IsActive == false).Take(productBFiles.Count()).ToList();
                                if (lst_mediaCurrent.Count() == 0)
                                {
                                    Media media = new Media();
                                    media.MediaTypeId = mt.MediaTypeId;
                                    media.MediaName = nameImage;
                                    media.Url = "/Media/Store/S" + store.StoreCode + "/Product/P" + ProductCollection.ProductCode + "/";
                                    lst_MediaInsert.Add(media);
                                }
                                else
                                {
                                    foreach (var item in lst_mediaCurrent)
                                    {
                                        item.MediaName = nameImage;
                                        item.IsActive = true;
                                        _iMediaService.Update(item);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (ProductCollection.ProductTypeCode == "PT2")
                {
                    Media mediaCurrentVideo = (lst_MediaVideo.FirstOrDefault() ?? new Media());
                    Media mediaCurrentEmbed = (lst_MediaEmbed.FirstOrDefault() ?? new Media());
                    if (VideoFile != null && txtBrochureVideo == "")
                    {
                        MediaType mt_Video = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTVIDEO_MEDIATYPECODE);
                        //===============================================
                        String nameImage = CreateNewName(Path.GetExtension(VideoFile.FileName), mt_Video.MediaTypeCode);
                        string fullPathName = Path.Combine(createProductFolder, nameImage); //path full
                        VideoFile.SaveAs(fullPathName);
                        //===============================================
                        if (mediaCurrentVideo.MediaId != 0 && mediaCurrentEmbed.MediaId == 0)
                        {
                            mediaCurrentVideo.MediaName = nameImage;
                            _iMediaService.Update(mediaCurrentVideo);
                        }
                        else if (mediaCurrentEmbed.MediaId != 0 && mediaCurrentVideo.MediaId == 0)
                        {
                            mediaCurrentVideo.MediaId = mediaCurrentEmbed.MediaId;
                            mediaCurrentVideo.MediaTypeId = mt_Video.MediaTypeId;
                            mediaCurrentVideo.MediaName = nameImage;
                            mediaCurrentVideo.Url = "/Media/Store/S" + store.StoreCode + "/Product/P" + ProductCollection.ProductCode + "/";
                            _iMediaService.Update(mediaCurrentVideo);
                        }
                        else
                        {
                            Media media = new Media();
                            media.MediaTypeId = mt_Video.MediaTypeId;
                            media.MediaName = nameImage;
                            media.Url = "/Media/Store/S" + store.StoreCode + "/Product/P" + ProductCollection.ProductCode + "/";
                            lst_MediaInsert.Add(media);
                        }

                    }
                    else if (txtBrochureVideo != "" && VideoFile == null)
                    {
                        MediaType mt_Embed = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTEMBED_MEDIATYPECODE);
                        string iframe = this.CastURLYoutubeToIFrame(txtBrochureVideo);

                        if (mediaCurrentEmbed.MediaId != 0 && mediaCurrentVideo.MediaId == 0)
                        {
                            mediaCurrentEmbed.MediaName = iframe;
                            mediaCurrentEmbed.Url = "<iframe width='" + mt_Embed.RWidth + "%' height='" + mt_Embed.RHeight + "' src='//www.youtube.com/embed/" + iframe + "' frameborder='0' allowfullscreen></iframe>";
                            _iMediaService.Update(mediaCurrentEmbed);
                        }
                        else if (mediaCurrentVideo.MediaId != 0 && mediaCurrentEmbed.MediaId == 0)
                        {
                            mediaCurrentEmbed.MediaId = mediaCurrentVideo.MediaId;
                            mediaCurrentEmbed.MediaTypeId = mt_Embed.MediaTypeId;
                            mediaCurrentEmbed.MediaName = iframe;
                            mediaCurrentEmbed.Url = "<iframe width='" + mt_Embed.RWidth + "%' height='" + mt_Embed.RHeight + "' src='//www.youtube.com/embed/" + iframe + "' frameborder='0' allowfullscreen></iframe>";
                            _iMediaService.Update(mediaCurrentEmbed);
                        }
                        else
                        {
                            Media media = new Media();
                            media.MediaTypeId = mt_Embed.MediaTypeId;
                            media.MediaName = iframe;
                            media.Url = "<iframe width='" + mt_Embed.RWidth + "%' height='" + mt_Embed.RHeight + "' src='//www.youtube.com/embed/" + iframe + "' frameborder='0' allowfullscreen></iframe>";
                            lst_MediaInsert.Add(media);
                        }
                    }
                }
                foreach (var media in lst_MediaInsert)
                {
                    long mediaId = _iMediaService.Insert(media);
                    if (mediaId != 0)
                    {
                        ProductInMedia sim = new ProductInMedia();
                        sim.ProductId = ProductCollection.ProductId;
                        sim.MediaId = mediaId;
                        _iProductInMediaService.Insert(sim);
                    }
                }
                if (saveType == 1)
                {
                    bool updateProductStatus = _iProductService.Update(ProductCollection);
                    if (ValidImage == true)
                    {
                        this.ShowProductWarning(ProductCollection);
                        if (TempData["listCategoryDuplicate"] != null)
                            TempData["tabActive"] = "tabthree";
                        else if (productBFiles[0] != null || productColourFile != null || productHBFile != null)
                            TempData["tabActive"] = "tabtwo";
                        this.LoadProductFormPage(ProductCollection.ProductId);

                        TempData["StatusMessage"] = 1; //1: Warning
                        ViewBag.SuccessAlert = "ShowSuccessAlert";
                        ViewBag.SideBarMenu = "ProductIndex";
                        return View(ProductCollection);
                    }

                    if (TempData["listCategoryDuplicate"] != null)
                        TempData["tabActive"] = "tabthree";
                    else if (productBFiles[0] != null || productColourFile != null || productHBFile != null)
                        TempData["tabActive"] = "tabtwo";
                    this.LoadProductFormPage(ProductCollection.ProductId);
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    TempData["StatusMessage"] = 0; //0: Error
                    ViewBag.SideBarMenu = "ProductIndex";
                    return View(ProductCollection);
                }
                else if (saveType == 2)
                {
                    ProductCollection.CreatedBy = accOnline.AccountId;
                    ProductCollection.Colour = newColour;
                    ProductCollection.ProductStockCode = String.Format("P" + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString("d2") + DateTime.Now.Day.ToString("d2") + "-" + new Random().Next(10000000, 99999999).ToString());
                    long productId = _iProductService.Insert(ProductCollection);
                    Product p = _iProductService.Get_ProductById(productId);
                    if (productId != 0)
                    {
                        string createProductsFolderColour = Path.Combine(Server.MapPath("~/Media/Store/S" + store.StoreCode), "Product");
                        if (!Directory.Exists(createProductsFolderColour))
                            Directory.CreateDirectory(createProductsFolderColour);
                        string createProductFolderColour = Path.Combine(createProductsFolderColour, "P" + p.ProductCode);// create folder Product
                        if (!Directory.Exists(createProductFolderColour))
                            Directory.CreateDirectory(createProductFolderColour);
                        if (productColourFile != null)
                        {
                            MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE);
                            //===============================================
                            String nameImage = CreateNewName(Path.GetExtension(productColourFile.FileName), mt.MediaTypeCode);
                            string fullPathName = Path.Combine(createProductFolderColour, nameImage); //path full
                            var rs = ImageHelper.UploadImage(productColourFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                            //===============================================
                            if (rs.Item1 == false)
                            {
                                ModelState.AddModelError("productColourFileDimension", productColourFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                                ValidImage = false;
                            }
                            else
                            {
                                Media media = new Media();
                                media.MediaTypeId = mt.MediaTypeId;
                                media.MediaName = nameImage;
                                media.Url = "/Media/Store/S" + store.StoreCode + "/Product/P" + ProductCollection.ProductCode + "/";
                                long mediaId = _iMediaService.Insert(media);
                                if (mediaId != 0)
                                {
                                    ProductInMedia sim = new ProductInMedia();
                                    sim.ProductId = productId;
                                    sim.MediaId = mediaId;
                                    _iProductInMediaService.Insert(sim);
                                }
                            }
                        }

                        if (ValidImage == true)
                        {
                            this.ShowProductWarning(ProductCollection);
                            if (TempData["listCategoryDuplicate"] != null)
                                TempData["tabActive"] = "tabthree";
                            else if (productBFiles[0] != null || productColourFile != null || productHBFile != null)
                                TempData["tabActive"] = "tabtwo";
                            this.LoadProductFormPage(ProductCollection.ProductId);

                            TempData["StatusMessage"] = 1; //1: Warning
                            ViewBag.SuccessAlert = "AddColourSuccess";
                            ViewBag.SideBarMenu = "ProductIndex";
                            return View(ProductCollection);
                        }
                    }

                    if (TempData["listCategoryDuplicate"] != null)
                        TempData["tabActive"] = "tabthree";
                    else if (productBFiles[0] != null || productColourFile != null || productHBFile != null)
                        TempData["tabActive"] = "tabtwo";
                    this.LoadProductFormPage(ProductCollection.ProductId);
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    TempData["StatusMessage"] = 0; //0: Error
                    ViewBag.SideBarMenu = "ProductIndex";
                    return View(ProductCollection);

                }
                else if (saveType == 3)
                {
                    bool updateProductStatus = _iProductService.Update(ProductCollection);

                    this.ShowProductWarning(ProductCollection);
                    TempData["StatusMessage"] = 2; //2: Success

                    TempData["productId"] = ProductCollection.ProductCode;

                    ViewBag.SideBarMenu = "ProductIndex";

                    return RedirectToAction("ProductIndex", "MSS");
                }
                else
                {
                    if (TempData["listCategoryDuplicate"] != null)
                        TempData["tabActive"] = "tabthree";
                    else if (productBFiles[0] != null || productColourFile != null || productHBFile != null)
                        TempData["tabActive"] = "tabtwo";
                    this.LoadProductFormPage(ProductCollection.ProductId);
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    TempData["StatusMessage"] = 0; //0: Error
                    ViewBag.SideBarMenu = "ProductIndex";
                    return View(ProductCollection);
                }
            }
            else
            {
                if (TempData["listCategoryDuplicate"] != null)
                    TempData["tabActive"] = "tabthree";
                else if (productBFiles[0] != null || productColourFile != null || productHBFile != null)
                    TempData["tabActive"] = "tabtwo";
                this.LoadProductFormPage(ProductCollection.ProductId);
                TempData["StatusMessage"] = 0; //0: Error
                ViewBag.SideBarMenu = "ProductIndex";
                return View(ProductCollection);
            }
        }
        #endregion

        #region 2. Action Method: ProductPriority
        //Description: ProductPriority
        public ActionResult ProductPriority(long groupPriorityId = 0)
        {
            ProductPriorityViewModel viewModel = new ProductPriorityViewModel();
            GroupPriorityRepository _iGroupPriorityService = new GroupPriorityRepository();
            viewModel.list_GroupPriority = _iGroupPriorityService.GetList_GroupPriorityAll(false);
            viewModel.groupPriorityId = groupPriorityId;
            return View(viewModel);
        }

        public PartialViewResult GetProductPriorityData(long groupPriorityId = 0)
        {
            ProductPriorityPatrialViewModel viewModel = new ProductPriorityPatrialViewModel();
            ProductRepository _iProductService = new ProductRepository();
            ProductInPriorityRepository _iProductInPriorityService = new ProductInPriorityRepository();
            GroupPriorityRepository _iGroupPriorityService = new GroupPriorityRepository();
            IList<ProductInPriority> lst_ProductInPriority = _iProductInPriorityService.GetList_ProductInPriorityAll(false).ToList();
            IList<Product> lst_Product = new List<Product>();
            if (groupPriorityId != 0)
            {
                lst_ProductInPriority = lst_ProductInPriority.Where(p => p.GroupPriorityId == groupPriorityId).ToList();
            }
            foreach (var item in lst_ProductInPriority)
            {
                Product p = _iProductService.Get_ProductById((long)item.ProductId);

                lst_Product.Add(p);
            }
            viewModel.list_ProductInPriority = lst_ProductInPriority;
            viewModel.list_GroupPriority = _iGroupPriorityService.GetList_GroupPriorityAll();
            viewModel.list_Product = lst_Product;
            return PartialView(viewModel);
        }
        public ActionResult ProductPriorityCreate()
        {
            CreateProductPriorityViewModel viewModel = new CreateProductPriorityViewModel();
            ProductRepository _iProductService = new ProductRepository();
            GroupPriorityRepository _iGroupPriorityService = new GroupPriorityRepository();
            ProductInPriorityRepository _iProductInPriorityService = new ProductInPriorityRepository();
            viewModel.list_GroupPriority = _iGroupPriorityService.GetList_GroupPriorityAll();
            viewModel.list_Product = _iProductService.GetList_ProductAll_IsDeleted(false).ToList();
            //viewModel.orderNumberDefault = _iProductInPriorityService.GetList_ProductInPriorityAll(false).Count() + 1;
            return View(viewModel);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ProductPriorityCreate(CreateProductPriorityViewModel viewModel)
        {
            Account accOnline = (Account)Session["Account"];
            ProductRepository _iProductService = new ProductRepository();
            ProductInPriorityRepository _iProductPriorityService = new ProductInPriorityRepository();
            GroupPriorityRepository _iGroupPriorityService = new GroupPriorityRepository();
            ProductInPriorityRepository _iProductInPriorityService = new ProductInPriorityRepository();
          //if (_iGroupPriorityService.GetList_GroupPriorityAll().Count()) ;
          //foreach (var genIdOrder in _iGroupPriorityService.GetList_GroupPriorityAll())
          //{
          //  if (genIdOrder == viewModel.list_GroupPriority)
          //  {
              //viewModel.orderNumberDefault = _iProductPriorityService.GetList_ProductInPriorityAll(false).Count() + 1;
          //  }
          //} 
            if (ValidateProductInPriority(viewModel.ProductInPriorityModel) == true)
            {
                //model.CreatedBy = accOnline.AccountId;
                long productPriorityId = _iProductPriorityService.Insert(viewModel.ProductInPriorityModel);
                if (productPriorityId != -1) //Insert success
                {
                    TempData["StatusMessage"] = 2; //1: Success
                    ViewBag.SideBarMenu = "ProductIndex";
                    return RedirectToAction("ProductPriority", "MSS");
                }
            }
             this.LoadProductInPriorityPage(0);
            ViewBag.SideBarMenu = "ProductIndex";

            viewModel.list_Product = _iProductService.GetList_ProductAll_IsDeleted(false).ToList();
            viewModel.list_GroupPriority = _iGroupPriorityService.GetList_GroupPriorityAll();
            viewModel.orderNumberDefault = _iProductInPriorityService.GetList_ProductInPriorityAll(false).Count() + 1;
            //TempData["StatusMessage"] = 0; //0: Error
            return View(viewModel);
        }

        #endregion

        #region 2. Action Method: AddProductColour
        public ActionResult AddProductColour(long productId)
        {
            return View();
        }
        #endregion

        #region 1. Json Method: ChangeProductVerified
        //Description: display Index
        public JsonResult ChangeProductVerified(long storeId, List<long> listProductId, int sbool, int? page, int? pageSizeSelected)
        {
            ProductRepository _iProductService = new ProductRepository();
            ProductTypeRepository _iProductTypeService = new ProductTypeRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);

            bool updateStatus = false;
            foreach (var productId in listProductId)
            {
                var productUpdateVerified = _iProductService.Get_ProductById(productId);
                if (sbool == -1)
                {
                    updateStatus = _iProductService.UpdateVerified(productId, (productUpdateVerified.IsVerified == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iProductService.UpdateVerified(productId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iProductService.UpdateVerified(productId, true);
                }
            }

            if (updateStatus == true)
            {
                IPagedList<Product> lst_ProductPaged = new PagedList<Product>(new List<Product>(), pageNum, pageSize);
                if (storeId == 0)
                {
                    lst_ProductPaged = _iProductService.GetList_ProductAll(pageNum, pageSize);
                }
                else
                {
                    lst_ProductPaged = _iProductService.GetList_Product_StoreId(storeId, pageNum, pageSize);
                }

                return Json(new { _listProduct = lst_ProductPaged, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 1. Json Method: ChangeProductActive
        //Description: display Index
        public JsonResult ChangeProductActive(long storeId, List<long> listProductId, int sbool, int? page, int? pageSizeSelected)
        {
            ProductRepository _iProductService = new ProductRepository();
            ProductTypeRepository _iProductTypeService = new ProductTypeRepository();

            int pageNum = (page ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            Tuple<bool, string> updateStatus = null;
            foreach (var productId in listProductId)
            {
                var productUpdateVerified = _iProductService.Get_ProductById(productId);
                if (sbool == -1)
                {
                    updateStatus = _iProductService.UpdateActive(productId, (productUpdateVerified.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iProductService.UpdateActive(productId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iProductService.UpdateActive(productId, true);
                }
            }
            if (updateStatus.Item1 == true)
            {
                IPagedList<Product> lst_ProductPaged = new PagedList<Product>(new List<Product>(), pageNum, pageSize);
                if (storeId == 0)
                {
                    lst_ProductPaged = _iProductService.GetList_ProductAll(pageNum, pageSize);
                }
                else
                {
                    lst_ProductPaged = _iProductService.GetList_Product_StoreId(storeId, pageNum, pageSize);
                }

                return Json(new { _listProduct = lst_ProductPaged, Success = true, Message = "OK!", code = updateStatus.Item2 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #endregion

        #region Article Type

        #region 1. Action Method: ArticleTypeIndex
        //Description: display Index
        public ActionResult ArticleTypeIndex(int? page)
        {
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();

            IList<ArticleType> lst_ArticleType = _iArticleTypeService.GetList_ArticleTypeIndex().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;
            LoadArticleTypeFormPage(0);
            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////

            ViewBag.SideBarMenu = "ArticleIndex";
            return View(lst_ArticleType.ToPagedList(pageNum, pageSizeDefault));
        }


        #endregion

        #region 2. Action Method: ArticleTypeCreate
        //Description: display Index
        public ActionResult ArticleTypeCreate()
        {
            //**** Load list ArticleType
            LoadArticleTypeFormPage(0);
            //*************************************************//
            ArticleType ar = new ArticleType();
            ar.IsActive = true;
            ar.IsDeleted = false;
            ViewBag.SideBarMenu = "ArticleIndex";
            return View(ar);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ArticleTypeCreate(ArticleType ArticleTypeCollection)
        {
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            ViewBag.SideBarMenu = "ArticleIndex";
            Account accOnline = (Account)Session["Account"];

            if (this.ValidateArticleTypeForm(ArticleTypeCollection) == true)
            {
                ArticleTypeCollection.IsDeleted = false;
                ArticleTypeCollection.DateCreated = DateTime.Now;
                ArticleTypeCollection.CreatedBy = accOnline.AccountId;

                long articleId = _iArticleTypeService.Insert(ArticleTypeCollection);

                if (articleId != -1) //Insert success
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    return RedirectToAction("ArticleTypeIndex", "MSS");
                }
            }

            //**** Load list ArticleType
            LoadArticleTypeFormPage(ArticleTypeCollection.ArticleTypeId);
            //*************************************************//
            TempData["ResponseMessage"] = 0; //0: Error

            return View(ArticleTypeCollection);
        }

        #endregion

        #region 3. Action Method: ArticleTypeEdit
        //Description: display Index
        public ActionResult ArticleTypeEdit(long? id, string lang)
        {

            if (lang == "" || lang == null || id == null)
            {
                return RedirectToAction("ArticleTypeIndex");
            }

            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            ArticleType articleType = new ArticleType();
            articleType = _iArticleTypeService.Get_ArticleTypeById((long)id);
            if (articleType == null)
            {
                return RedirectToAction("ArticleIndex");
            }
            //**** Load list ArticleType
            LoadArticleTypeFormPage((long)id);
            //*************************************************//
            if (!(lang == "zh" || lang == "en" || lang == "vi"))
            {
                return RedirectToAction("ArticleTypeIndex");
            }
            ViewBag.LanguageCode = (lang == "vi") ? "vi" : (lang == "zh") ? "zh" : "en";//trường này null thi mặc định là tiếng en :D

            ArticleType ar_tmp = _iArticleTypeService.Get_ArticleTypeByCodeandLanguageCode((long)id, lang);
            if (ar_tmp != null)
            {
                articleType = ar_tmp;
            }
            else
            {
                articleType.ArticleTypeName = "";
                articleType.LanguageCode = lang;
            }
            ViewBag.SideBarMenu = "ArticleTypeIndex";
            return View(articleType);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ArticleTypeEdit(ArticleType ArticleTypeCollection, long Code)
        {
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            ViewBag.SideBarMenu = "ArticleTypeIndex";
            if (this.ValidateArticleTypeForm(ArticleTypeCollection) == true)
            {
                ArticleTypeCollection.Code = Code;
                bool kq = _iArticleTypeService.Update(ArticleTypeCollection);

                if (kq == true) //Update success
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    return RedirectToAction("ArticleTypeIndex", "MSS", new { id = ArticleTypeCollection.ArticleTypeId });
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    //**** Load list ArticleType
                    LoadArticleFormPage(ArticleTypeCollection.ArticleTypeId);
                    //*************************************************//
                    TempData["ResponseMessage"] = 0; //0: Error

                    return View(ArticleTypeCollection);
                }
            }
            else
            {
                //**** Load list ArticleType
                LoadArticleFormPage(-1);
                //*************************************************//
                TempData["ResponseMessage"] = 0; //0: Error

                return View(ArticleTypeCollection);
            }


        }

        #endregion

        #endregion

        #region Article

        #region 1. Action Method: ArticleIndex
        //Description: display Index
        public ActionResult ArticleIndex(int? page)
        {
            ArticleRepository _iArticleService = new ArticleRepository();

            IList<Article> lst_Article = _iArticleService.GetList_ArticleIndex().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;
            LoadArticleFormPage(0);
            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////

            ViewBag.SideBarMenu = "ArticleIndex";
            return View(lst_Article.ToPagedList(pageNum, pageSizeDefault));
        }
        #endregion

        #region 2. Action Method: ArticleCreate
        //Description: display Index
        public ActionResult ArticleCreate()
        {
            //**** Load list ArticleType
            LoadArticleFormPage(0);
            //*************************************************//

            ViewBag.SideBarMenu = "ArticleIndex";
            return View(new Article());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ArticleCreate(Article ArticleCollection)
        {
            ArticleRepository _iArticleService = new ArticleRepository();
            ViewBag.SideBarMenu = "ArticleIndex";
            Account accOnline = (Account)Session["Account"];

            if (this.ValidateArticleForm(ArticleCollection) == true)
            {
                ArticleCollection.IsPublish = false;
                ArticleCollection.IsVerified = false;
                ArticleCollection.IsDeleted = false;
                ArticleCollection.DateCreated = DateTime.Now;
                ArticleCollection.CreatedBy = accOnline.AccountId;

                long articleId = _iArticleService.Insert(ArticleCollection);

                if (articleId != -1) //Insert success
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    return RedirectToAction("ArticleIndex", "MSS");
                }
            }

            //**** Load list ArticleType
            LoadArticleFormPage(ArticleCollection.ArticleId);
            //*************************************************//
            TempData["ResponseMessage"] = 0; //0: Error

            return View(ArticleCollection);
        }

        #endregion

        #region 3. Action Method: ArticleEdit
        //Description: display Index
        public ActionResult ArticleEdit(long? id, string lang)
        {
            if (lang == "" || lang == null || id == null)
            {
                return RedirectToAction("ArticleIndex");
            }

            ArticleRepository _iArticleService = new ArticleRepository();
            Article article = new Article();
            article = _iArticleService.Get_ArticleById((long)id);
            if (article == null)
            {
                return RedirectToAction("ArticleIndex");
            }
            //**** Load list ArticleType
            LoadArticleFormPage((long)id);
            //*************************************************//
            if (!(lang == "zh" || lang == "en" || lang == "vi"))
            {
                return RedirectToAction("ArticleIndex");
            }
            ViewBag.LanguageCode = (lang == "vi") ? "vi" : (lang == "zh") ? "zh" : "en";//trường này null thi mặc định là tiếng en :D

            Article ar_tmp = _iArticleService.Get_ArticleByCodeandLanguageCode((long)id, lang);
            if (ar_tmp != null)
            {
                article = ar_tmp;
            }
            else
            {
                article.Title = "";
                article.Introduction = "";
                article.ArticleContent = "";
                article.MetaDescription = "";
                article.MetaKeywords = "";
                article.MetaTitle = "";
                article.LanguageCode = lang;
            }

            ViewBag.SideBarMenu = "ArticleIndex";
            return View(article);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ArticleEdit(Article ArticleCollection, int? saveType, long Code)
        {
            ArticleRepository _iArticleService = new ArticleRepository();
            ViewBag.SideBarMenu = "ArticleIndex";
            Account accOnline = (Account)Session["Account"];
            if (this.ValidateArticleForm(ArticleCollection) == true)
            {
                ArticleCollection.ModifiedBy = accOnline.AccountId; //Update Modified By
                ArticleCollection.Code = Code;
                bool kq = _iArticleService.Update(ArticleCollection);

                if (kq == true) //Update success
                {
                    if (saveType != 1)
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("ArticleIndex", "MSS");
                    }
                    else
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("ArticleEdit", "MSS", new { id = ArticleCollection.ArticleId });
                    }
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    //**** Load list ArticleType
                    LoadArticleFormPage(ArticleCollection.ArticleId);
                    //*************************************************//
                    TempData["ResponseMessage"] = 0; //0: Error

                    return View(ArticleCollection);
                }
            }
            else
            {
                //**** Load list ArticleType
                LoadArticleFormPage(-1);
                //*************************************************//
                TempData["ResponseMessage"] = 0; //0: Error

                return View(ArticleCollection);
            }


        }

        #endregion

        #region 1. Json Method: ChangeArticleVerified
        //Description: display Index
        public JsonResult ChangeArticleVerified(List<long> listArticleId, int sbool)
        {
            ArticleRepository _iArticleService = new ArticleRepository();
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            bool updateStatus = false;
            foreach (var articleId in listArticleId)
            {
                var storeUpdateVerified = _iArticleService.Get_ArticleById(articleId);
                if (sbool == -1)
                {
                    updateStatus = _iArticleService.UpdateVerified(articleId, (storeUpdateVerified.IsVerified == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iArticleService.UpdateVerified(articleId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iArticleService.UpdateVerified(articleId, true);
                }
            }

            if (updateStatus == true)
            {
                IList<Article> lst_Article = _iArticleService.GetList_ArticleAll();

                var lst_ArticleType = _iArticleTypeService.GetList_ArticleTypeAll(true, false);

                return Json(new { _listArticle = lst_Article.ToPagedList(1, pageSizeDefault), _listArticleType = lst_ArticleType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region 1. Json Method: ChangeArticlePublished
        //Description: display Index
        public JsonResult ChangeArticlePublished(List<long> listArticleId, int sbool)
        {
            ArticleRepository _iArticleService = new ArticleRepository();
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            bool updateStatus = false;
            foreach (var articleId in listArticleId)
            {
                var storeUpdatePublished = _iArticleService.Get_ArticleById(articleId);
                if (sbool == -1)
                {
                    updateStatus = _iArticleService.UpdatePublished(articleId, (storeUpdatePublished.IsPublish == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iArticleService.UpdatePublished(articleId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iArticleService.UpdatePublished(articleId, true);
                }
            }

            if (updateStatus == true)
            {
                IList<Article> lst_Article = _iArticleService.GetList_ArticleAll();

                var lst_ArticleType = _iArticleTypeService.GetList_ArticleTypeAll(true, false);

                return Json(new { _listArticle = lst_Article.ToPagedList(1, pageSizeDefault), _listArticleType = lst_ArticleType, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #endregion

        #region Brand

        #region 1. Action Method: BrandIndex
        //Description: display Index
        public ActionResult BrandIndex(int? page)
        {
            BrandRepository _iBrandService = new BrandRepository();
            IList<Brand> lst_Brand = _iBrandService.GetList_BrandAll().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;

            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////


            ViewBag.SideBarMenu = "BrandIndex";
            return View(lst_Brand.ToPagedList(pageNum, pageSizeDefault));
        }
        #endregion

        #region 2. Action Method: BrandCreate
        //Description: display Index
        public ActionResult BrandCreate()
        {
            ViewBag.SideBarMenu = "BrandIndex";
            return View(new Brand());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BrandCreate(Brand BrandCollection)
        {
            BrandRepository _iBrandService = new BrandRepository();
            Account accOnline = (Account)Session["Account"];

            if (this.ValidateBrandForm(BrandCollection, null, null) == true)
            {

                BrandCollection.CreatedBy = accOnline.AccountId;

                long brandId = _iBrandService.Insert(BrandCollection);

                if (brandId != -1) //Insert success
                {
                    ViewBag.ResponseMessage = true;
                    return RedirectToAction("BrandIndex", "MSS");
                }
            }
            ViewBag.ResponseMessage = false;
            ViewBag.SideBarMenu = "BrandIndex";
            return View(BrandCollection);
        }

        #endregion

        #region 3. Action Method: BrandEdit
        //Description: display Index
        public ActionResult BrandEdit(long id)
        {
            BrandRepository _iBrandService = new BrandRepository();

            Brand brand = _iBrandService.Get_BrandById(id);
            LoadBrandFormPage(id);
            ViewBag.SideBarMenu = "BrandIndex";
            return View(brand);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BrandEdit(Brand BrandCollection, HttpPostedFileBase brandLogoFile, HttpPostedFileBase brandBannerFile, int? saveType)
        {
            BrandRepository _iBrandService = new BrandRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            Account accOnline = (Account)Session["Account"];

            Brand Brand = new Brand();
            Brand old_Brand = _iBrandService.Get_BrandById(BrandCollection.BrandId);//lấy brand cũ lên để check update


            if (this.ValidateBrandForm(BrandCollection, brandLogoFile, brandBannerFile) == true)
            {
                List<Media> lst_MediaInsert = new List<Media>();
                bool ValidImage = true;
                //xử lý thêm ảnh
                string createBrandsFolder = Path.Combine(Server.MapPath("~/Media"), "Brand");
                if (!System.IO.Directory.Exists(createBrandsFolder))
                {
                    Directory.CreateDirectory(createBrandsFolder);
                }
                string createBrandFolder = Path.Combine(createBrandsFolder, "B" + BrandCollection.BrandId);// create folder Brand

                if (!System.IO.Directory.Exists(createBrandFolder))
                {
                    Directory.CreateDirectory(createBrandFolder);
                }

                if (brandLogoFile != null)
                {
                    MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(BRANDLOGO_MEDIATYPECODE);
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(brandLogoFile.FileName), mt.MediaTypeCode);
                    string fullPathName = Path.Combine(createBrandFolder, nameImage); //path full
                    var rs = ImageHelper.UploadImage(brandLogoFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                    //===============================================
                    if (rs.Item1 == false)
                    {
                        ModelState.AddModelError("brandLogoFileDimension", brandLogoFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                        ValidImage = false;
                        ViewBag.SideBarMenu = "BrandIndex";

                        LoadBrandFormPage(BrandCollection.BrandId);
                        return View(BrandCollection);
                    }
                    else
                    {
                        Media media = new Media();

                        media.MediaTypeId = mt.MediaTypeId;
                        media.MediaName = nameImage;
                        media.Url = "/Media/Brand/B" + BrandCollection.BrandId + "/";

                        if (old_Brand.LogoMediaId == null)
                        {
                            long new_mediaId = _iMediaService.Insert(media);
                            if (new_mediaId != -1)
                                BrandCollection.LogoMediaId = new_mediaId;
                        }
                        else
                        {
                            media.MediaId = (long)old_Brand.LogoMediaId;
                            _iMediaService.Update(media);
                        }
                    }
                }

                if (brandBannerFile != null)
                {
                    MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(BRANDBANNER_MEDIATYPECODE);
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(brandBannerFile.FileName), mt.MediaTypeCode);
                    string fullPathName = Path.Combine(createBrandFolder, nameImage); //path full
                    var rs = ImageHelper.UploadImage(brandBannerFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                    //===============================================
                    if (rs.Item1 == false)
                    {
                        ModelState.AddModelError("brandBannerFileDimension", brandBannerFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                        ValidImage = false;
                        ViewBag.SideBarMenu = "BrandIndex";

                        LoadBrandFormPage(BrandCollection.BrandId);
                        return View(BrandCollection);
                    }
                    else
                    {
                        Media media = new Media();

                        media.MediaTypeId = mt.MediaTypeId;
                        media.MediaName = nameImage;
                        media.Url = "/Media/Brand/B" + BrandCollection.BrandId + "/";

                        if (old_Brand.BannerMediaId == null)
                        {
                            long new_mediaId = _iMediaService.Insert(media);
                            if (new_mediaId != -1)
                                BrandCollection.BannerMediaId = new_mediaId;
                        }
                        else
                        {
                            media.MediaId = (long)old_Brand.BannerMediaId;
                            _iMediaService.Update(media);
                        }
                    }
                }

                bool kq = _iBrandService.Update(BrandCollection);

                if (kq == true) //Insert success
                {
                    if (saveType != 1)
                    {
                        TempData["ResponseMessage"] = 1; //1: Success

                        return RedirectToAction("BrandIndex", "MSS");
                    }
                    else
                    {
                        TempData["ResponseMessage"] = 1; //1: Success

                        return RedirectToAction("BrandEdit", "MSS", new { id = BrandCollection.BrandId });
                    }
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    ViewBag.SideBarMenu = "BrandIndex";

                    LoadBrandFormPage(BrandCollection.BrandId);
                    return View(BrandCollection);
                }
            }
            else
            {
                ViewBag.SideBarMenu = "BrandIndex";

                LoadBrandFormPage(BrandCollection.BrandId);
                return View(BrandCollection);
            }
        }

        #endregion

        #region 1. Json Method: ChangeBrandActive
        //Description: display Index
        public JsonResult ChangeBrandActive(List<long> listBrandId, int sbool)
        {
            BrandRepository _iBrandService = new BrandRepository();
            bool updateStatus = false;
            foreach (var brandId in listBrandId)
            {
                var brandUpdateIsActive = _iBrandService.Get_BrandById(brandId);
                if (sbool == -1)
                {
                    updateStatus = _iBrandService.UpdateActive(brandId, (brandUpdateIsActive.IsActive == true ? false : true));
                }
                if (sbool == 0)
                {
                    updateStatus = _iBrandService.UpdateActive(brandId, false);
                }
                if (sbool == 1)
                {
                    updateStatus = _iBrandService.UpdateActive(brandId, true);
                }
            }
            if (updateStatus == true)
            {
                IList<Brand> lst_Brand = _iBrandService.GetList_BrandAll();


                return Json(new { _listBrand = lst_Brand.ToPagedList(1, pageSizeDefault), Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #endregion

        #region Category

        #region 2. Action Method: CategoryIndex
        public ActionResult CategoryIndex()
        {
            CategoryRepository _iCategoryService = new CategoryRepository();

            _listCategorySortForParentID = new List<Category>();
            List<Category> t = _iCategoryService.GetList_CategoryAll().ToList();
            GetAllCategory_SortParent(t.Where(i => i.ParentCateId == 0).ToList());
            IList<Category> lst_Gift = _listCategorySortForParentID;
            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////

            Category_MultiLangRepository _iCategory_MutilangService = new Category_MultiLangRepository();
            ViewBag.LanguageCategoryName = _iCategory_MutilangService.GetList_CategoryCategoryMultiLangAll();

            ViewBag.SideBarMenu = "CategoryIndex";
            return View(lst_Gift.ToList());
        }
        #endregion

        #region 3. Action Method: CategoryCreate
        public ActionResult CategoryCreate()
        {
            ViewBag.SideBarMenu = "CategoryIndex";
            LoadCategoryFromPage(0);
            Category model = new Category();
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CategoryCreate(string Language, Category CategoryCollection)
        {
            CategoryRepository _iCategoryService = new CategoryRepository();
            Account accOnline = (Account)Session["Account"];
            //lưu tên user tạo
            CategoryCollection.CreatedBy = accOnline.AccountId;

            LoadCategoryFromPage(0);

            if (CategoryCollection.ParentCateId == null)
            {
                CategoryCollection.ParentCateId = 0;
                CategoryCollection.CateLevel = 0;
            }
            else
            {
                CategoryCollection.CateLevel = _iCategoryService.Get_CategoryById((long)CategoryCollection.ParentCateId).CateLevel + 1;
            }
            CategoryCollection.OrderNumber = _iCategoryService.GetList_CategoryAll().Where(i => i.ParentCateId == CategoryCollection.ParentCateId).ToList().Count + 1;
            CategoryCollection.DateCreated = DateTime.Now;
            CategoryCollection.VisitCount = 0;

            if (this.ValidateCategoryForm(Language, CategoryCollection, null, null) == true)
            {
                long CategoryId = 0;

                if (Language == "zh")
                {
                    CategoryId = _iCategoryService.Insert(CategoryCollection);
                }
                else
                {
                    string CategoryName = CategoryCollection.CategoryName;
                    string Description = CategoryCollection.Description;
                    CategoryCollection.CategoryName = "";
                    CategoryCollection.Description = "";
                    CategoryId = _iCategoryService.Insert(CategoryCollection);
                    Category_MultiLang camu = new Category_MultiLang();
                    Category_MultiLangRepository _icateMulang = new Category_MultiLangRepository();
                    camu.CategoryName = CategoryName;
                    camu.Description = CategoryName;
                    camu.IsActive = CategoryCollection.IsActive;
                    camu.IsDeleted = CategoryCollection.IsDeleted;
                    camu.CreatedBy = accOnline.AccountId;
                    camu.LanguageCode = Language;
                    camu.CategoryId = CategoryId;
                    _icateMulang.Insert(camu);
                }

                if (CategoryId != -1) //Insert success
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    return RedirectToAction("CategoryIndex", "MSS");
                }
            }
            ViewBag.ResponseMessage = 0;
            ViewBag.SideBarMenu = "CategoryIndex";
            return View(CategoryCollection);
        }
        #endregion

        #region 4. Action Method: CategoryEdit
        public ActionResult CategoryEdit(int? id, string lang)
        {
            if (id == null)
            {
                return RedirectToAction("CategoryIndex");
            }
            CategoryRepository _iCategoryService = new CategoryRepository();
            Category_MultiLangRepository _iCategory_MultiLangService = new Category_MultiLangRepository();
            Category category = _iCategoryService.Get_CategoryById((long)id);

            ViewBag.LanguageCode = (lang == "vi") ? "vi" : (lang == "en") ? "en" : "zh";//trường này null thi mặc định là tiếng cn :D
            if (ViewBag.LanguageCode != "zh")
            {
                Category_MultiLang catemu = _iCategory_MultiLangService.Get_CategoryByIdandLanguageCode(category.CategoryId, ViewBag.LanguageCode);
                if (catemu != null)
                {
                    category.Description = catemu.Description;
                    category.MetaKeywords = catemu.MetaKeywords;
                    category.MetaDescription = catemu.MetaDescription;
                    category.MetaTitle = catemu.MetaTitle;
                    category.CategoryName = catemu.CategoryName;
                    category.Alias = catemu.Alias;
                }
                else
                {
                    category.Description = "";
                    category.MetaKeywords = "";
                    category.MetaTitle = "";
                    category.CategoryName = "";
                    category.Alias = "";
                    category.MetaDescription = "";
                }

            }
            ViewBag.SideBarMenu = "CategoryIndex";
            LoadCategoryFromPage((long)id);
            return View(category);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CategoryEdit(Category CategoryCollection, HttpPostedFileBase categoryLogoFile, HttpPostedFileBase categoryBannerFile, int? saveType, string LanguageCode)
        {
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();

            CategoryRepository _iCategoryService = new CategoryRepository();
            Account accOnline = (Account)Session["Account"];
            Category old_Category = _iCategoryService.Get_CategoryById(CategoryCollection.CategoryId);
            List<Category> lst_CategoryAll = _iCategoryService.GetList_CategoryAll().ToList();
            if (CategoryCollection.ParentCateId == null)
            {
                CategoryCollection.ParentCateId = 0;
                CategoryCollection.CateLevel = 0;
            }
            else
            {
                CategoryCollection.CateLevel = _iCategoryService.Get_CategoryById((long)CategoryCollection.ParentCateId).CateLevel + 1;
            }

            int checkExits = lst_CategoryAll.Where(c => c.ParentCateId == CategoryCollection.ParentCateId).ToList().Where(k => k.CategoryId == CategoryCollection.CategoryId).ToList().Count;
            if (checkExits == 1)//có tồn tại :)
            {
                if (lst_CategoryAll.Where(c => c.ParentCateId == CategoryCollection.ParentCateId).ToList().Where(i => i.OrderNumber == CategoryCollection.OrderNumber && i.CategoryId != CategoryCollection.CategoryId).ToList().Count > 0)
                {
                    Category cate = lst_CategoryAll.Where(c => c.ParentCateId == CategoryCollection.ParentCateId).ToList().Where(i => i.OrderNumber == CategoryCollection.OrderNumber && i.CategoryId != CategoryCollection.CategoryId).FirstOrDefault();
                    foreach (var item in lst_CategoryAll.Where(c => c.ParentCateId == CategoryCollection.ParentCateId).ToList())
                    {
                        if (item.OrderNumber >= cate.OrderNumber && item.OrderNumber < old_Category.OrderNumber)
                        {
                            _iCategoryService.UpdateCategoryOrderNumber(item.CategoryId, (int)item.OrderNumber + 1);
                        }
                        if (item.OrderNumber > old_Category.OrderNumber && item.OrderNumber <= CategoryCollection.OrderNumber)
                        {

                            _iCategoryService.UpdateCategoryOrderNumber(item.CategoryId, (int)item.OrderNumber - 1);
                        }
                    }
                }
            }
            else // không tồn tại
            {
                //thêm vào parent mới
                List<Category> list_CountOrderNumber = lst_CategoryAll.Where(i => i.ParentCateId == CategoryCollection.ParentCateId).ToList();
                int countOrdernumber = list_CountOrderNumber.Count;
                CategoryCollection.OrderNumber = countOrdernumber + 1;
            }

            //Parent cũ
            long oldParentId = (long)old_Category.ParentCateId;
            if (this.ValidateCategoryForm("[Edit]", CategoryCollection, categoryLogoFile, categoryBannerFile) == true)
            {
                //xử lý thêm ảnh
                List<Media> lst_MediaInsert = new List<Media>();
                MediaRepository _iMediaService = new MediaRepository();

                string createCategorysFolder = Path.Combine(Server.MapPath("~/Media"), "Category");
                if (!System.IO.Directory.Exists(createCategorysFolder))
                {
                    Directory.CreateDirectory(createCategorysFolder);
                }
                string createCategoryFolder = Path.Combine(createCategorysFolder, "C" + CategoryCollection.CategoryId);// create folder Product

                if (!System.IO.Directory.Exists(createCategoryFolder))
                {
                    Directory.CreateDirectory(createCategoryFolder);
                }

                if (categoryLogoFile != null)
                {
                    MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(CATEGORYLOGO_MEDIATYPECODE);
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(categoryLogoFile.FileName), mt.MediaTypeCode);
                    string fullPathName = Path.Combine(createCategoryFolder, nameImage); //path full
                    var rs = ImageHelper.UploadImage(categoryLogoFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);

                    if (rs.Item1 == false)
                    {
                        ModelState.AddModelError("categoryLogoFileDimension", categoryLogoFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                    }
                    else
                    {
                        //===============================================
                        Media media = new Media();

                        media.MediaTypeId = mt.MediaTypeId;
                        media.MediaName = nameImage;
                        media.Url = "/Media/Category/C" + CategoryCollection.CategoryId + "/";

                        if (old_Category.LogoMediaId == null)
                        {
                            long new_mediaId = _iMediaService.Insert(media);
                            if (new_mediaId != -1)
                                CategoryCollection.LogoMediaId = new_mediaId;
                        }
                        else
                        {
                            media.MediaId = (long)old_Category.LogoMediaId;
                            _iMediaService.Update(media);
                        }
                    }
                }

                if (categoryBannerFile != null)
                {
                    MediaType mt = _iMediaTypeService.Get_MediaTypeByCode(CATEGORYBANNER_MEDIATYPECODE);
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(categoryBannerFile.FileName), mt.MediaTypeCode);
                    string fullPathName = Path.Combine(createCategoryFolder, nameImage); //path full
                    var rs = ImageHelper.UploadImage(categoryBannerFile, fullPathName, (int)mt.RWidth, (int)mt.RHeight);
                    //===============================================

                    if (rs.Item1 == false)
                    {
                        ModelState.AddModelError("categoryBannerFileDimension", categoryBannerFile.FileName + " dimension is invaild, dimension allow " + mt.RWidth.ToString() + "x" + mt.RHeight.ToString());
                    }
                    else
                    {
                        Media media = new Media();
                        media.MediaTypeId = mt.MediaTypeId;
                        media.MediaName = nameImage;
                        media.Url = "/Media/Category/C" + CategoryCollection.CategoryId + "/";

                        if (old_Category.BannerMediaId == null)
                        {
                            long new_mediaId = _iMediaService.Insert(media);
                            if (new_mediaId != -1)
                                CategoryCollection.BannerMediaId = new_mediaId;
                        }
                        else
                        {
                            media.MediaId = (long)old_Category.BannerMediaId;
                            _iMediaService.Update(media);
                        }
                    }
                }

                //update....
                bool kq = false;
                Category_MultiLangRepository _iCategory_MultiLangService = new Category_MultiLangRepository();
                if (LanguageCode == "zh")
                {
                    kq = _iCategoryService.Update(CategoryCollection);
                }
                else
                {
                    kq = _iCategory_MultiLangService.Update(CategoryCollection, LanguageCode);

                    Category temp_cate = new Category();
                    temp_cate.IsActive = CategoryCollection.IsActive;
                    temp_cate.IsDeleted = CategoryCollection.IsDeleted;
                    temp_cate.CategoryId = CategoryCollection.CategoryId;
                    temp_cate.OrderNumber = CategoryCollection.OrderNumber;
                    temp_cate.ParentCateId = CategoryCollection.ParentCateId;
                    temp_cate.BannerMediaId = CategoryCollection.BannerMediaId;
                    temp_cate.LogoMediaId = CategoryCollection.LogoMediaId;
                    _iCategoryService.Update(temp_cate);
                }

                if (kq == true) //Insert success
                {
                    //sau khi update thành công
                    if (checkExits == 0)// fix lại ordernumber cũ :]]
                    {
                        _iCategoryService.FixOrderNumberCategory(oldParentId);
                    }
                }
                if (saveType != 1)
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    return RedirectToAction("CategoryIndex", "MSS");
                }
                else
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    ViewBag.SideBarMenu = "CategoryIndex";
                    LoadCategoryFromPage(CategoryCollection.CategoryId);
                    if (categoryLogoFile != null || categoryBannerFile != null)
                    {
                        TempData["tabActive"] = "tabtwo";
                    }
                    return View(CategoryCollection);
                }
            }
            else
            {
                ViewBag.SideBarMenu = "CategoryIndex";
                ViewBag.LanguageCode = LanguageCode;
                LoadCategoryFromPage(CategoryCollection.CategoryId);
                if (categoryLogoFile != null || categoryBannerFile != null)
                {
                    TempData["tabActive"] = "tabtwo";
                }

                return View(CategoryCollection);
            }
        }

        #endregion

        #region 1. Json Method: GetListCategoryLv
        //Description: display Index
        public JsonResult GetListCategoryLv(int categoryId, int parentCateId, string lang)
        {
            try
            {
                CategoryRepository _iCategoryService = new CategoryRepository();
                var lstCategory = _iCategoryService.GetByLang(categoryId, parentCateId, lang);



                var ddl_CategoryLv = new SelectList(
                    _iCategoryService.GetList_Category_CateLevel_ParentCateId(categoryId, parentCateId), "CategoryId", "CategoryName");
                return Json(new { ddl_CategoryLv = ddl_CategoryLv, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" + exc.Message });
            }
        }
        #endregion
        #endregion

        #region ProductInCategory
        #region 1. Json Method: LoadEditProductInCategory
        //Description: display Index
        public JsonResult LoadEditProductInCategory(long productInCategoryId)
        {
            try
            {
                CategoryRepository _iCategoryService = new CategoryRepository();
                ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();

                ProductInCategory pic = _iProductInCategoryService.Get_ProductInCategoryById(productInCategoryId);

                Category category = new Category();
                Category categoryParentLv1 = new Category();
                Category categoryParentLv0 = new Category();
                SelectList ddl_CategoryLv1;
                SelectList ddl_CategoryLv2;

                category = _iCategoryService.Get_CategoryById((long)pic.CategoryId);

                if (category.CateLevel == 2)
                {
                    categoryParentLv1 = _iCategoryService.Get_CategoryById((long)category.ParentCateId);
                    categoryParentLv0 = _iCategoryService.Get_CategoryById((long)categoryParentLv1.ParentCateId);

                    ddl_CategoryLv1 = new SelectList(_iCategoryService.GetList_Category_CateLevel_ParentCateId(1, Convert.ToInt32(categoryParentLv0.CategoryId)), "CategoryId", "CategoryName");
                    ddl_CategoryLv2 = new SelectList(_iCategoryService.GetList_Category_CateLevel_ParentCateId(2, Convert.ToInt32(categoryParentLv1.CategoryId)), "CategoryId", "CategoryName");

                    return Json(new { categoryLast = category, categoryParentLv1 = categoryParentLv1, categoryParentLv0 = categoryParentLv0, ddl_CategoryLv1 = ddl_CategoryLv1, ddl_CategoryLv2 = ddl_CategoryLv2, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
                }
                else if (category.CateLevel == 1)
                {
                    categoryParentLv0 = _iCategoryService.Get_CategoryById((long)category.ParentCateId);

                    ddl_CategoryLv1 = new SelectList(_iCategoryService.GetList_Category_CateLevel_ParentCateId(1, Convert.ToInt32(categoryParentLv0.CategoryId)), "CategoryId", "CategoryName");
                    ddl_CategoryLv2 = new SelectList(_iCategoryService.GetList_Category_CateLevel_ParentCateId(2, Convert.ToInt32(category.CategoryId)), "CategoryId", "CategoryName");

                    return Json(new { categoryLast = category, categoryParentLv0 = categoryParentLv0, ddl_CategoryLv1 = ddl_CategoryLv1, ddl_CategoryLv2 = ddl_CategoryLv2, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    ddl_CategoryLv1 = new SelectList(_iCategoryService.GetList_Category_CateLevel_ParentCateId(1, Convert.ToInt32(category.CategoryId)), "CategoryId", "CategoryName");
                    ddl_CategoryLv2 = new SelectList(new List<Category>(), "CategoryId", "CategoryName");

                    return Json(new { categoryLast = category, ddl_CategoryLv1 = ddl_CategoryLv1, ddl_CategoryLv2 = ddl_CategoryLv2, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exc)
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" + exc.Message });
            }
        }
        #endregion

        #region 1. Json Method: EditProductInCategory
        //Description: display Index
        [HttpPost]
        public JsonResult EditProductInCategory(long productInCategoryId, long categoryId)
        {
            try
            {
                ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();
                CategoryRepository _iCategoryService = new CategoryRepository();

                ProductInCategory productInCategory = _iProductInCategoryService.Get_ProductInCategoryById(productInCategoryId);
                productInCategory.CategoryId = categoryId;
                bool updateStatus = _iProductInCategoryService.Update(productInCategory);

                if (updateStatus == true)
                {
                    IList<ProductInCategory> lst_productInCategory = _iProductInCategoryService.GetList_ProductInCategory_ProductId((long)productInCategory.ProductId);

                    var lst_Category = _iCategoryService.GetList_CategoryAll();

                    return Json(new { list_productInCategory = lst_productInCategory, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false, Message = "An error occurred, please try later!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exc)
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" + exc.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 1. Json Method: ChangeIsActiveProductInCategory
        //Description: display Index
        public ActionResult ChangeIsActiveProductInCategory(long id)
        {
            ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();

            ProductInCategory productInCategory = _iProductInCategoryService.Get_ProductInCategoryById(id);

            bool updateStatus = _iProductInCategoryService.UpdateIsActive(productInCategory.ProductInCategoryId, (productInCategory.IsActive == true ? false : true));
            return RedirectToAction("ProductEdit", "MSS", new { tabActive = "tabthree", id = productInCategory.ProductId });
        }
        #endregion
        #endregion

        #region Media
        #region 1. Action Method: MediaIsActive
        //Description: display Index
        public ActionResult MediaIsActive(int? page, long mediaId, long mallId, long storeId, long productId)
        {
            try
            {
                MediaRepository _iMediaService = new MediaRepository();

                Media media = _iMediaService.Get_MediaById(mediaId);
                bool updateStatus = false;

                if (mallId != 0 && storeId == 0 && productId == 0)
                {
                    updateStatus = _iMediaService.UpdateActive(media.MediaId, (media.IsActive == true ? false : true));

                    return RedirectToAction("MallMedia", "MSS");
                }
                if (mallId == 0 && storeId != 0 && productId == 0)
                {
                    updateStatus = _iMediaService.UpdateActive(media.MediaId, (media.IsActive == true ? false : true));

                    return RedirectToAction("StoreEdit", "MSS", new { tabActive = "tabthree", id = storeId });
                }
                if (mallId == 0 && storeId == 0 && productId != 0)
                {
                    updateStatus = _iMediaService.UpdateActive(media.MediaId, (media.IsActive == true ? false : true));

                    return RedirectToAction("ProductEdit", "MSS", new { tabActive = "tabtwo", id = productId });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exc)
            {
                return null;
            }
        }
        public ActionResult MediaIsActiveCategory(long mediaId, long categoryId)
        {
            try
            {
                MediaRepository _iMediaService = new MediaRepository();
                VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
                Media media = _iMediaService.Get_MediaById(mediaId);

                bool updateStatus = _iMediaService.UpdateActive(media.MediaId, (media.IsActive == true ? false : true));
                ViewBag.TabIsActive = "tabtwo";
                return RedirectToAction("CategoryEdit", "MSS", new { tabActive = "tabtwo", id = categoryId });
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public ActionResult MediaIsActiveGift(long mediaId, long giftId)
        {
            try
            {
                MediaRepository _iMediaService = new MediaRepository();
                VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
                Media media = _iMediaService.Get_MediaById(mediaId);

                bool updateStatus = _iMediaService.UpdateActive(media.MediaId, (media.IsActive == true ? false : true));
                ViewBag.TabIsActive = "tabtwo";
                return RedirectToAction("GiftEdit", "MSS", new { tabActive = "tabtwo", id = giftId });
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public ActionResult MediaIsActiveBrand(long mediaId, long brandId)
        {
            try
            {
                MediaRepository _iMediaService = new MediaRepository();
                VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();

                Media media = _iMediaService.Get_MediaById(mediaId);

                bool updateStatus = _iMediaService.UpdateActive(media.MediaId, (media.IsActive == true ? false : true));
                ViewBag.TabIsActive = "tabtwo";
                return RedirectToAction("BrandEdit", "MSS", new { tabActive = "tabtwo", id = brandId });
            }
            catch (Exception exc)
            {
                return null;
            }
        }
        #endregion

        #region 1. Json Method: ChangeLinkMediaProduct
        //Description: display Index
        public JsonResult ChangeLinkMediaProduct(long productId, string mediaTypeCode, string urlMediaFile)
        {
            return null;
        }
        #endregion
        #endregion

        #region SearchAdvanced
        //Description: display Index
        public ActionResult SearchAdvanced(int? page, int? selecttype, string searchkeywords, bool? chk_Product, bool? chk_Store, bool? chk_Article)
        {

            StoreRepository _iStoreService = new StoreRepository();
            ProductRepository _iProductService = new ProductRepository();
            ArticleRepository _iArticleService = new ArticleRepository();

            SearchViewModel searchModel = new SearchViewModel();
            IList<SearchForm> lst_searchResult = new List<SearchForm>();

            int pageNum = (page ?? 1);
            ViewBag.page = page;
            if (searchkeywords == null) searchkeywords = "";
            ViewBag.searchkeywords = searchkeywords;

            if (selecttype == 0)
            {
                chk_Store = false;
                chk_Product = false;
                chk_Article = false;
            }
            if (selecttype == 1)
            {
                chk_Store = true;
                chk_Product = false;
                chk_Article = false;
            }
            if (selecttype == 2)
            {
                chk_Store = false;
                chk_Product = true;
                chk_Article = false;
            }
            if (selecttype == 3)
            {
                chk_Store = false;
                chk_Product = false;
                chk_Article = true;
            }

            if (chk_Store == true)
            {
                ViewBag.chk_Store = chk_Store;
            }
            else
            {
                ViewBag.chk_Store = false;
            }

            if (chk_Product == true)
            {
                ViewBag.chk_Product = chk_Product;
            }
            else
            {
                ViewBag.chk_Product = false;
            }

            if (chk_Article == true)
            {
                ViewBag.chk_Article = chk_Article;
            }
            else
            {
                ViewBag.chk_Article = false;
            }

            LoadSearchFormPage();

            IList<Store> lst_Store = new List<Store>();
            IList<Product> lst_Product = new List<Product>();
            IList<Article> lst_Article = new List<Article>();

            if (chk_Store == true)
            {
                lst_Store = _iStoreService.SearchStore(searchkeywords);
                if (chk_Product == true)
                {
                    lst_Product = _iProductService.SearchProduct(searchkeywords);
                }
                if (chk_Article == true)
                {
                    lst_Article = _iArticleService.SearchArticle(searchkeywords);
                }
            }
            else if (chk_Product == true)
            {
                lst_Product = _iProductService.SearchProduct(searchkeywords);
                if (chk_Store == true)
                {
                    lst_Product = _iProductService.SearchProduct(searchkeywords);
                }
                if (chk_Article == true)
                {
                    lst_Article = _iArticleService.SearchArticle(searchkeywords);
                }
            }
            else if (chk_Article == true)
            {
                lst_Article = _iArticleService.SearchArticle(searchkeywords);
                if (chk_Store == true)
                {
                    lst_Store = _iStoreService.SearchStore(searchkeywords);
                }
                if (chk_Product == true)
                {
                    lst_Product = _iProductService.SearchProduct(searchkeywords);
                }
            }
            else
            {
                lst_Store = _iStoreService.SearchStore(searchkeywords);
                lst_Product = _iProductService.SearchProduct(searchkeywords);
                lst_Article = _iArticleService.SearchArticle(searchkeywords);
            }

            foreach (var item in lst_Store)
            {
                SearchForm searchForm = new SearchForm();
                searchForm.store = item;
                lst_searchResult.Add(searchForm);
            }
            foreach (var item in lst_Product)
            {
                SearchForm searchForm = new SearchForm();
                searchForm.product = item;
                lst_searchResult.Add(searchForm);
            }
            foreach (var item in lst_Article)
            {
                SearchForm searchForm = new SearchForm();
                searchForm.article = item;
                lst_searchResult.Add(searchForm);
            }

            searchModel.list_SearchResult = lst_searchResult.ToPagedList(pageNum, pageSizeDefault);

            return View(searchModel);
        }

        #endregion

        #region Gift
        #region 1. Action Method: GiftIndex
        public ActionResult GiftIndex(int? page)
        {
            GiftRepository _iGiftService = new GiftRepository();
            IList<Gift> lst_Gift = _iGiftService.GetList_GiftAll().ToList();


            int pageNum = (page ?? 1);
            ViewBag.page = page;

            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////


            ViewBag.SideBarMenu = "GiftIndex";
            return View(lst_Gift.ToPagedList(pageNum, pageSizeDefault));
        }
        #endregion

        #region 2. Action Method: GiftCreate
        [HttpPost, ValidateInput(false)]
        public ActionResult GiftCreate(Gift GiftCollection)
        {
            GiftRepository _iGiftService = new GiftRepository();
            Account accOnline = (Account)Session["Account"];

            GiftCollection.DateCreated = DateTime.Now;
            if (this.ValidateGiftForm(GiftCollection, null) == true)
            {
                long giftId = _iGiftService.InsertGift(GiftCollection);

                if (giftId != -1) //Insert success
                {
                    TempData["ResponseMessage"] = 1; //1: Success
                    return RedirectToAction("GiftIndex", "MSS");
                }
            }
            ViewBag.ResponseMessage = 0;
            ViewBag.SideBarMenu = "GiftIndex";
            return View(GiftCollection);
        }

        public ActionResult GiftCreate()
        {
            ViewBag.SideBarMenu = "GiftIndex";
            return View(new Gift());
        }
        #endregion

        #region 3. Action Method: GiftEdit
        public ActionResult GiftEdit(int id)
        {
            GiftRepository _iGiftService = new GiftRepository();
            Gift gift = _iGiftService.Get_GiftById(id);
            LoadGiftFormPage(id);
            ViewBag.SideBarMenu = "GiftIndex";
            return View(gift);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GiftEdit(Gift GiftCollection, HttpPostedFileBase giftBannerFile, int? saveType)
        {
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            MediaRepository _iMediaService = new MediaRepository();
            GiftRepository _iGiftService = new GiftRepository();
            Account accOnline = (Account)Session["Account"];
            Gift gift = new Gift();

            Gift old_Gift = _iGiftService.Get_GiftById(GiftCollection.GiftId);//lấy gift cũ lên để check update
            if (this.ValidateGiftForm(GiftCollection, giftBannerFile) == true)
            {
                //xử lý thêm ảnh
                string createGiftsFolder = Path.Combine(Server.MapPath("~/Media"), "Gift");
                if (!System.IO.Directory.Exists(createGiftsFolder))
                {
                    Directory.CreateDirectory(createGiftsFolder);
                }
                string createGiftFolder = Path.Combine(createGiftsFolder, "G" + GiftCollection.GiftId);// create folder Gift

                if (!System.IO.Directory.Exists(createGiftFolder))
                {
                    Directory.CreateDirectory(createGiftFolder);
                }
                if (giftBannerFile != null)
                {
                    //===============================================
                    String nameImage = CreateNewName(Path.GetExtension(giftBannerFile.FileName), GIFTBANNER_MEDIATYPECODE);
                    string fullPathName = Path.Combine(createGiftFolder, nameImage); //path full
                    ImageHelper.UploadImage(giftBannerFile, fullPathName);
                    //===============================================
                    Media media = new Media();

                    media.MediaTypeId = _iMediaTypeService.Get_MediaTypeByCode(GIFTBANNER_MEDIATYPECODE).MediaTypeId;
                    media.MediaName = nameImage;
                    media.Url = "/Media/Gift/G" + GiftCollection.GiftId + "/";

                    if (old_Gift.BannerMediaId == null)
                    {
                        long new_mediaId = _iMediaService.Insert(media);
                        if (new_mediaId != -1)
                            GiftCollection.BannerMediaId = new_mediaId;
                    }
                    else
                    {
                        media.MediaId = (long)old_Gift.BannerMediaId;
                        _iMediaService.Update(media);
                    }
                }

                bool kq = _iGiftService.UpdateGift(GiftCollection);

                if (kq == true) //Insert success
                {
                    if (saveType != 1)
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("GiftIndex", "MSS");
                    }
                    else
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("GiftEdit", "MSS", new { id = GiftCollection.GiftId });
                    }
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    ViewBag.SideBarMenu = "GiftIndex";
                    return View(GiftCollection);
                }
            }
            else
            {
                ViewBag.SideBarMenu = "GiftIndex";
                return View(GiftCollection);
            }
        }
        #endregion
        #endregion

        #endregion

        #region Common Method (key:CM):

        //Store
        #region 1. Common Method: LoadStoreFormPage
        //Description: load store page
        private void LoadStoreFormPage(long storeId)
        {
            StoreRepository _iStoreService = new StoreRepository();
            VendorRepository _iVendorService = new VendorRepository();
            ViewBag.list_Vendor = _iVendorService.GetList_VendorAll();

            if (storeId > 0)
            {
                StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
                MediaRepository _iMediaService = new MediaRepository();
                MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();

                Store store = _iStoreService.Get_StoreById(storeId);

                IList<StoreInMedia> lst_StoreInMedia = _iStoreInMediaService.GetList_StoreInMedia_StoreId(storeId).ToList();
                IList<Media> lst_MediaSHB = new List<Media>();
                IList<Media> lst_MediaSB = new List<Media>();
                IList<Media> lst_MediaSCNB = new List<Media>();

                foreach (var item in lst_StoreInMedia)
                {
                    Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREHIGHLIGHT_MEDIATYPECODE).MediaTypeId)
                    {
                        lst_MediaSHB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREBANNER_MEDIATYPECODE).MediaTypeId)
                    {
                        lst_MediaSB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STORECOMPLEXNAME_MEDIATYPECODE).MediaTypeId)
                    {
                        lst_MediaSCNB.Add(media);
                    }
                }

                ViewBag.list_StoreHighLightBanner = lst_MediaSHB;
                ViewBag.list_StoreBanner = lst_MediaSB;
                ViewBag.list_StoreComplexNameBanner = lst_MediaSCNB;

                ViewBag.list_MediaType = _iMediaTypeService.GetList_MediaTypeAll();

                ViewBag.listStoreShowInMall = _iStoreService.GetList_StoreAll_ShowIsMall(true, false, true, true);

                ViewBag.CreateByName = this.GetAccountByName((long)store.CreatedBy);
                ViewBag.ModifiedByName = this.GetAccountByName(store.ModifiedBy ?? 0);
            }
        }
        #endregion

        #region 7. Common Method: ValidateStoreForm
        private bool ValidateStoreForm(Store StoreCollection, HttpPostedFileBase storeHBFile, HttpPostedFileBase storeCNBFile, List<HttpPostedFileBase> storeBFiles, int? currentPage)
        {
            StoreRepository _iStoreService = new StoreRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();

            bool valid = true;
            if (currentPage == 2) // 
            {
                Store Storedb = _iStoreService.Get_StoreById(StoreCollection.StoreId);
                

                //int MaxContentLength = 500; //2 MB
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };

                IList<StoreInMedia> lst_StoreInMedia = _iStoreInMediaService.GetList_StoreInMedia_StoreId(StoreCollection.StoreId).ToList();

                IList<Media> lst_MediaSHB = new List<Media>();
                IList<Media> lst_MediaSB = new List<Media>();
                IList<Media> lst_MediaSCNB = new List<Media>();

                foreach (var item in lst_StoreInMedia)
                {
                    Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                    {
                        lst_MediaSHB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREBANNER_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                    {
                        lst_MediaSB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STORECOMPLEXNAME_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                    {
                        lst_MediaSCNB.Add(media);
                    }
                }

                if (storeHBFile != null)
                {
                    MediaType mediaType_StoreHB = _iMediaTypeService.Get_MediaTypeByCode(STOREHIGHLIGHT_MEDIATYPECODE);

                    if (!AllowedFileExtensions.Contains(storeHBFile.FileName.Substring(storeHBFile.FileName.LastIndexOf('.')).ToLower()))
                    {
                        ModelState.AddModelError("storeHBFileType", "Please upload Store Highlight Banner of type: " + string.Join(", ", AllowedFileExtensions));
                        valid = false;
                    }
                    if (storeHBFile.ContentLength / 1024 > mediaType_StoreHB.MaxSize)
                    {
                        ModelState.AddModelError("storeHBFileMaxSize", "Store Highlight Banner is too large, maximum allowed size is : " + mediaType_StoreHB.MaxSize.ToString() + "KB");
                        valid = false;
                    }
                    if (storeHBFile.ContentLength / 1024 < mediaType_StoreHB.MinSize)
                    {
                        ModelState.AddModelError("storeHBFileMinSize", storeHBFile.FileName + " is too small, minimize allowed size is : " + mediaType_StoreHB.MinSize.ToString() + "KB");
                        valid = false;
                    }
                }

                if (storeCNBFile != null)
                {
                    MediaType mediaType_StoreCNB = _iMediaTypeService.Get_MediaTypeByCode(STORECOMPLEXNAME_MEDIATYPECODE);

                    if (!AllowedFileExtensions.Contains(storeCNBFile.FileName.Substring(storeCNBFile.FileName.LastIndexOf('.')).ToLower()))
                    {
                        ModelState.AddModelError("storeCNBFileType", "Please upload Store Complex Name Banner of type: " + string.Join(", ", AllowedFileExtensions));
                        valid = false;
                    }
                    if (storeCNBFile.ContentLength / 1024 > mediaType_StoreCNB.MaxSize)
                    {
                        ModelState.AddModelError("storeCNBFileMaxSize", "Store Complex Name Banner is too large, maximum allowed size is : " + mediaType_StoreCNB.MaxSize.ToString() + "KB");
                        valid = false;
                    }
                    if (storeCNBFile.ContentLength / 1024 < mediaType_StoreCNB.MinSize)
                    {
                        ModelState.AddModelError("storeCNBFileMinSize", storeCNBFile.FileName + " is too small, minimize allowed size is : " + mediaType_StoreCNB.MinSize.ToString() + "KB");
                        valid = false;
                    }
                }
                if (storeBFiles != null && storeBFiles[0] != null)
                {
                    MediaType mediaType_StoreB = _iMediaTypeService.Get_MediaTypeByCode(STOREBANNER_MEDIATYPECODE);

                    int t = 1;

                    foreach (HttpPostedFileBase file in storeBFiles)
                    {

                        if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower()))
                        {
                            ModelState.AddModelError("storeBFilesType" + t, file.FileName + " not image type. Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                            valid = false;
                        }
                        if (file.ContentLength / 1024 > mediaType_StoreB.MaxSize)
                        {
                            ModelState.AddModelError("storeBFilesMaxSize" + t, file.FileName + " is too large, maximum allowed size is : " + mediaType_StoreB.MaxSize.ToString() + "KB");
                            valid = false;
                        }
                        if (file.ContentLength / 1024 < mediaType_StoreB.MinSize)
                        {
                            ModelState.AddModelError("storeBFilesMinSize" + t, file.FileName + " is too small, minimize allowed size is : " + mediaType_StoreB.MinSize.ToString() + "KB");
                            valid = false;
                        }
                        t++;
                    }

                    if ((lst_MediaSB.Count + storeBFiles.Count) > 5)
                    {
                        ModelState.AddModelError("storeBImageCount", "Store Banner is limit 5 pictures");
                        valid = false;
                    }
                }
                
                if (StoreCollection.ShowInMallPage == true)
                {
                    IList<Store> lst_Storedb = _iStoreService.GetList_StoreAll_ShowIsMall(true, false, true, true);
                    TempData["tabActive"] = "tabsix";
                    if (lst_Storedb.Count >= 20)
                    {
                        ModelState.AddModelError("ShowInMallPage1", "ShowInMall is limit 20 stores");
                        valid = false;
                    }
                    if (StoreCollection.IsDeleted == true || StoreCollection.IsVerified == false || StoreCollection.IsActive == false)
                    {
                        ModelState.AddModelError("ShowInMallPage2", "Store can't show in mall !! Let's it Checking [IsDeleted], [IsVerified], [IsActive] in [Store Info]");
                        valid = false;
                    }
                }
                else 
                {
                    TempData["tabActive"] = "tabsix";
                }
            }
            if (StoreCollection.StoreName == null)
            {
                ModelState.AddModelError("StoreName", "Store Name is empty !!");
                valid = false;
            }
            if (StoreCollection.OnlineDate >= StoreCollection.OfflineDate)
            {
                ModelState.AddModelError("EndDate", "End Date invalid! Date must be after Start Date");
                valid = false;
            }

            return valid;
        }

        #endregion

        #region 1. Common Method: ShowStoreWarning
        private void ShowStoreWarning(Store StoreCollection)
        {
            StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();

            IList<StoreInMedia> lst_StoreInMedia = _iStoreInMediaService.GetList_StoreInMedia_StoreId(StoreCollection.StoreId).ToList();
            IList<Media> lst_MediaSHB = new List<Media>();
            IList<Media> lst_MediaSB = new List<Media>();
            IList<Media> lst_MediaSCNB = new List<Media>();

            foreach (var item in lst_StoreInMedia)
            {
                Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                {
                    lst_MediaSHB.Add(media);
                }
                if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STOREBANNER_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                {
                    lst_MediaSB.Add(media);
                }
                if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(STORECOMPLEXNAME_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                {
                    lst_MediaSCNB.Add(media);
                }
            }

            if (StoreCollection.OnlineDate == null)
            {
                ModelState.AddModelError("StartDateWarning", "Must input [Start Date] in [Store Info]");
            }
            if (StoreCollection.OfflineDate == null)
            {
                ModelState.AddModelError("EndDateWarning", "Must input [End Date] in [Store Info]");
            }
            if (StoreCollection.IsDeleted == true)
            {
                ModelState.AddModelError("IsDeletedWarning", "Must input [IsDeleted] is No in [Store Info]");
            }
            if (StoreCollection.IsVerified == false)
            {
                ModelState.AddModelError("IsVerifiedWarning", "Must input [IsVerified] is Yes in [Store Info]");
            }
            if (StoreCollection.IsActive == false)
            {
                ModelState.AddModelError("IsActiveWarning", "Must input [IsActive] is Yes in [Store Info]");
            }
            if (StoreCollection.ShowInMallPage == false)
            {
                ModelState.AddModelError("ShowInMallPageWarning", "Must input [ShowInMallPage] is Yes in [Store Info]");
            }
            if (lst_MediaSHB.Count == 0)
            {
                ModelState.AddModelError("StoreHLBWarning", "Must upload [Store Highlight Banner] image in [Media]");
            }
            if (lst_MediaSB.Count == 0)
            {
                ModelState.AddModelError("StoreBWarning", "Must upload [Store Banner] image in [Media]");
            }
            List<string> listWarning = ModelState.Values.Where(E => E.Errors.Count > 0)
                                                 .SelectMany(E => E.Errors)
                                                 .Select(E => E.ErrorMessage)
                                                 .ToList();

            TempData["listStoreWarning"] = listWarning;
        }
        #endregion

        //Product
        #region 1. Common Method: LoadProductFormPage
        private void LoadProductFormPage(long productId)
        {
            ProductRepository _iProductService = new ProductRepository();
            ProductTypeRepository _iProductTypeService = new ProductTypeRepository();
            ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            StoreRepository _iStoreService = new StoreRepository();
            ProductStatusRepository _iProductStatusService = new ProductStatusRepository();
            BrandRepository _iBrandService = new BrandRepository();
            CategoryRepository _iCategoryService = new CategoryRepository();
            SizeRepository _iSizeService = new SizeRepository();
            SizeGlobalRepository _iSizeGlobalService = new SizeGlobalRepository();
            if (productId > 0)// editform
            {
                Product product = _iProductService.Get_ProductById(productId);
                IList<ProductInMedia> lst_ProductInMedia = _iProductInMediaService.Get_ProductInMedia_ProductId(productId).ToList();
                IList<Media> lst_MediaPHB = new List<Media>();
                IList<Media> lst_MediaPB = new List<Media>();
                IList<Media> lst_MediaProductColourImage = new List<Media>();

                foreach (var item in lst_ProductInMedia)
                {
                    Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                    {
                        lst_MediaPHB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTBANNER_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                    {
                        lst_MediaPB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                    {
                        lst_MediaProductColourImage.Add(media);
                    }
                }
                if (lst_MediaPHB.Count == 0)
                {
                    IList<ProductInMedia> lst_ProductGroupInMedia = _iProductInMediaService.Get_ProductInMedia_ProductId((long)product.GroupProductId).ToList();
                    foreach (var item in lst_ProductGroupInMedia)
                    {
                        Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                        if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsDeleted == false)
                        {
                            lst_MediaPHB.Add(media);
                        }
                    }
                }

                ViewBag.list_ProductInCategory = _iProductInCategoryService.GetList_ProductInCategory_ProductId(productId);
                ViewBag.list_ProductHighLightBanner = lst_MediaPHB;
                ViewBag.list_ProductBanner = lst_MediaPB;
                ViewBag.list_ProductColourImage = lst_MediaProductColourImage;
                ViewBag.list_MediaType = _iMediaTypeService.GetList_MediaTypeAll();
                ViewBag.list_ProductShowInStore = _iProductService.GetList_ProductAll_ShowInStore((long)product.StoreId, true, false, true, true);
                ViewBag.CreateByName = this.GetAccountByName((long)product.CreatedBy);
                ViewBag.ModifiedByName = this.GetAccountByName(product.ModifiedBy ?? 0);
            }

            ViewBag.list_Store = _iStoreService.GetList_StoreAll();
            ViewBag.list_ProductStatus = _iProductStatusService.GetList_ProductStatusAll();
            ViewBag.list_ProductType = _iProductTypeService.GetList_ProductTypeAll();
            ViewBag.list_Brand = _iBrandService.GetList_BrandAll();
            ViewBag.list_SizeGlobal = _iSizeGlobalService.GetList_SizeGlobalAll();
            ViewBag.list_Size = _iSizeService.GetList_SizeAll();
            ViewBag.ddl_CategoryLv0 = _iCategoryService.GetList_Category_CateLevel_ParentCateId(0, 0);
            ViewBag.ddl_CategoryLv1 = _iCategoryService.GetList_Category_CateLevel_ParentCateId(1, -1);
            ViewBag.ddl_CategoryLv2 = _iCategoryService.GetList_Category_CateLevel_ParentCateId(2, -1);
            ViewBag.list_Category = _iCategoryService.GetList_CategoryAll();
        }
        #endregion

        #region 2. Common Method: ValidateProductForm
        private bool ValidateProductForm(Product ProductCollection, List<long> CategoryId, HttpPostedFileBase productHBFile, HttpPostedFileBase productColourFile, List<HttpPostedFileBase> productBFiles, HttpPostedFileBase VideoFile, string URLYoutube, string newColour, int? saveType)
        {
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            CategoryRepository _iCategoryService = new CategoryRepository();
            MediaRepository _iMediaService = new MediaRepository();
            ProductInCategoryRepository _iProductInCategoryService = new ProductInCategoryRepository();

            bool valid = true;
            if (ProductCollection.ProductTypeCode == "PT1")
            {
                //Product ProductCollection, List<long> CategoryId, HttpPostedFileBase productHBFile, HttpPostedFileBase productCNBFile, List<HttpPostedFileBase> productBFiles
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
                IList<ProductInMedia> lst_ProductInMedia = _iProductInMediaService.Get_ProductInMedia_ProductId(ProductCollection.ProductId).ToList();
                IList<Media> lst_MediaPHB = new List<Media>();
                IList<Media> lst_MediaPB = new List<Media>();
                IList<Media> lst_MediaPCNB = new List<Media>();

                foreach (var item in lst_ProductInMedia)
                {
                    Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                    {
                        lst_MediaPHB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTBANNER_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                    {
                        lst_MediaPB.Add(media);
                    }
                    if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                    {
                        lst_MediaPCNB.Add(media);
                    }
                }

                if (productHBFile != null)
                {
                    MediaType mediaType_ProductHB = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE);

                    if (!AllowedFileExtensions.Contains(productHBFile.FileName.Substring(productHBFile.FileName.LastIndexOf('.')).ToLower()))
                    {
                        ModelState.AddModelError("productHBFileType", "Please upload [Product Highlight Banner] of type: " + string.Join(", ", AllowedFileExtensions));
                        valid = false;
                    }
                    if (productHBFile.ContentLength / 1024 > mediaType_ProductHB.MaxSize)
                    {
                        ModelState.AddModelError("productHBFileMaxSize", productHBFile.FileName + " is too large, maximum allowed size is : " + mediaType_ProductHB.MaxSize.ToString() + "KB");
                        valid = false;
                    }
                    if (productHBFile.ContentLength / 1024 < mediaType_ProductHB.MinSize)
                    {
                        ModelState.AddModelError("productHBFileMinSize", productHBFile.FileName + " is too small, minimize allowed size is : " + mediaType_ProductHB.MinSize.ToString() + "KB");
                        valid = false;
                    }
                }

                if (productColourFile != null)
                {
                    MediaType mediaType_ProductColour = _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE);

                    if (!AllowedFileExtensions.Contains(productColourFile.FileName.Substring(productColourFile.FileName.LastIndexOf('.')).ToLower()))
                    {
                        ModelState.AddModelError("productColourFileType", "Please upload [Product Colour Avatar] of type: " + string.Join(", ", AllowedFileExtensions));
                        valid = false;
                    }
                    if (productColourFile.ContentLength / 1024 > mediaType_ProductColour.MaxSize)
                    {
                        ModelState.AddModelError("productColourFileMaxSize", productColourFile.FileName + " is too large, maximum allowed size is : " + mediaType_ProductColour.MaxSize.ToString() + "KB");
                        valid = false;
                    }
                    if (productColourFile.ContentLength / 1024 < mediaType_ProductColour.MinSize)
                    {
                        ModelState.AddModelError("productColourFileMinSize", productColourFile.FileName + " is too small, minimize allowed size is : " + mediaType_ProductColour.MinSize.ToString() + "KB");
                        valid = false;
                    }
                }
                if (productBFiles != null && productBFiles[0] != null)
                {
                    MediaType mediaType_ProductB = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTBANNER_MEDIATYPECODE);

                    int t = 1;

                    foreach (HttpPostedFileBase file in productBFiles)
                    {
                        if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower()))
                        {
                            ModelState.AddModelError("productBFilesType" + t, file.FileName + " not image type. Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                            valid = false;
                        }
                        if (file.ContentLength / 1024 > mediaType_ProductB.MaxSize)
                        {
                            ModelState.AddModelError("productBFilesMaxSize" + t, file.FileName + " is too large, maximum allowed size is : " + mediaType_ProductB.MaxSize.ToString() + "KB");
                            valid = false;
                        }
                        if (file.ContentLength / 1024 < mediaType_ProductB.MinSize)
                        {
                            ModelState.AddModelError("productBFilesMinSize" + t, file.FileName + " is too small, minimize allowed size is : " + mediaType_ProductB.MinSize.ToString() + "KB");
                            valid = false;
                        }
                        t++;
                    }

                    if ((lst_MediaPB.Count + productBFiles.Count) > 5)
                    {
                        ModelState.AddModelError("productBImageCount", "[Product Banner] is limit 5 pictures");
                        valid = false;
                    }
                }

                if (saveType == 2)
                {
                    if (productColourFile == null)
                    {
                        ModelState.AddModelError("productColourFile", "[Colour Avatar] is empty !!");
                        valid = false;
                    }
                    if (newColour == null)
                    {
                        ModelState.AddModelError("newColour", "[Colour Name] is empty !!");
                        valid = false;
                    }
                }
            }
            else if (ProductCollection.ProductTypeCode == "PT2")
            {
                if (VideoFile != null && URLYoutube == null)
                {
                    //Product ProductCollection, HttpPostedFileBase VideoFile, string URLYoutube
                    MediaType mediaType_ProductVideo = _iMediaTypeService.Get_MediaTypeByCode(PRODUCTVIDEO_MEDIATYPECODE);
                    string[] AllowedFileExtensions = new string[] { ".mp4", ".flv" };

                    if (!AllowedFileExtensions.Contains(VideoFile.FileName.Substring(VideoFile.FileName.LastIndexOf('.')).ToLower()))
                    {
                        ModelState.AddModelError("VideoProductType", VideoFile.FileName + " not video type. Please upload video of type: " + string.Join(", ", AllowedFileExtensions));
                        valid = false;
                    }
                    if (VideoFile.ContentLength / 1024 > mediaType_ProductVideo.MaxSize)
                    {
                        ModelState.AddModelError("VideoProductMaxSize", VideoFile.FileName + " is too large, maximum allowed size is : " + (mediaType_ProductVideo.MaxSize / 1024).ToString() + "MB");
                        valid = false;
                    }
                    if (VideoFile.ContentLength / 1024 < mediaType_ProductVideo.MinSize)
                    {
                        ModelState.AddModelError("VideoProductMinSize", VideoFile.FileName + " is too small, minimize allowed size is : " + mediaType_ProductVideo.MinSize.ToString() + "KB");
                        valid = false;
                    }
                }
            }
            else
            {
                //PT3,4...
            }

            if (CategoryId != null)
            {
                IList<Category> lst_CategoryDuplicate = new List<Category>();
                var lst_ProductInCategory = _iProductInCategoryService.GetList_ProductInCategory_ProductId(ProductCollection.ProductId);
                int t = 1;
                foreach (var item1 in lst_ProductInCategory)
                {
                    foreach (var item2 in CategoryId)
                    {
                        if (item2 == item1.CategoryId)
                        {
                            Category category = _iCategoryService.Get_CategoryById(item2);
                            lst_CategoryDuplicate.Add(category);

                            ModelState.AddModelError("CategoryDuplicate" + t, category.CategoryName + " is duplicate !!");
                            valid = false;
                        }
                        t++;
                    }
                }
                TempData["listCategoryDuplicate"] = lst_CategoryDuplicate;
            }

            if (ProductCollection.ProductId != 0)
            {
                if (ProductCollection.Size == null)
                {
                    ModelState.AddModelError("Size", "[Size] is empty !!");
                    valid = false;
                }
                if (ProductCollection.RetailPrice == null)
                {
                    ModelState.AddModelError("RetailPrice", "[RetailPrice] is empty !!");
                    valid = false;
                }
                if (ProductCollection.MobileOnlinePrice == null)
                {
                    ModelState.AddModelError("MobileOnlinePrice", "[MobileOnlinePrice] is empty !!");
                    valid = false;
                }
                if (ProductCollection.RetailPrice < 0)
                {
                    ModelState.AddModelError("RetailPrice", "[RetailPrice] is more than zero !!");
                    valid = false;
                }
                if (ProductCollection.MobileOnlinePrice < 0)
                {
                    ModelState.AddModelError("MobileOnlinePrice", "[MobileOnlinePrice] is more than zero !!");
                    valid = false;
                }
                if (ProductCollection.PromotePrice < 0)
                {
                    ModelState.AddModelError("PromotePrice", "[PromotePrice] is more than zero !!");
                    valid = false;
                }
                if (ProductCollection.RetailPrice > 0 && ProductCollection.MobileOnlinePrice > 0)
                {
                    if (ProductCollection.RetailPrice < ProductCollection.MobileOnlinePrice)
                    {
                        ModelState.AddModelError("MobileOnlinePrice", "Must input [Mobile Price] more than [Retail Price] in [Product Info]");
                        valid = false;
                    }
                    if (ProductCollection.RetailPrice < ProductCollection.PromotePrice)
                    {
                        ModelState.AddModelError("PromotePrice1", "Must input [Promote Price] more than [Retail Price] in [Product Info]");
                        valid = false;
                    }
                    if (ProductCollection.MobileOnlinePrice < ProductCollection.PromotePrice)
                    {
                        ModelState.AddModelError("PromotePrice2", "Must input [Promote Price] more than [Mobile Price] in [Product Info]");
                        valid = false;
                    }
                }
            }

            if (ProductCollection.StoreId == null || ProductCollection.StoreId == 0)
            {
                ModelState.AddModelError("StoreName", "ppStore Name] is empty !!");
                valid = false;
            }
            if (ProductCollection.ProductName == null)
            {
                ModelState.AddModelError("ProductName1", "[Product Name] is empty !!");
                valid = false;
            }
            else
            {
                if (ProductCollection.ProductName.Length > 40)
                {
                    ModelState.AddModelError("ProductName2", "[Product Name] length more than 40 characters !!");
                    valid = false;
                }
            }
            if (ProductCollection.ProductComplexName == null)
            {
                ModelState.AddModelError("ProductComplexName1", "[ProductComplexName1] is empty !!");
                valid = false;
            }
            else
            {
                if (ProductCollection.ProductComplexName.Length > 75)
                {
                    ModelState.AddModelError("ProductComplexName2", "[Product Complex Name] length more than 75 characters !!");
                    valid = false;
                }
            }
            if (ProductCollection.RetailPrice == null)
            {
                ModelState.AddModelError("RetailPrice", "[Retail Price] is empty !!");
                valid = false;
            }
            if (ProductCollection.MobileOnlinePrice == null)
            {
                ModelState.AddModelError("MobileOnlinePrice", "[Mobile Online Price] is empty !!");
                valid = false;
            }
            if (ProductCollection.PromotePrice == null)
            {
                ModelState.AddModelError("PromotePrice", "[Promote Price] is empty !!");
                valid = false;
            }

            if (ProductCollection.ShowInStorePage == true)
            {
                ProductRepository _iProductService = new ProductRepository();
                IList<Product> lst_Productdb = _iProductService.GetList_ProductAll_ShowIsStore((long)ProductCollection.StoreId, true, false, true, true);
                TempData["tabActive"] = "tabfive";
                if (lst_Productdb.Count >= 10)
                {
                    ModelState.AddModelError("ShowInStorePage1", "Show in Store must be limit 10 products");
                    valid = false;
                }
                if (ProductCollection.IsDeleted == true || ProductCollection.IsVerified == false || ProductCollection.IsActive == false)
                {
                    ModelState.AddModelError("ShowInStorePage2", "Product can't show in store !! Let's it Checking [IsDeleted], [IsVerified], [IsActive] in [Store Info]");
                    valid = false;
                }
            }
            else
            {
                TempData["tabActive"] = "tabfive";
            }

            return valid;

        }
        #endregion

        #region 1. Common Method: ShowStoreWarning
        private void ShowProductWarning(Product ProductCollection)
        {
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();

            IList<ProductInMedia> lst_ProductInMedia = _iProductInMediaService.Get_ProductInMedia_ProductId(ProductCollection.ProductId).ToList();
            IList<Media> lst_MediaPHB = new List<Media>();
            IList<Media> lst_MediaPB = new List<Media>();
            IList<Media> lst_MediaPColour = new List<Media>();

            foreach (var item in lst_ProductInMedia)
            {
                Media media = _iMediaService.Get_MediaById((long)item.MediaId);
                if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTHIGHLIGHT_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                {
                    lst_MediaPHB.Add(media);
                }
                if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCTBANNER_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                {
                    lst_MediaPB.Add(media);
                }
                if (media.MediaTypeId == _iMediaTypeService.Get_MediaTypeByCode(PRODUCT_COLOUR_MEDIATYPECODE).MediaTypeId && media.IsActive == true && media.IsDeleted == false)
                {
                    lst_MediaPColour.Add(media);
                }
            }

            if (ProductCollection.IsDeleted == true)
            {
                ModelState.AddModelError("IsDeletedWarning", "Must input [IsDeleted] is No in [Product Info]");
            }
            if (ProductCollection.IsVerified == false)
            {
                ModelState.AddModelError("IsVerifiedWarning", "Must input [IsVerified] is Yes in [Product Info]");
            }
            if (ProductCollection.IsActive == false)
            {
                ModelState.AddModelError("IsActiveWarning", "Must input [IsActive] is Yes in [Product Info]");
            }
            if (ProductCollection.ShowInStorePage == false)
            {
                ModelState.AddModelError("ShowInMallPageWarning", "Must input [ShowInStorePage] is Yes in [Product Info]");
            }
            if (lst_MediaPHB.Count == 0)
            {
                ModelState.AddModelError("ProductHBWarning", "Must upload [Product Highlight Banner] image in [Media]");
            }
            if (lst_MediaPB.Count == 0)
            {
                ModelState.AddModelError("ProductBWarning", "Must upload [Product Banner] image in [Media]");
            }

            List<string> listWarning = ModelState.Values.Where(E => E.Errors.Count > 0)
                                                 .SelectMany(E => E.Errors)
                                                 .Select(E => E.ErrorMessage)
                                                 .ToList();

            TempData["listProductWarning"] = listWarning;
        }
        #endregion


        #region 2. Common Method: ValidateArticleTypeForm
        private bool ValidateArticleTypeForm(ArticleType ArticleTypeCollection)
        {
            bool valid = true;

            if (ArticleTypeCollection.ArticleTypeName == null)
            {
                ModelState.AddModelError("ArticleTypeName", "Article Type Name is empty !!!");
                valid = false;
            }

            return valid;
        }
        #endregion

        #region 1. Common Method: LoadArticleTypeFormPage
        //Description: // 
        private void LoadArticleTypeFormPage(long articleId)
        {
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            ViewBag.list_ArticleType = _iArticleTypeService.GetList_ArticleTypeAll();

        }
        #endregion

        //Article
        #region 2. Common Method: ValidateArticleForm
        private bool ValidateArticleForm(Article ArticleCollection)
        {
            bool valid = true;
            if (ArticleCollection.ArticleTypeId == null || ArticleCollection.ArticleTypeId < 1)
            {
                ModelState.AddModelError("ArticleTypeId", "Article Type Name is empty !!!");
                valid = false;
            }
            if (ArticleCollection.Title == null)
            {
                ModelState.AddModelError("Title", "Title is empty !!!");
                valid = false;
            }

            return valid;
        }
        #endregion

        #region 1. Common Method: LoadArticleFormPage
        //Description: // 
        private void LoadArticleFormPage(long articleId)
        {
            ArticleTypeRepository _iArticleTypeService = new ArticleTypeRepository();
            ArticleRepository _iArticleService = new ArticleRepository();

            List<ArticleType> list_ar = _iArticleTypeService.GetList_ArticleTypeAll(true, false).ToList();
            //foreach (ArticleType item in list_ar)
            //{
            //    List<ArticleType> lst_ar = (List<Article>)ViewBag.list_Article;
            //    List<Article> lst_tmp = lst_ar.Where(x => x.Code == item.Code & x.LanguageCode == "zh").ToList();
            //    string arTitleZh = (lst_tmp.Count > 0) ? lst_tmp[0].Title : "[Add zh language]";
            //    lst_tmp = lst_ar.Where(x => x.Code == item.Code & x.LanguageCode == "en").ToList();
            //    string arTitleEn = (lst_tmp.Count > 0) ? lst_tmp[0].Title : "[Add en language]";
            //    lst_tmp = lst_ar.Where(x => x.Code == item.Code & x.LanguageCode == "vi").ToList();
            //    string arTitleVi = (lst_tmp.Count > 0) ? lst_tmp[0].Title : "[Add vi language]";

            //    item.ArticleTypeName
            //}
            List<ArticleType> temp = new List<ArticleType>();
            foreach (ArticleType item in _iArticleTypeService.GetList_ArticleTypeIndex())
            {
                List<ArticleType> lst_ar = _iArticleTypeService.GetList_ArticleTypeAll().ToList();
                List<ArticleType> lst_tmp = lst_ar.Where(x => x.Code == item.Code & x.LanguageCode == "zh").ToList();
                string arTitleZh = (lst_tmp.Count > 0) ? "[" + lst_tmp[0].ArticleTypeName + "]" : "";
                lst_tmp = lst_ar.Where(x => x.Code == item.Code & x.LanguageCode == "en").ToList();
                string arTitleEn = (lst_tmp.Count > 0) ? "[" + lst_tmp[0].ArticleTypeName + "]" : "";
                lst_tmp = lst_ar.Where(x => x.Code == item.Code & x.LanguageCode == "vi").ToList();
                string arTitleVi = (lst_tmp.Count > 0) ? "[" + lst_tmp[0].ArticleTypeName + "]" : "";
                item.ArticleTypeName = "";
                item.ArticleTypeName = arTitleEn + " " + arTitleVi + " " + arTitleZh;
                temp.Add(item);
            }
            ViewBag.list_ArticleType = temp;
            ViewBag.list_Article = _iArticleService.GetList_ArticleAll();
        }
        #endregion

        //Brand
        #region 1. Common Method: ValidateBrandForm
        private bool ValidateBrandForm(Brand BrandCollection, HttpPostedFileBase brandLogoFile, HttpPostedFileBase brandBannerFile)
        {
            bool valid = true;
            // int MaxContentLength = 2048; //2 MB
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            if (brandLogoFile != null)
            {
                MediaType mediaType_BrandLogo = _iMediaTypeService.Get_MediaTypeByCode(BRANDLOGO_MEDIATYPECODE);
                if (!AllowedFileExtensions.Contains(brandLogoFile.FileName.Substring(brandLogoFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("brandLogoFileType", "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (brandLogoFile.ContentLength / 1024 > mediaType_BrandLogo.MaxSize)
                {
                    ModelState.AddModelError("brandLogoFileMaxSize", "Your Photo is too large, maximum allowed size is : " + mediaType_BrandLogo.MaxSize.ToString() + "KB");
                    valid = false;
                }
            }

            if (brandBannerFile != null)
            {
                MediaType mediaType_BrandBanner = _iMediaTypeService.Get_MediaTypeByCode(BRANDBANNER_MEDIATYPECODE);
                if (!AllowedFileExtensions.Contains(brandBannerFile.FileName.Substring(brandBannerFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("brandBannerFileType", "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (brandBannerFile.ContentLength / 1024 > mediaType_BrandBanner.MaxSize)
                {
                    ModelState.AddModelError("brandBannerFileMaxSize", "Your Photo is too large, maximum allowed size is : " + mediaType_BrandBanner.MaxSize.ToString() + "KB");
                    valid = false;
                }
            }
            if (BrandCollection.BrandName == null)
            {
                ModelState.AddModelError("BrandName", "BrandName is empty !!");
                valid = false;
            }
            return valid;
        }
        #region 1. Common Method:LoadBrandFormPage
        private void LoadBrandFormPage(long _brandId)
        {
            if (_brandId != 0)
            {
                MediaRepository _iMediaService = new MediaRepository();
                MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
                BrandRepository _iBrandService = new BrandRepository();

                Brand old_Brand = _iBrandService.Get_BrandById(_brandId);//bớt truy xuất DB 
                //Load Media
                List<Media> lst_mediaBrandLogo = new List<Media>();
                if (old_Brand.LogoMediaId != null)
                {
                    if (_iMediaService.Get_MediaById((long)old_Brand.LogoMediaId) != null)
                        lst_mediaBrandLogo.Add(_iMediaService.Get_MediaById((long)old_Brand.LogoMediaId));
                }
                ViewBag.BrandLogoMedia = lst_mediaBrandLogo;

                List<Media> lst_mediaBrandBanner = new List<Media>();
                if (old_Brand.BannerMediaId != null)
                {
                    if (_iMediaService.Get_MediaById((long)old_Brand.BannerMediaId) != null)
                        lst_mediaBrandBanner.Add(_iMediaService.Get_MediaById((long)old_Brand.BannerMediaId));
                }
                ViewBag.BrandBannerMedia = lst_mediaBrandBanner;

                ViewBag.list_MediaType = _iMediaTypeService.GetList_MediaTypeAll_IsDeleted_IsActive(false, true).ToList();
            }

        }
        #endregion
        #endregion

        //Search
        #region 1. Common Method: LoadSearchFormPage
        //Description: // 
        private void LoadSearchFormPage()
        {

        }
        #endregion

        //Gift
        #region 1. Common Method: ValidateGiftForm
        private bool ValidateGiftForm(Gift GiftCollection, HttpPostedFileBase giftBannerFile)
        {
            bool valid = true;
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();

            if (giftBannerFile != null)
            {
                MediaType mediaType_GiftBanner = _iMediaTypeService.Get_MediaTypeByCode(GIFTBANNER_MEDIATYPECODE);
                if (!AllowedFileExtensions.Contains(giftBannerFile.FileName.Substring(giftBannerFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("giftBannerFileType", "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (giftBannerFile.ContentLength / 1024 > mediaType_GiftBanner.MaxSize)
                {
                    ModelState.AddModelError("giftBannerFileMaxSize", "Your Photo is too large, maximum allowed size is : " + mediaType_GiftBanner.MaxSize.ToString() + "KB");
                    valid = false;
                }
            }
            if (GiftCollection.GiftName == null)
            {
                ModelState.AddModelError("GiftName", "Gift Name is empty !!");
                valid = false;
            }
            if (GiftCollection.GiftPrice == null)
            {
                ModelState.AddModelError("GiftPrice", "Gift Price is empty !!");
                valid = false;
            }
            return valid;
        }

        #endregion
        
        #region 2. Common Medthod: LoadGiftFormPage
        private void LoadGiftFormPage(long _giftId)
        {
            if (_giftId != 0)
            {
                MediaRepository _iMediaService = new MediaRepository();
                MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
                GiftRepository _iGiftService = new GiftRepository();
                Gift old_Gift = _iGiftService.Get_GiftById(_giftId);//bớt truy xuất DB 
                //Load Media   
                List<Media> lst_mediaGiftBanner = new List<Media>();
                if (old_Gift.BannerMediaId != null)
                {
                    if (_iMediaService.Get_MediaById((long)old_Gift.BannerMediaId) != null)
                        lst_mediaGiftBanner.Add(_iMediaService.Get_MediaById((long)old_Gift.BannerMediaId));
                }
                ViewBag.GiftBannerMedia = lst_mediaGiftBanner;
                ViewBag.list_MediaType = _iMediaTypeService.GetList_MediaTypeAll_IsDeleted_IsActive(false, true).ToList();
            }
        }
        #endregion

        //ProductInPriority
        #region 1. Common Method: validateProductInPriority
        private bool ValidateProductInPriority(ProductInPriority productInpriority)
        {
            ProductInPriorityRepository _iProductInPriorityService = new ProductInPriorityRepository();
            bool valid = true;
            if (productInpriority.StartDate == null)
            {
                ModelState.AddModelError("StartDate", "Start Date is empty !!");
                valid = false;
            }
            if (productInpriority.EndDate == null)
            {
                ModelState.AddModelError("EndDate", "End Date is empty !!");
                valid = false;
            }
            if (productInpriority.StartDate >= productInpriority.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date invalid! Date must be after Start Date");
                valid = false;
            }
            return valid;
        }
        #region 1. Common Method: LoadProductInPriority
        //Description: // 
        private void LoadProductInPriorityPage(long productInPriorityId)
        {
            ProductInPriorityRepository _iProductInPriorityService = new ProductInPriorityRepository();
          ViewBag.list_ProductInPriority = _iProductInPriorityService.GetList_ProductInPriorityAll(false).ToList();
        }
        #endregion

        #endregion


        //Category
        #region 1. Common Method: ValidateCategoryForm
        private bool ValidateCategoryForm(string Language, Category CateCollection, HttpPostedFileBase categoryLogoFile, HttpPostedFileBase categoryBannerFile)
        {
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            CategoryRepository catere = new CategoryRepository();
            bool valid = true;
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };

            if (categoryLogoFile != null)
            {
                MediaType mediaType_CategoryLogo = _iMediaTypeService.Get_MediaTypeByCode(CATEGORYLOGO_MEDIATYPECODE);
                if (!AllowedFileExtensions.Contains(categoryLogoFile.FileName.Substring(categoryLogoFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("categoryLogoFileType", "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (categoryLogoFile.ContentLength / 1024 > mediaType_CategoryLogo.MaxSize)
                {
                    ModelState.AddModelError("categoryLogoFileMaxSize", "Your Photo is too large, maximum allowed size is : " + mediaType_CategoryLogo.MaxSize.ToString() + "KB");
                    valid = false;
                }
                if (categoryLogoFile.ContentLength / 1024 < mediaType_CategoryLogo.MinSize)
                {
                    ModelState.AddModelError("categoryLogoFileMinSize", "Your Photo is too small, minimize allowed size is : " + mediaType_CategoryLogo.MinSize.ToString() + "KB");
                    valid = false;
                }
            }

            if (categoryBannerFile != null)
            {
                MediaType mediaType_CategoryBanner = _iMediaTypeService.Get_MediaTypeByCode(CATEGORYBANNER_MEDIATYPECODE);
                if (!AllowedFileExtensions.Contains(categoryBannerFile.FileName.Substring(categoryBannerFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("categoryBannerFileType", "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (categoryBannerFile.ContentLength / 1024 > mediaType_CategoryBanner.MaxSize)
                {
                    ModelState.AddModelError("categoryBannerFileMaxSize", "Your Photo is too large, maximum allowed size is : " + mediaType_CategoryBanner.MaxSize.ToString() + "KB");
                    valid = false;
                }
                if (categoryBannerFile.ContentLength / 1024 < mediaType_CategoryBanner.MinSize)
                {
                    ModelState.AddModelError("categoryBannerFileMinSize", "Your Photo is too small, minimize allowed size is : " + mediaType_CategoryBanner.MinSize.ToString() + "KB");
                    valid = false;
                }
            }

            if (CateCollection.CategoryName == null)
            {
                ModelState.AddModelError("CategoryName", "Category Name is empty !!");
                valid = false;
            }

            if (Language != "[Edit]")
            {
                if (Language == null || Language == "")
                {
                    ModelState.AddModelError("Language", "Language is empty !!");
                    valid = false;
                }
                else
                {
                    if (!(Language == "vi" || Language == "zh" || Language == "en"))
                    {
                        ModelState.AddModelError("Language", "Language code is wrong !!");
                        valid = false;
                    }
                }
            }

            /*  if (CateCollection.Description == null)
              {
                  ModelState.AddModelError("Description", "Description Price is empty !!");
                  valid = false;
              }*/

            if (CateCollection.CategoryId != 0)
            {
                int OrderNumberCount = catere.GetList_CategoryAll().Where(c => c.ParentCateId == CateCollection.ParentCateId).ToList().Count;
                //kiểm tra xem cate này có tồn tại trong cateParent không
                // nếu có tồn tại thì không sao, ngược lại phải thêm một, vì đây là cate mới đối với parent này

                int checkExits = catere.GetList_CategoryAll().Where(c => c.ParentCateId == CateCollection.ParentCateId).ToList().Where(k => k.CategoryId == CateCollection.CategoryId).ToList().Count;
                if (checkExits == 1)//có tồn tại :)
                {
                    if (CateCollection.OrderNumber < 1 || CateCollection.OrderNumber > OrderNumberCount)
                    {
                        ModelState.AddModelError("OrderNumber", "OrderNumber is [1 - " + OrderNumberCount + "] !!");
                        valid = false;
                    }
                }
                else
                {
                    if (CateCollection.OrderNumber < 1 || CateCollection.OrderNumber > OrderNumberCount + 1)
                    {
                        ModelState.AddModelError("OrderNumber", "OrderNumber is [1 - " + (OrderNumberCount + 1) + "] !!");
                        valid = false;
                    }
                }
                // trường hợp logic đặc biệt
                // ví dụ Cate1 có con là cate2
                // giả sử ta thay đổi parent của cate 1 là parent có lvl 2
                // => cate1 sẽ là lvl 3 
                // => cate2 sẽ có lvl 4
                // => vô lý, vì ở đây chỉ chấp nhận có lvl 3 là tối đa

                //kiểm tra có con hay không
                int checkchild = catere.GetList_CategoryAll().Where(c => c.ParentCateId == CateCollection.CategoryId).ToList().Count;
                if (checkchild > 0)
                {
                    if (CateCollection.ParentCateId != 0)
                    {
                        //nếu parent của cate có lvl > 0, tức là 2 trở lên =>sai Level[0,1,2]
                        int plvl = (int)catere.Get_CategoryById((long)CateCollection.ParentCateId).CateLevel;
                        if (plvl > 0)
                        {
                            ModelState.AddModelError("OrderNumber", "This Category have a child, You must be select Parent level 1");
                            valid = false;
                        }
                    }
                }
            }

            return valid;
        }
        #endregion

        #region 2. Common Method: LoadCategoryFormPage
        public void LoadCategoryFromPage(long _cateId)
        {
            //khai báo các class cần dùng
            CategoryRepository catere = new CategoryRepository();
            ProductRepository pr = new ProductRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            Category_MultiLangRepository _iCategory_MultiLangService = new Category_MultiLangRepository();

            _listCategorySortForParentID = new List<Category>();
            GetAllCategory_SortParent(catere.GetList_CategoryAll().ToList().Where(i => i.ParentCateId == 0).ToList());

            IList<Category> lst_Category;
            if (_cateId != 0)
                lst_Category = _listCategorySortForParentID.Where(a => (long)a.CategoryId != _cateId && a.IsActive == true && a.IsDeleted == false && (a.CateLevel == 0 || a.CateLevel == 1)).Where(m => m.CateLevel == (int)catere.Get_CategoryById(_cateId).CateLevel - 1).ToList();
            else
                lst_Category = _listCategorySortForParentID.Where(a => (long)a.CategoryId != _cateId && a.IsActive == true && a.IsDeleted == false && (a.CateLevel == 0 || a.CateLevel == 1)).ToList();

            List<Category> cate_VietNam = new List<Category>();
            foreach (var item in lst_Category)
            {
                Category tmp = new Category();
                tmp.CategoryId = item.CategoryId;
                tmp.CategoryName = item.CategoryName;
                tmp.CateLevel = item.CateLevel;
                tmp.ParentCateId = item.ParentCateId;
                cate_VietNam.Add(tmp);
            }

            //Chỉnh thêm một tí: gạch đầu dòng cho người dùng dễ xem
            foreach (var item2 in cate_VietNam)
            {
                Category_MultiLang cate = _iCategory_MultiLangService.Get_CategoryByIdandLanguageCode(item2.CategoryId, "vi");
                if (cate != null)
                {
                    item2.CategoryName = cate.CategoryName;
                }
                if (item2.CateLevel == 1)
                {
                    item2.CategoryName = "- " + item2.CategoryName;
                }
            }
            ViewBag.cateParentVietNam = cate_VietNam;

            List<Category> cate_English = new List<Category>();
            foreach (var item in lst_Category)
            {
                Category tmp = new Category();
                tmp.CategoryId = item.CategoryId;
                tmp.CategoryName = item.CategoryName;
                tmp.CateLevel = item.CateLevel;
                tmp.ParentCateId = item.ParentCateId;
                cate_English.Add(tmp);
            }
            //Chỉnh thêm một tí: gạch đầu dòng cho người dùng dễ xem
            foreach (var item2 in cate_English)
            {
                Category_MultiLang cate = _iCategory_MultiLangService.Get_CategoryByIdandLanguageCode(item2.CategoryId, "en");
                if (cate != null)
                {
                    item2.CategoryName = cate.CategoryName;
                }
                if (item2.CateLevel == 1)
                {
                    item2.CategoryName = "- " + item2.CategoryName;
                }
            }
            ViewBag.cateParentEnglish = cate_English;

            ViewBag.cateParentTaiwan = lst_Category;
            //Chỉnh thêm một tí: gạch đầu dòng cho người dùng dễ xem
            foreach (var item1 in ViewBag.cateParentTaiwan)
            {
                if (item1.CateLevel == 1)
                {
                    item1.CategoryName = "- " + item1.CategoryName;
                }
            }
            //Show danh sách sản phẩm thuộc category này
            if (_cateId != 0)
            {
                ProductInCategoryRepository picr = new ProductInCategoryRepository();
                //lấy những sản phẩm có trong Product in Category, sau đó lọc ra những Product có categoryid = _cateId
                var p = (from pro in pr.GetList_ProductAll()
                         join cate in picr.GetList_ProductInCategoryAll()
                         on pro.ProductId equals cate.ProductId
                         select new { ProductID = pro.ProductId, CategoryID = cate.CategoryId }).Where(t => t.CategoryID == _cateId).ToList();

                List<Product> lp = new List<Product>();
                foreach (var item in p)
                {
                    lp.Add(pr.GetById(item.ProductID));
                }
                ViewBag.ListProduct = lp;

                //show dropdownlist ordernumber

                List<SelectListItem> templist = new List<SelectListItem>();
                int OrderNumberCount = catere.GetList_CategoryAll().Where(c => c.ParentCateId == catere.Get_CategoryById(_cateId).ParentCateId).ToList().Count;
                for (int i = 1; i <= OrderNumberCount; i++)
                {
                    templist.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.OrderNumberList = templist;

                //load media

                List<Media> lst_mediaCategoryLogo = new List<Media>();
                if (catere.Get_CategoryById(_cateId).LogoMediaId != null)
                {
                    if (_iMediaService.Get_MediaById((long)catere.Get_CategoryById(_cateId).LogoMediaId) != null)
                        lst_mediaCategoryLogo.Add(_iMediaService.Get_MediaById((long)catere.Get_CategoryById(_cateId).LogoMediaId));
                }
                ViewBag.categoryLogoMedia = lst_mediaCategoryLogo;

                List<Media> lst_mediaCategoryBanner = new List<Media>();
                if (catere.Get_CategoryById(_cateId).BannerMediaId != null)
                {
                    if (_iMediaService.Get_MediaById((long)catere.Get_CategoryById(_cateId).BannerMediaId) != null)
                        lst_mediaCategoryBanner.Add(_iMediaService.Get_MediaById((long)catere.Get_CategoryById(_cateId).BannerMediaId));
                }
                ViewBag.categoryBannerMedia = lst_mediaCategoryBanner;

                ViewBag.list_MediaType = _iMediaTypeService.GetList_MediaTypeAll_IsDeleted_IsActive(false, true).ToList();
            }

            ProductTypeRepository _iProductTypeService = new ProductTypeRepository();
            ViewBag.ddl_ProductType = _iProductTypeService.GetList_ProductTypeAll_IsDeleted_IsActive(false, true);

        }
        #endregion

        private string CreateNewName(string extension, string typeMedia)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + typeMedia + "_" + Guid.NewGuid().ToString() + extension;
        }

        [HttpPost]
        public ActionResult GetProductPage(string keyword)
        {
            try
            {
                ISearchRepository _searchRep = new SearchRepository();
                var raw = "[";
                var rs = _searchRep.SearchProductSimple(keyword, -1).Distinct().ToList();
                foreach (var item in rs)
                {

                    if (item.IsDeleted == false && item.IsVerified == true && item.Store.IsDeleted == false && item.Store.IsVerified == true && item.Store.ShowInMallPage == true)
                    {
                        raw += "{\"ProductId\":\"" + item.ProductId + "\",\"ProductStockId\":\"" + item.ProductStockCode + "\",\"Price\":\"" + (item.PromotePrice != null ? item.PromotePrice.ToString() : item.MobileOnlinePrice.ToString()) + "\",\"OnlinePrice\":\"" + item.MobileOnlinePrice.ToString() + "\",\"ProductName\":\"" + ReplaceAll(item.ProductName) + "\"},";
                    }
                }
                if (raw == "[")
                    raw = "[]";
                else
                    raw = raw.Substring(0, raw.Length - 1) + "]";
                return Json(raw);
            }
            catch
            {
                return Json("[]");
            }
        }
        public string ReplaceAll(string input)
        {
            var s = input.IndexOf("'");
            if (input.IndexOf("'") >= 0 || input.IndexOf('"') >= 0)
            {
                input = input.Replace("'", HttpUtility.HtmlEncode("'"));
                input = input.Replace('"', ' ');
                return ReplaceAll(input);
            }
            else
                return input;
        }

        public void GetAllCategory_SortParent(List<Category> _Parent)
        {
            CategoryRepository cr = new CategoryRepository();
            List<Category> _Parent2 = _Parent.OrderBy(x => x.OrderNumber).ToList();
            foreach (var item in _Parent2)
            {
                _listCategorySortForParentID.Add(item);
                if (cr.GetList_CategoryAll().Where(f => f.ParentCateId == item.CategoryId).ToList().Count > 0)
                {
                    GetAllCategory_SortParent(cr.GetList_CategoryAll().Where(f => f.ParentCateId == item.CategoryId).ToList());
                }
            }
        }
        #endregion

        #region WebMethod API
        #region Chart

        [WebMethod, HttpPost]
        public string StatisticStoreViewCount(int? viewcount, string fromdate, string todate)
        {
            int view_count = viewcount ?? 10;
            DateTime myfromDate = fromdate != "undefined" && fromdate != null ? DateTime.ParseExact(fromdate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : new DateTime(2006, 1, 1);
            DateTime mytoDate = todate != "undefined" && todate != null ? DateTime.ParseExact(todate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            //lấy dữ liệu View Count
            StoreRepository _iStoreService = new StoreRepository();
            List<Store> list_store = _iStoreService.GetList_StoreAll().Where(k => k.OnlineDate != null & k.IsDeleted == false & k.IsActive == true).Where(f => f.OnlineDate.Value.Ticks > myfromDate.Ticks & f.OnlineDate.Value.Ticks < mytoDate.Ticks).OrderByDescending(s => s.VisitCount).ToList();
            //Tạo Json
            string label = "Visit Count";
            string color = "#00A1F1";
            int col_count = list_store.ToList().Count > view_count ? view_count : list_store.ToList().Count;

            string strJson = "";
            strJson = "[{";
            strJson += "\"label\" : \"" + label + "\",";
            strJson += "\"color\" : \"" + color + "\",";
            strJson += "\"data\": ";
            strJson += "[";
            for (int i = 0; i < col_count; i++)
            {
                strJson += "[\"" + list_store[i].StoreName.ToString() + "" + "\", " + list_store[i].VisitCount.ToString() + "]";
                if (i < col_count - 1)
                {
                    strJson += ",";
                }
            }
            strJson += "]";
            strJson += "}]";
            return strJson;
        }

        [WebMethod, HttpPost]
        public string StatisticProductViewCount(int? viewcount, string fromdate, string todate)
        {
            int view_count = viewcount ?? 10;
            DateTime myfromDate = fromdate != "undefined" && fromdate != null ? DateTime.ParseExact(fromdate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : new DateTime(2006, 1, 1);
            DateTime mytoDate = todate != "undefined" && todate != null ? DateTime.ParseExact(todate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            //lấy dữ liệu View Count
            ProductRepository _iStoreService = new ProductRepository();
            List<Product> list_store = _iStoreService.GetList_ProductAll().Where(k => k.DateCreated != null & k.IsDeleted == false & k.IsActive == true).Where(f => f.DateCreated.Value.Ticks > myfromDate.Ticks & f.DateCreated.Value.Ticks < mytoDate.Ticks).OrderByDescending(s => s.VisitCount).ToList();
            //Tạo Json
            string label = "Visit Count";
            string color = "#00A1A1";
            int col_count = list_store.ToList().Count > view_count ? view_count : list_store.ToList().Count;

            string strJson = "";
            strJson = "[{";
            strJson += "\"label\" : \"" + label + "\",";
            strJson += "\"color\" : \"" + color + "\",";
            strJson += "\"data\": ";
            strJson += "[";
            for (int i = 0; i < col_count; i++)
            {
                strJson += "[\"" + list_store[i].ProductName.ToString() + "" + "\", " + list_store[i].VisitCount.ToString() + "]";
                if (i < col_count - 1)
                {
                    strJson += ",";
                }
            }
            strJson += "]";
            strJson += "}]";
            return strJson;
        }

        [WebMethod, HttpPost]
        public string StatisticBrandViewCount(int? viewcount, string fromdate, string todate)
        {
            int view_count = viewcount ?? 10;
            DateTime myfromDate = fromdate != "undefined" && fromdate != null ? DateTime.ParseExact(fromdate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : new DateTime(2006, 1, 1);
            DateTime mytoDate = todate != "undefined" && todate != null ? DateTime.ParseExact(todate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            //lấy dữ liệu View Count
            BrandRepository _iStoreService = new BrandRepository();
            List<Brand> list_store = _iStoreService.GetList_BrandAll().Where(k => k.DateCreated != null & k.IsDeleted == false & k.IsActive == true).Where(f => f.DateCreated.Value.Ticks > myfromDate.Ticks & f.DateCreated.Value.Ticks < mytoDate.Ticks).OrderByDescending(s => s.VisitCount).ToList();
            //Tạo Json
            string label = "Visit Count";
            string color = "#00B1B1";
            int col_count = list_store.ToList().Count > view_count ? view_count : list_store.ToList().Count;

            string strJson = "";
            strJson = "[{";
            strJson += "\"label\" : \"" + label + "\",";
            strJson += "\"color\" : \"" + color + "\",";
            strJson += "\"data\": ";
            strJson += "[";
            for (int i = 0; i < col_count; i++)
            {
                strJson += "[\"" + list_store[i].BrandName.ToString() + "" + "\", " + list_store[i].VisitCount.ToString() + "]";
                if (i < col_count - 1)
                {
                    strJson += ",";
                }
            }
            strJson += "]";
            strJson += "}]";
            return strJson;
        }

        [WebMethod, HttpPost]
        public string StatisticCategoryViewCount(int? viewcount, string fromdate, string todate)
        {
            int view_count = viewcount ?? 10;
            DateTime myfromDate = fromdate != "undefined" && fromdate != null ? DateTime.ParseExact(fromdate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : new DateTime(2006, 1, 1);
            DateTime mytoDate = todate != "undefined" && todate != null ? DateTime.ParseExact(todate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Now;
            //lấy dữ liệu View Count
            CategoryRepository _iStoreService = new CategoryRepository();
            List<Category> list_store = _iStoreService.GetList_CategoryAll().Where(k => k.DateCreated != null & k.IsDeleted == false & k.IsActive == true).Where(f => f.DateCreated.Value.Ticks > myfromDate.Ticks & f.DateCreated.Value.Ticks < mytoDate.Ticks).OrderByDescending(s => s.VisitCount).ToList();
            //Tạo Json
            string label = "Visit Count";
            string color = "#00C1C1";
            int col_count = list_store.ToList().Count > view_count ? view_count : list_store.ToList().Count;

            string strJson = "";
            strJson = "[{";
            strJson += "\"label\" : \"" + label + "\",";
            strJson += "\"color\" : \"" + color + "\",";
            strJson += "\"data\": ";
            strJson += "[";
            for (int i = 0; i < col_count; i++)
            {
                strJson += "[\"" + list_store[i].CategoryName.ToString() + "" + "\", " + list_store[i].VisitCount.ToString() + "]";
                if (i < col_count - 1)
                {
                    strJson += ",";
                }
            }
            strJson += "]";
            strJson += "}]";
            return strJson;
        }
        #endregion
        #endregion

        public string CastURLYoutubeToIFrame(string url)
        {
            //string pattern = @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})";
            //string id = Regex.Match(URLYoutube, pattern).Groups[1].Value;
            //return id;
            var query_string = "";
            var query = url;
            var vars = query.Split('&');
            for (var i = 0; i < vars.Length; i++)
            {
                var pair = vars[i].Split('=');
                // If first entry with this name
                query_string = pair[1];
            }
            return query_string;
        }


    }
}
