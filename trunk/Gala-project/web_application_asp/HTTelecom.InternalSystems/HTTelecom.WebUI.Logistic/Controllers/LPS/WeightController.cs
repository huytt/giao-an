using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.Repository.lps;
using HTTelecom.WebUI.Logistic.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTelecom.Domain.Core.Repository.cis;
using PagedList;
using System.Web.Services;

namespace HTTelecom.WebUI.Logistic.Controllers
{
      [SessionLoginFilter]
    public class WeightController : Controller
    {
        // GET: /Weight/
        private int pageSizeDefault = 50;
        public ActionResult Index(int? page)
        {
            WeightRepository _iWeightService = new WeightRepository();
            ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
            DistrictRepository _iDistrictService = new DistrictRepository();
            ProvinceRepository _iProvinceService = new ProvinceRepository();
            IList<Weight> lst_Weight = _iWeightService.GetList_WeightAll().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;

            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////

            ViewBag.SideBarMenu = "WeightIndex";
            IPagedList<Weight> listWeightShow = lst_Weight.ToPagedList(pageNum, pageSizeDefault);
            List<Province> lst_tmp = _iProvinceService.GetAll().OrderBy(q => q.ProvinceName).ToList();
            lst_tmp.ForEach(s => s.ProvinceName = s.Type + " " + s.ProvinceName);
            ViewBag.Province = lst_tmp;
            List<District> lst_tmp2 = _iDistrictService.GetAll().OrderBy(q => q.DistrictName).ToList();
            lst_tmp2.ForEach(s => s.DistrictName = s.Type + " " + s.DistrictName);
            ViewBag.District = lst_tmp2;
            return View(listWeightShow);
        }
        public ActionResult Create()
        {
            LoadWeightFormPage(0,null,null);
            return View(new Weight());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Weight WeightCollection, long? District, long? Province)
        {
            try
            {
                WeightRepository _iWeightService = new WeightRepository();
                if (WeightCollection.Type == "1")
                {
                    WeightCollection.TargetId = District ?? 0;
                }
                else
                {
                    WeightCollection.TargetId = Province ?? 0;
                }

                if (this.ValidateWeightForm(WeightCollection,District,Province) == true)
                {
                    long kq = _iWeightService.InsertWeight(WeightCollection);

                    if (kq != -1) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "Weight");
                    }
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
            }

            ViewBag.SideBarMenu = "Weight";
            LoadWeightFormPage(0, District, Province);
            return View(WeightCollection);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            WeightRepository _iWeightService = new WeightRepository();

            Weight Weight = _iWeightService.Get_WeightById((long)id);
            LoadWeightFormPage((long)id,null,null);
            ViewBag.SideBarMenu = "WeightIndex";
            return View(Weight);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Weight WeightCollection, long? District, long? Province)
        {
            try
            {
                if (WeightCollection.Type == "1")
                {
                    WeightCollection.TargetId = District ?? 0;
                }
                else
                {
                    WeightCollection.TargetId = Province ?? 0;
                }

                WeightRepository _iWeightService = new WeightRepository();
                if (this.ValidateWeightForm(WeightCollection, District,Province) == true)
                {
                    bool kq = _iWeightService.UpdateWeight(WeightCollection);

                    if (kq == true) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "Weight");
                    }
                    ModelState.AddModelError("UpdateStatus", "Error: There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
            }

            //Trường hợp cuối cùng!. lỗi
            ViewBag.SideBarMenu = "Weight";
            LoadWeightFormPage(WeightCollection.WeightId,null,null);
            return View(WeightCollection);
        }

        #region Common Method
        private bool ValidateWeightForm(Weight WeightCollection, long? District, long? Province)
        {
            WeightRepository _iWeightService = new WeightRepository();
            bool valid = true;

            if (WeightCollection.Price == null)
            {
                ModelState.AddModelError("Price", "Price is empty !!");
                valid = false;
            }
            if (WeightCollection.WeightFrom == null) 
            {
                ModelState.AddModelError("WeightFrom", "Weight From is empty !!");
                valid = false;
            }
            if (WeightCollection.WeightTo == null)
            {
                ModelState.AddModelError("WeightTo", "Weight To is empty !!");
                valid = false;
            }
            if (WeightCollection.Type == null)
            {
                ModelState.AddModelError("Type", "Type is empty !!");
                valid = false;
            }
            if (WeightCollection.Type == "1")
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
            if (WeightCollection.Type == "2")
            {
                if (Province == null || Province == 0)
                {
                    ModelState.AddModelError("Province", "Province is empty !!");
                    valid = false;
                }
            }
            //long tmp_id = _iWeightService.CheckExists(WeightCollection.Type, WeightCollection.TargetId); ----Hung
            //if (tmp_id != -1 && tmp_id != WeightCollection.WeightId)
            //{
            //    ModelState.AddModelError("Exists", "Area that you want to add already exists !!");
            //    valid = false;
            //}
          
            return valid;
        }
        private void LoadWeightFormPage(long _WeightId, long? District, long? Province)
        {
            try
            {
                WeightRepository _iWeightService = new WeightRepository();
                DistrictRepository _iDistrictService = new DistrictRepository();
                ProvinceRepository _iProvinceService = new ProvinceRepository();

                Weight pi = new Weight();
                List<Province> lst_tmp = _iProvinceService.GetAll().OrderBy(q=>q.ProvinceName).ToList();
                lst_tmp.ForEach(s => s.ProvinceName = s.Type + " " + s.ProvinceName);
                ViewBag.Province = lst_tmp;
                ViewBag.District = _iDistrictService.GetAll().OrderBy(q => q.DistrictName).ToList();
                List<District> lst_tmp2 = _iDistrictService.GetAll().OrderBy(q => q.DistrictName).ToList();
                lst_tmp2.ForEach(s => s.DistrictName = s.Type + " " + s.DistrictName);
                ViewBag.DistrictLoad = new List<District>();
                if (_WeightId != 0)
                {
                    pi = _iWeightService.Get_WeightById(_WeightId);
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

                VendorRepository _iVendorService = new VendorRepository();
                ViewBag.Vendor = _iVendorService.GetList_VendorAll().Where(v => v.IsActive == true && v.IsDeleted == false).ToList();
                List<SelectListItem> lstselectType = new List<SelectListItem>();
                lstselectType.Add(new SelectListItem() { Text = "District", Value = "1" });
                lstselectType.Add(new SelectListItem() { Text = "Province", Value = "2" });
                ViewBag.Type = lstselectType;
            }
            catch
            {
            }
        }

        [WebMethod, HttpPost]
        public JsonResult ChangePrice(long WeightId, string Price)
        {
            List<string> error = new List<string>();
            try
            {
                WeightRepository _iWeightService = new WeightRepository();
                decimal tmp = 0;
                if (decimal.TryParse(Price, out tmp))
                {
                    decimal result = _iWeightService.ChangePrice(WeightId, tmp);
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
        #endregion
    }
}
