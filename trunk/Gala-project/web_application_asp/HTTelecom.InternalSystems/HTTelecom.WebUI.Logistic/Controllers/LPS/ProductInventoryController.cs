using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.Repository.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.Logistic.Filters;


namespace HTTelecom.WebUI.Logistic.Controllers.LPS
{
    [SessionLoginFilter]
    public class ProductInventoryController : Controller
    {
        //
        // GET: /ProductInventory/
        private int pageSizeDefault = 50;
        public ActionResult Index(int? page)
        {
            ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
            IList<ProductInventory> lst_ProductInventory = _iProductInventoryService.GetList_ProductInventoryAll().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;

            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////


            ViewBag.SideBarMenu = "ProductInventoryIndex";
            return View(lst_ProductInventory.ToPagedList(pageNum, pageSizeDefault));
        }
        public ActionResult Create()
        {
            return RedirectToAction("Index");
            LoadProductInventoryFormPage(0);
            return View(new ProductInventory());
        }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult Create(ProductInventory ProductInventoryCollection)
        //{
        //    try
        //    {
        //        ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
        //        Account accOnline = (Account)Session["Account"];
        //        ProductInventoryCollection.CreatedBy = accOnline.AccountId;
        //        if (this.ValidateProductInventoryForm(ProductInventoryCollection) == true)
        //        {
        //            long kq = _iProductInventoryService.InsertProductInventory(ProductInventoryCollection);

        //            if (kq != -1) //Insert success
        //            {
        //                TempData["ResponseMessage"] = 1; //1: Success
        //                return RedirectToAction("Index", "ProductInventory");
        //            }
        //            ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
        //    }

        //    ViewBag.SideBarMenu = "ProductInventory";
        //    LoadProductInventoryFormPage(0);
        //    return View(ProductInventoryCollection);
        //}

        public ActionResult Edit(long? id)
        {
            return RedirectToAction("Index");
            if (id == null)
                return RedirectToAction("Index");
            ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();

            ProductInventory ProductInventory = _iProductInventoryService.Get_ProductInventoryById((long)id);
            LoadProductInventoryFormPage((long)id);
            return View(ProductInventory);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ProductInventory ProductInventoryCollection)
        {
            try
            {
                ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
                if (this.ValidateProductInventoryForm(ProductInventoryCollection) == true)
                {
                    bool kq = _iProductInventoryService.UpdateProductInventory(ProductInventoryCollection);

                    if (kq == true) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "ProductInventory");
                    }
                    ModelState.AddModelError("UpdateStatus", "Error: There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
            }

            //Trường hợp cuối cùng!. lỗi
            ViewBag.SideBarMenu = "ProductInventory";
            LoadProductInventoryFormPage(ProductInventoryCollection.ProductInventoryId);
            return View(ProductInventoryCollection);
        }

        #region Common Method
        private bool ValidateProductInventoryForm(ProductInventory ProductInventoryCollection)
        {
            bool valid = true;

            //if (ProductInventoryCollection.ProductName == null)
            //{
            //    ModelState.AddModelError("ProductInventoryName", "ProductInventoryName is empty !!");
            //    valid = false;
            //}
            //if (ProductInventoryCollection.Quantity == null)
            //{
            //    ModelState.AddModelError("ProductInventoryName", "Quantity is empty !!");
            //    valid = false;
            //}
            //if (ProductInventoryCollection.LimitWarrantyDays == null)
            //{
            //    ModelState.AddModelError("LimitWarrantyDays", "Limit Warranty Days is empty !!");
            //    valid = false;
            //}
            //if (ProductInventoryCollection.ProductCode == null)
            //{
            //    ModelState.AddModelError("ProductCode", "Product Code is empty !!");
            //    valid = false;
            //}
            //if (ProductInventoryCollection.ProductBarCode == null)
            //{
            //    ModelState.AddModelError("ProductBarCode", "ProductBar Code is empty !!");
            //    valid = false;
            //}

            return valid;
        }
        private void LoadProductInventoryFormPage(long _ProductInventoryId)
        {
            try
            {
                ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
                ProductInventory pi = new ProductInventory();
                if (_ProductInventoryId != 0)
                {
                    pi = _iProductInventoryService.Get_ProductInventoryById(_ProductInventoryId);
                }

                BrandRepository _iBrandService = new BrandRepository();
                VendorRepository _iVendorService = new VendorRepository();
                ViewBag.Vendor = _iVendorService.GetList_VendorAll().Where(v => v.IsActive == true && v.IsDeleted == false).ToList();
                ViewBag.Brand = _iBrandService.GetList_BrandAll().Where(v => v.IsActive == true && v.IsDeleted == false).ToList();
            }
            catch
            {
            }
        }
        #endregion
    }
}
