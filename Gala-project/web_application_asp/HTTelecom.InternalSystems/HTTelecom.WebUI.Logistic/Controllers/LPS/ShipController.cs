using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.lps;
using HTTelecom.WebUI.Logistic.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace HTTelecom.WebUI.Logistic.Controllers.LPS
{
    [SessionLoginFilter]
    public class ShipController : Controller
    {
        // GET: /Ship/
        private int pageSizeDefault = 100;
        public ActionResult Index(int? page)
        {
            ShipRepository _iShipService = new ShipRepository();
            ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
            DistrictRepository _iDistrictService = new DistrictRepository();
            ProvinceRepository _iProvinceService = new ProvinceRepository();
            IList<Ship> lst_Ship = _iShipService.GetList_ShipAll().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;

            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////

            ViewBag.SideBarMenu = "ShipIndex";
            IPagedList<Ship> listShipShow = lst_Ship.ToPagedList(pageNum, pageSizeDefault);
            List<Province> lst_tmp = _iProvinceService.GetAll().OrderBy(q => q.ProvinceName).ToList();
            lst_tmp.ForEach(s => s.ProvinceName = s.Type + " " + s.ProvinceName);
            ViewBag.Province = lst_tmp;
            List<District> lst_tmp2 = _iDistrictService.GetAll().OrderBy(q => q.DistrictName).ToList();
            lst_tmp2.ForEach(s => s.DistrictName = s.Type + " " + s.DistrictName);
            ViewBag.District = lst_tmp2;
            return View(listShipShow);
        }
        public ActionResult Create()
        {
            LoadShipFormPage(0, null, null);
            return View(new Ship());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Ship ShipCollection, long? District, long? Province)
        {
            try
            {
                ShipRepository _iShipService = new ShipRepository();
                if (ShipCollection.Type == "1")
                {
                    ShipCollection.TargetId = District ?? 0;
                }
                else
                {
                    ShipCollection.TargetId = Province ?? 0;
                }

                if (this.ValidateShipForm(ShipCollection, District, Province) == true)
                {
                    long kq = _iShipService.InsertShip(ShipCollection);

                    if (kq != -1) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "Ship");
                    }
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
            }

            ViewBag.SideBarMenu = "Ship";
            LoadShipFormPage(0, District, Province);
            return View(ShipCollection);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            ShipRepository _iShipService = new ShipRepository();

            Ship Ship = _iShipService.Get_ShipById((long)id);
            LoadShipFormPage((long)id, null, null);
            ViewBag.SideBarMenu = "ShipIndex";
            return View(Ship);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Ship ShipCollection, long? District, long? Province)
        {
            try
            {
                if (ShipCollection.Type == "1")
                {
                    ShipCollection.TargetId = District ?? 0;
                }
                else
                {
                    ShipCollection.TargetId = Province ?? 0;
                }

                ShipRepository _iShipService = new ShipRepository();
                if (this.ValidateShipForm(ShipCollection, District, Province) == true)
                {
                    bool kq = _iShipService.UpdateShip(ShipCollection);

                    if (kq == true) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "Ship");
                    }
                    ModelState.AddModelError("UpdateStatus", "Error: There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
            }

            //Trường hợp cuối cùng!. lỗi
            ViewBag.SideBarMenu = "Ship";
            LoadShipFormPage(ShipCollection.ShipId, null, null);
            return View(ShipCollection);
        }

        #region Common Method
        private bool ValidateShipForm(Ship ShipCollection, long? District, long? Province)
        {
            ShipRepository _iShipService = new ShipRepository();
            bool valid = true;

            if (ShipCollection.Price == null)
            {
                ModelState.AddModelError("Price", "Price is empty !!");
                valid = false;
            }
           
            if (ShipCollection.Type == null)
            {
                ModelState.AddModelError("Type", "Type is empty !!");
                valid = false;
            }
            if (ShipCollection.Type == "1")
            {
                if (District == null || District == 0)
                {
                    ModelState.AddModelError("District", "District is empty !!");
                    valid = false;
                }
                if (Province == null || Province == 0)
                {
                    ModelState.AddModelError("Province", "Province is empty !!");
                    valid = false;
                }
            }
            if (ShipCollection.Type == "2")
            {
                if (Province == null || Province == 0)
                {
                    ModelState.AddModelError("Province", "Province is empty !!");
                    valid = false;
                }
            }
            //long tmp_id = _iShipService.CheckExists(ShipCollection.Type, ShipCollection.TargetId); ----Hung
            //if (tmp_id != -1 && tmp_id != ShipCollection.ShipId)
            //{
            //    ModelState.AddModelError("Exists", "Area that you want to add already exists !!");
            //    valid = false;
            //}

            return valid;
        }
        private void LoadShipFormPage(long _ShipId, long? District, long? Province)
        {
            try
            {
                ShipRepository _iShipService = new ShipRepository();
                DistrictRepository _iDistrictService = new DistrictRepository();
                ProvinceRepository _iProvinceService = new ProvinceRepository();

                Ship pi = new Ship();
                List<Province> lst_tmp = _iProvinceService.GetAll().OrderBy(q => q.ProvinceName).ToList();
                lst_tmp.ForEach(s => s.ProvinceName = s.Type + " " + s.ProvinceName);
                ViewBag.Province = lst_tmp;
                ViewBag.District = _iDistrictService.GetAll().OrderBy(q => q.DistrictName).ToList();
                List<District> lst_tmp2 = _iDistrictService.GetAll().OrderBy(q => q.DistrictName).ToList();
                lst_tmp2.ForEach(s => s.DistrictName = s.Type + " " + s.DistrictName);
                ViewBag.DistrictLoad = new List<District>();
                if (_ShipId != 0)
                {
                    pi = _iShipService.Get_ShipById(_ShipId);
                    if (pi.Type == "1")
                    {
                        ViewBag.DistrictLoad = lst_tmp2.Where(x => x.ProvinceId == _iDistrictService.GetById((long)pi.TargetId).ProvinceId).ToList();
                    }
                }
                if (Province != null)
                {
                    if (pi.Type == "1")
                    {
                        ViewBag.DistrictLoad = lst_tmp2.Where(x => x.ProvinceId == _iDistrictService.GetById((long)pi.TargetId).ProvinceId).ToList();
                    }
                }

                //VendorRepository _iVendorService = new VendorRepository();
                //ViewBag.Vendor = _iVendorService.GetList_VendorAll().Where(v => v.IsActive == true && v.IsDeleted == false).ToList();
                List<SelectListItem> lstselectType = new List<SelectListItem>();
                lstselectType.Add(new SelectListItem() { Text = "District", Value = "1" });
                lstselectType.Add(new SelectListItem() { Text = "Province", Value = "2" });
                ViewBag.Type = lstselectType;
            }
            catch
            {
            }
        }
        #endregion

        [WebMethod, HttpPost]
        public JsonResult ChangePrice(long ShipId, string Price)
        {
            List<string> error = new List<string>();
            try
            {
                ShipRepository _iShipService = new ShipRepository();
                  decimal tmp = 0;
                  if (decimal.TryParse(Price, out tmp))
                  {
                      decimal result = _iShipService.ChangePrice(ShipId, tmp);
                      if (result != -1)
                      {
                          return Json(new { success = true, price = string.Format("{0 : 0,0 vnđ}", result) }, JsonRequestBehavior.AllowGet);
                      }
                  }
                error.Add("Error: " + " Update Fails.");
            }
            catch (Exception ex)
            {
                error.Add("Error: " + ex.Message);
            }
            return Json(new { success = false, error = error }, JsonRequestBehavior.AllowGet);
        }

        [WebMethod, HttpPost]
        public JsonResult ChangeFreeShip(long ShipId, string FreeShip)
        {
            List<string> error = new List<string>();
            try
            {
                ShipRepository _iShipService = new ShipRepository();
                decimal tmp = 0;
                if (decimal.TryParse(FreeShip, out tmp))
                {
                    decimal result = _iShipService.ChangeFreeShip(ShipId, tmp);
                    if (result != -1)
                    {
                        return Json(new { success = true, freeship = string.Format("{0 : 0,0 vnđ}", result) }, JsonRequestBehavior.AllowGet);
                    }                
                }               
                error.Add("Error: " + " Update Fails.");
            }
            catch (Exception ex)
            {
                error.Add("Error: " + ex.Message);
            }
            return Json(new { success = false, error = error }, JsonRequestBehavior.AllowGet);
        }
    }
}
