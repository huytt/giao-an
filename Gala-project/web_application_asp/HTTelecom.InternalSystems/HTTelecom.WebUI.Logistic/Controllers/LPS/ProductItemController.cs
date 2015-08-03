using HTTelecom.Domain.Core.DataContext.lps;
using HTTelecom.Domain.Core.Repository.lps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.ams;
using System.Web.Services;
using HTTelecom.Domain.Core.IRepository.mss;
using HTTelecom.WebUI.Logistic.Filters;

namespace HTTelecom.WebUI.Logistic.Controllers
{
     [SessionLoginFilter]
    public class ProductItemController : Controller
    {
        //
        // GET: /ProductItem/
        private int pageSizeDefault = 50;
        private string strSizeOneSize = "OneSize";
        public ActionResult Index(int?page)
        {
            ProductItemRepository _iProductItemService = new ProductItemRepository();
            ProductInventoryRepository _iProductInventoryService = new ProductInventoryRepository();
            ProductItemInSizeRepository _iProductItemInSize = new ProductItemInSizeRepository();
            SizeRepository _iSizeService = new SizeRepository();
            VendorRepository _iVendorService = new VendorRepository();
            IList<ProductItem> lst_ProductItem = _iProductItemService.GetList_ProductItemAll().ToList();

            int pageNum = (page ?? 1);
            ViewBag.page = page;

            ////////////////////////////////////////////////////////////////////////////////////
            if (TempData["ResponseMessage"] != null)
                ViewBag.ResponseMessage = TempData["ResponseMessage"];
            //////////////////////////////////////////////////////////////////////////////////

            ViewBag.Vendor = _iVendorService.GetList_VendorAll().Where(p => p.IsActive == true && p.IsDeleted == false).ToList();
            ViewBag.SideBarMenu = "ProductItemIndex";
            IPagedList<ProductItem> listProductItemShow = lst_ProductItem.OrderByDescending(a => a.ProductItemId).ToPagedList(pageNum, pageSizeDefault);
            List<ProductInventory> lst_productInven = new List<ProductInventory>();
            foreach (ProductItem item in listProductItemShow)
            {
                ProductInventory piv = _iProductInventoryService.Get_ProductInventoryById_ProductItemId(item.ProductItemId);
                if(piv !=null)
                    lst_productInven.Add(piv);
            }
            ViewBag.ProductItemInSize = _iProductItemInSize.GetList_ProductItemInSizeAll()?? null;
            ViewBag.Size = _iSizeService.GetList_SizeAll().OrderBy(x => x.SizeId).ToList();
            ViewBag.ProductInventory = lst_productInven;
            return View(listProductItemShow);
        }
        public ActionResult Create()
        {
            LoadProductItemFormPage(0);
            return View(new ProductItem());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ProductItem ProductItemCollection)
        {
            try
            {
                ProductItemRepository _iProductItemService = new ProductItemRepository();
                Account accOnline = (Account)Session["Account"];
                ProductItemCollection.CreatedBy = accOnline.AccountId;
                if (this.ValidateProductItemForm(ProductItemCollection) == true)
                {
                    
                    long kq = _iProductItemService.InsertProductItem(ProductItemCollection);

                    if (kq != -1) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "ProductItem");
                    }
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus","Error: "+ ex.Message);
            }           

            ViewBag.SideBarMenu = "ProductItem";
            LoadProductItemFormPage(0);
            return View(ProductItemCollection);
        }

        public ActionResult Edit(long ?id)
        {
            if (id == null)
                return RedirectToAction("Index");
            ProductItemRepository _iProductItemService = new ProductItemRepository();

            ProductItem ProductItem = _iProductItemService.Get_ProductItemById((long)id);
            LoadProductItemFormPage((long)id);
            ViewBag.SideBarMenu = "ProductItemIndex";
            return View(ProductItem);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ProductItem ProductItemCollection)
        {
            try
            {
                ProductItemRepository _iProductItemService = new ProductItemRepository();
                Account accOnline = (Account)Session["Account"];
                ProductItemCollection.ModifiedBy = accOnline.AccountId;
                if (this.ValidateProductItemForm(ProductItemCollection) == true)
                {
                    bool kq = _iProductItemService.UpdateProductItem(ProductItemCollection);

                    if (kq == true) //Insert success
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("Index", "ProductItem");
                    }
                    ModelState.AddModelError("UpdateStatus", "Error: There was an error occurs !!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateStatus", "Error: " + ex.Message);
            }

            //Trường hợp cuối cùng!. lỗi
            ViewBag.SideBarMenu = "ProductItem";
            LoadProductItemFormPage(ProductItemCollection.ProductItemId);
            return View(ProductItemCollection);        
        }

        #region Common Method
        private bool ValidateProductItemForm(ProductItem ProductItemCollection)
        {
            bool valid = true;
          
            if (ProductItemCollection.ProductName == null)
            {
                ModelState.AddModelError("ProductItemName", "ProductItemName is empty !!");
                valid = false;
            }
            //if (ProductItemCollection.Quantity == null)
            //{
            //    ModelState.AddModelError("ProductItemName", "Quantity is empty !!");
            //    valid = false;
            //}
            //if (ProductItemCollection.LimitWarrantyDays == null)
            //{
            //    ModelState.AddModelError("LimitWarrantyDays", "Limit Warranty Days is empty !!");
            //    valid = false;
            //}
            if (ProductItemCollection.ProductCode == null)
            {
                ModelState.AddModelError("ProductCode", "Product Code is empty !!");
                valid = false;
            }
            else 
            {
                if (ProductItemCollection.ProductItemId == 0)
                {
                    ProductRepository _iProductService = new ProductRepository();
                    var tmp_p = _iProductService.Get_ProductByStockCode(ProductItemCollection.ProductCode);
                    if (tmp_p == null)
                    {
                        ModelState.AddModelError("ProductCode", "This Product does not exists !!");
                        valid = false;
                    }
                    else
                    {
                        ProductItemRepository _iProductItemService = new ProductItemRepository();
                        var tmp_pi = _iProductItemService.GetList_ProductItemAll().Where(x => x.ProductCode == ProductItemCollection.ProductCode).ToList();
                        if (tmp_pi.Count >= 1)
                        {
                            ModelState.AddModelError("ProductCode", "This ProductItem is exits!!");
                            valid = false;
                        }
                    }
                }
            }
            //if (ProductItemCollection.ProductBarCode == null)
            //{
            //    ModelState.AddModelError("ProductBarCode", "ProductBar Code is empty !!");
            //    valid = false;
            //}
            if (ProductItemCollection.VendorId == null)
            {
                ModelState.AddModelError("VendorId", "Vendor is empty !!");
                valid = false;
            }
            //if (ProductItemCollection.ProductStatusCode == null)
            //{
            //    ModelState.AddModelError("ProductStatusCode", "Product Status is empty !!");
            //    valid = false;
            //}    

            return valid;
        }
        private void LoadProductItemFormPage(long _ProductItemId)
        {
            try
            {
                ProductItemRepository _iProductItemService = new ProductItemRepository();
                AccountRepository _iAccountService = new AccountRepository();
                ProductItemInSizeRepository _iProductItemInSize = new ProductItemInSizeRepository();
                ProductItem pi = new ProductItem();
                if (_ProductItemId != 0)
                {
                    pi = _iProductItemService.Get_ProductItemById(_ProductItemId);
                }
                VendorRepository _iVendorService = new VendorRepository();
                ProductStatusRepository _iProductStatusService = new ProductStatusRepository();
                SizeRepository _iSizeService = new SizeRepository();
                SizeGlobalRepository _iGlobalSizeService = new SizeGlobalRepository();

                ViewBag.Vendor = _iVendorService.GetList_VendorAll().Where(v=>v.IsActive == true && v.IsDeleted == false).ToList();

                ViewBag.Size = _iSizeService.GetList_SizeAll().OrderBy(x => x.SizeId).ToList();
                ViewBag.ProductStatus = _iProductStatusService.GetList_ProductStatusAll().Where(v => v.IsActive == true && v.IsDeleted == false).ToList();
                ViewBag.ProductItemInSize = _iProductItemInSize.GetList_ProductItemInSizeAll().Where(x=>x.ProductItemId == _ProductItemId).ToList() ?? null;
                ViewBag.GlobalSize = _iGlobalSizeService.GetList_SizeGlobalAll_IsDeleted(false);
             
                ViewBag.CreatedBy = _iAccountService.Get_AccountById(pi.CreatedBy??0)??new Account();
                ViewBag.ModifiedBy = _iAccountService.Get_AccountById(pi.ModifiedBy??0)??new Account();

            }
            catch 
            { 
            }           
        }

        public ActionResult AddNewSizeForProductItem(long? ProductItemId, long? SizeId)
        {          
            try
            {
                if (ProductItemId == null || SizeId == null)
                {
                    TempData["ErrorMessage"] = "\"Product Item\" or \"Size\" do does not exactly.";
                    return RedirectToAction("Edit", "ProductItem", new { id = ProductItemId, tabActive = "tabtwo" });
                }
                ProductItemRepository _iProductItemService = new ProductItemRepository();
                SizeRepository _iSizeService = new SizeRepository();
                ProductItemInSizeRepository _iProductItemInSizeService = new ProductItemInSizeRepository();
                //long Size_OneSize = _iSizeService.GetList_SizeAll() 
                //nếu là "OneSize" thì không được thêm PItem mới
                var piteminsize = _iProductItemInSizeService.GetList_ProductItemInSizeAll().Where(x=>x.ProductItemId == ProductItemId).ToList();
                //if (SizeId == _iSizeService.Get_Id_With_Size_Name(strSizeOneSize))
                //{
                //    if (piteminsize.Count >= 1)
                //    {
                //        TempData["ErrorMessage"] = "Product Item can not add \"OneSize\".";
                //        return RedirectToAction("Edit", "ProductItem", new { id = ProductItemId, tabActive = "tabtwo" });

                //    }             
                //}
                //else
                //{
                //    if (piteminsize.Count == 1 && piteminsize[0].SizeId == _iSizeService.Get_Id_With_Size_Name(strSizeOneSize))
                //    {
                //        TempData["ErrorMessage"] = "Product Item One Size can not add new size.";
                //        return RedirectToAction("Edit", "ProductItem", new { id = ProductItemId, tabActive = "tabtwo" });

                //    }            
                //}
                
                var tmp_p = _iProductItemInSizeService.GetList_ProductItemInSizeAll().Where(h => h.SizeId == SizeId && h.ProductItemId == ProductItemId).ToList();
                if (tmp_p.Count == 0)
                {
                    
                    //insert here
                    ProductItemInSize insertP = new ProductItemInSize();
                    insertP.ProductItemId = ProductItemId;
                    insertP.SizeId = SizeId;
                    insertP.Quantity = 0;
                    if (_iProductItemInSizeService.InsertProductItemInSize(insertP) != -1)
                    {
                        return RedirectToAction("Edit", "ProductItem", new { id = ProductItemId, tabActive = "tabtwo" });
                    }
                }
                else {
                    TempData["ErrorMessage"] = "This Size item already exists.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
  

            return RedirectToAction("Edit", "ProductItem", new { id = ProductItemId, tabActive = "tabtwo" });
        }

        //[WebMethod, HttpPost]
        //public JsonResult AddQuantity(long ProductItemId, long Quantity)
        //{
        //    List<string> error = new List<string>();
        //    try
        //    {
        //        ProductItemRepository _iProductItemService = new ProductItemRepository();            

        //        long result = _iProductItemService.QuantityAddProductItem(ProductItemId, Quantity);
        //        if (result !=-1)
        //        {
        //            return Json(new { success = true, quantity = result }, JsonRequestBehavior.AllowGet);
        //        }  
        //        error.Add("Error: " + " Update Fails.");
        //    }
        //    catch (Exception ex)
        //    {
        //        error.Add("Error: " + ex.Message);
        //    }
        //    return Json(new { success = false ,quantity = -1,error = error}, JsonRequestBehavior.AllowGet);
        //}

        [WebMethod]
        public JsonResult AddQuantityProductInSize(long ProductItemId,long SizeId, int Quantity)
        {
            List<string> error = new List<string>();
            try
            {
                ProductItemRepository _iProductItemService = new ProductItemRepository();
                ProductItemInSizeRepository _iProductItemInSizeService = new ProductItemInSizeRepository();
                //long result = _iProductItemService.QuantityAddProductItem(ProductItemId, Quantity);
                long result = _iProductItemInSizeService.QuantityAddProductItemAndSize(ProductItemId,SizeId, Quantity);
                if (result !=-1)
                {
                    return Json(new { success = true, quantity = result }, JsonRequestBehavior.AllowGet);
                }  
                error.Add("Error: " + " Update Fails.");
            }
            catch (Exception ex)
            {
                error.Add("Error: " + ex.Message);
            }
            return Json(new { success = false ,quantity = -1,error = error}, JsonRequestBehavior.AllowGet);
        }

        [WebMethod,HttpPost]
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
                        raw += "{\"ProductId\":\"" + item.ProductId + "\",\"ProductCode\":\"" + item.ProductCode + "\",\"ProductStockId\":\"" + item.ProductStockCode + "\",\"Price\":\"" + (item.PromotePrice != null ? item.PromotePrice.ToString() : item.MobileOnlinePrice.ToString()) + "\",\"OnlinePrice\":\"" + item.MobileOnlinePrice.ToString() + "\",\"ProductName\":\"" + ReplaceAll(item.ProductName) + "\"},";
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
        #endregion
    }
}
