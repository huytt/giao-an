using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.WebUI.CustomerService.Filters;
using PagedList;
using PagedList.Mvc;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.Repository.ams;
using System.IO;
using MSSRepository.Common;
using System.Web.Services;
using System.Net;
using System.Text;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.ops;
using System.Web.Script.Serialization;
using HTTelecom.Domain.Core.DataContext.ops;
using Newtonsoft.Json;
using HTTelecom.WebUI.CustomerService.Common;
using HTTelecom.Domain.Core.ExClass;
#region INfo
/*
 * ======================================================================================================
 * @File name: MSSController.cs
 * @Author:
 *          1. vinhphu24191
 * @Creation Date: 25/05/2015
 * @Description: Controllerof CIS
 * @List Action Method (key:AM):
 *          1. ActionResult Index();
 *          
 * @List Common Method (key:CM): 
 *         
 * @Update History:
 * ---------------------------------------------------------------------------------------------------
 * ---------------------------------------------------------------------------------------------------
 *   Last Submit Date   ||  Originator          ||  Description
 * ---------------------------------------------------------------------------------------------------
 *      25/05/2015      ||   vinhphu24191       ||  Create Controllers
 *                      ||                      ||  
 *                      ||                      ||   
 *                      ||                      ||
 * ===================================================================================================
 */
#endregion
namespace HTTelecom.WebUI.CustomerService.Controllers
{
   
    [SessionLoginFilter]
    public class CISController : Controller
    {
        //
        // GET: /CIS/
        #region Action Method (key:AM):

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Customer
        public ActionResult CustomerIndex(int? page, string email, string firstname, string lastname, string gender, string phone, string address, string DateOfBirth)
        {
            int pageNum = (page ?? 1);
            email = email??"";
            firstname=firstname??"";
            lastname = lastname??"";
            gender = gender??"";
            phone = phone??"";
            address = address?? "";
            DateOfBirth = DateOfBirth ?? "";
            ViewBag.page = page;

            TempData["email"]=email;
            TempData["firstname"]=firstname;
            TempData["lastname"]=lastname;
            TempData["gender"]=gender;
            TempData["phone"]=phone;
            TempData["address"]=address;
            TempData["dateofbirth"] = DateOfBirth;


            CustomerRepository _iCustomerService = new CustomerRepository();
            ViewBag.RegisteredCount = _iCustomerService.GetAllCustomer().Where(a => a.IsActive == true & a.IsDeleted == false).ToList().Count;
            var lst_Customers = _iCustomerService.GetList_CustomerPagingAll(pageNum, 10, email, firstname, lastname, gender, phone, address, DateOfBirth == "Empty"? "" : DateOfBirth);
            ViewBag.TabIsActive = "tabone";
            return View(lst_Customers);
        }
        public ActionResult CustomerEdit(long? id, int? pageProduct, int? pageSizeSelected, string tabActive, string ptype, string ProductNameOrder, string DateOrder, int? Delivery, int? Payment, int? IsDeleted)
        {
            if(id == null)
            {
                return RedirectToAction("CustomerIndex");
            }
            Customer cus = new Customer();
            CustomerRepository _iCustomerService = new CustomerRepository();
            ProductTypeRepository _iProductTypeService = new ProductTypeRepository();
            ProductRepository _iProductService = new ProductRepository();
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
            WishlistRepository _iWishlistService = new WishlistRepository();
            BankRepository _iBankService = new BankRepository();
            CardTypeRepository _iCardTypeService = new CardTypeRepository();

            cus = _iCustomerService.GetById(id ?? 0);
            if (cus.InBlackList == null)
            {
                cus.InBlackList = false;
            }

            TempData["tabActive"] = "tabone";
            ViewBag.SideBarMenu = "CustomerIndex";
            //load wish list
            int pageNumDefault = 1;
            int pageSizeDefault = 10;
            int pageNum = (pageProduct ?? pageNumDefault);
            int pageSize = (pageSizeSelected ?? pageSizeDefault);
            ProductNameOrder = ProductNameOrder ?? "";
            DateOrder = DateOrder?? "";
            ViewBag.page = pageProduct;

            if (tabActive != null)
            {
                TempData["tabActive"] = tabActive;
            }

            TempData["pType"] = ptype;
            TempData["pageSize"] = pageSize;
            TempData["ProductNameOrder"] = ProductNameOrder;
            TempData["DateOrder"] = DateOrder;     

            //Load danh sách để hiển thị tab Customer Card
            ViewBag.ListBank = _iBankService.GetAll(false, true);
            ViewBag.ListCardType = _iCardTypeService.GetAll(false, true);

            //đếm số lượng sản phẩm ưa thích của customer này
            ViewBag.WishlistCount = _iWishlistService.GetAll(false, true).Where(w=>w.CustomerId == id).ToList().Count;
            List<Product> tmp_lst_product =  _iProductService.GetList_Product_CustomerId((long)id);

            //đếm số lượng sản phẩm active
            ViewBag.ProductActiveCount = tmp_lst_product.Where(w => w.IsActive == true).ToList().Count;
            var pro_tmp = from ProductTypeCode in  tmp_lst_product.GroupBy(w => w.ProductTypeCode).ToList() 
                          select  ProductTypeCode;

            //lấy ra loại product type mà người này thích nhất
            if(pro_tmp.ToList().Count>0)
            {
                int count = tmp_lst_product.Where(p => p.ProductTypeCode == pro_tmp.ToList()[0].Key.ToString()).ToList().Count;
                string favorite = pro_tmp.ToList()[0].Key.ToString();
                foreach (var item in pro_tmp)
                { 
                    if (tmp_lst_product.Where(p => p.ProductTypeCode == item.Key.ToString()).ToList().Count > count)
                    {
                        favorite = item.Key.ToString();
                    }
                }
                ViewBag.TypeFavorite = _iProductTypeService.Get_ProductTypeByCode(favorite).ProductTypeName;
            }
            else
            {
                ViewBag.ProductActiveCount ="";
            }

            //Load dữ liệu order của customer này
            if (Payment != null || Delivery != null || IsDeleted != null)
            {
                TempData["tabActive"] = "tabfour";
            }
            //load data form info
            this.LoadDataCustomerForm(cus.CustomerId,ptype,pageNum,pageSize,ProductNameOrder,DateOrder,Delivery,Payment,IsDeleted);
            return View(cus);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerEdit(Customer _cus)
        {
            if (this.ValidateCustomerForm(_cus))
            {
                try
                {
                    CustomerRepository _iCustomerService = new CustomerRepository();
                    if (_iCustomerService.UpdateCustomer(_cus))
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("CustomerIndex", "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            LoadDataCustomerForm(_cus.CustomerId,
                TempData["pType"] == null ? "All" : TempData["pType"].ToString(),
                1,
                int.Parse(TempData["pageSize"] == null ? "10" : TempData["pageSize"].ToString()), 
                TempData["ProductNameOrder"] == null ? "" : TempData["ProductNameOrder"].ToString(), 
                TempData["DateOrder"] == null ? "" : TempData["DateOrder"].ToString(),-1,-1,0);
            TempData["ResponseMessage"] = 0; //0: Error
            TempData["tabActive"] = "tabone";
            return View(_cus);
        }

        [HttpPost, WebMethod]
        public JsonResult GetTotalOrder(long orderId)
        {
            OrderRepository _iOrderService = new OrderRepository();
            OrderDetailRepository _iOrderDetailService = new OrderDetailRepository();
            ProductRepository _iProductService = new ProductRepository();

            List<myTotalOrderObject> mylistOrder = new List<myTotalOrderObject>();
            foreach (var item in _iOrderDetailService.GetListByOrderId(orderId))
            {
                Product p = _iProductService.Get_ProductById(item.ProductId);
                myTotalOrderObject or = new myTotalOrderObject()
                {
                    OrderId = item.OrderId,
                    ProductStockCode = p.ProductStockCode,
                    ProductName = p.ProductName,
                    Quantity = item.OrderQuantity,
                    Price = item.UnitPriceDiscount == null ? item.UnitPrice : (decimal)item.UnitPriceDiscount,
                    Total = item.TotalPrice ?? 0
                };
                mylistOrder.Add(or);
            }
            Order o = _iOrderService.GetById(orderId);
            
            return Json(new
            {
                mylistOrder,
                //Total info
                SubTotalFee = o.SubTotalFee,
                ShippingFee = o.ShippingFee,
                TaxFee = o.TaxFee,
                TotalPaid = o.TotalPaid,                
                //status message
                Success = true,
                Message = "OK!"
            }, JsonRequestBehavior.AllowGet);
        }

        /*
        [HttpPost, WebMethod]
        public JsonResult GetDatatable(long customerId,int IsPaymentConfirmed, int IsDeliveryConfirmed, int IsDeleted)
        {
            OrderRepository _iOrderService = new OrderRepository();
            bool? isP = IsPaymentConfirmed== -1 ? (bool?)null : IsPaymentConfirmed ==0 ?false:true;
            bool? isD = IsDeliveryConfirmed == -1 ? (bool?)null : IsDeliveryConfirmed == 0 ? false : true;

            List<Order> lstOrder = _iOrderService.GetAll(customerId, IsDeleted == 1 ? true : false, true).Where(o => o.IsPaymentConfirmed == isP && o.IsDeliveryConfirmed== isD).ToList();
            List<myOrderObject> mylistOrder = new List<myOrderObject>();
            foreach (var item in lstOrder)
            {
                myOrderObject or = new myOrderObject() {
                    OrderId = item.OrderId,
                    TransactionStatusCode = item.TransactionStatusCode,
                    PaymentTypeCode = item.PaymentTypeCode,
                    OrderTypeCode = item.OrderTypeCode,
                    ReceiverName = item.ReceiverName,
                    ReceiverPhone = item.ReceiverPhone,
                    OrderCode = item.OrderCode,
                            };
                mylistOrder.Add(or);
            }

            return Json(new { mylistOrder, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
        }
         * */
        #endregion

        #region CustomerCard
        public ActionResult CustomerCardIndex(long _venC)
        {
            return View();
        }

        public ActionResult CustomerCardCreate(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("CustomerIndex");
            }
            AccountRepository _iAccountService = new AccountRepository();
            Account accOnline = (Account)Session["Account"];
            ViewBag.CreatedBy = _iAccountService.Get_AccountById(accOnline.AccountId);

            CustomerCard venC = new CustomerCard();
            venC.CustomerId = (long)id;
            venC.IsDeleted = false;
            venC.IsActive = true;
            venC.DateCreated = DateTime.Now;
            this.LoadDataCustomerCardForm((long)id);
            return View(venC);
        }

        private void LoadDataCustomerCardForm(long _cusId)
        {
            try
            {
                CustomerRepository _iCustomerService = new CustomerRepository();
                CustomerCardRepository _iCustomerAddressService = new CustomerCardRepository();
                CardTypeRepository _iCardTypeService = new CardTypeRepository();
                BankRepository _iBankService = new BankRepository();
                AccountRepository _iAccountService = new AccountRepository();

                CustomerCard ven = _iCustomerAddressService.Get_CustomerCardsById(_cusId);
                ViewBag.CardType = _iCardTypeService.GetAll(false, true);
                ViewBag.Bank = _iBankService.GetAll(false, true);
                ViewBag.CreatedBy = _iAccountService.Get_AccountById((long)ven.CreatedBy);
                ViewBag.ModifiedBy = _iAccountService.Get_AccountById((long)ven.ModifiedBy);
            }
            catch
            {
                ViewBag.ModifiedBy = ViewBag.ModifiedBy ?? new Account();
                ViewBag.CreatedBy = ViewBag.CreatedBy ?? new Account();
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerCardCreate(CustomerCard cusC)
        {
            if (this.ValidateCustomerCardForm(cusC))
            {
                try
                {
                    CustomerCardRepository _iCustomerCardService = new CustomerCardRepository();
                    Account accOnline = (Account)Session["Account"];
                    cusC.CreatedBy = accOnline.AccountId;


                    if (_iCustomerCardService.Insert(cusC) != -1)
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        TempData["tabActive"] = "tabfour";
                        return RedirectToAction("CustomerEdit/" + cusC.CustomerId.ToString(), "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            this.LoadDataCustomerCardForm((long)cusC.CustomerId);
            return View(cusC);
        }

        public ActionResult CustomerCardEdit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("CustomerIndex");
            }
            CustomerCardRepository _iCustomerCardService = new CustomerCardRepository();
            CustomerCard venA = _iCustomerCardService.Get_CustomerCardsById((long)id);
            LoadDataVendorCardForm((long)id);
            return View(venA);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CustomerCardEdit(CustomerCard venC)
        {
            if (this.ValidateCustomerCardForm(venC))
            {
                try
                {
                    CustomerCardRepository _iCustomerCardService = new CustomerCardRepository();
                    Account accOnline = (Account)Session["Account"];
                    venC.ModifiedBy = accOnline.AccountId;

                    if (_iCustomerCardService.Update(venC))
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        TempData["tabActive"] = "tabfour";
                        return RedirectToAction("CustomerEdit/" + venC.CustomerId, "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            LoadDataCustomerCardForm((long)venC.CustomerId);
            return View(venC);
        }
        #endregion

        #region AdsCustomer
        public ActionResult AdsCustomerIndex(int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;

            AdsCustomerRepository _iAdsCustomerService = new AdsCustomerRepository();

            var lst_AdsCustomers = _iAdsCustomerService.GetList_AdsCustomerPagingAll(pageNum, 10);

            return View(lst_AdsCustomers);
        }

        public ActionResult AdsCustomerEdit(long ? id)
        {
            AdsCustomer adsCus = new AdsCustomer();
            AdsCustomerRepository _iCustomerService = new AdsCustomerRepository();
            adsCus = _iCustomerService.GetById(id ?? 0);
            return View(adsCus);
        }
        #endregion

        #region Vendor
        public ActionResult VendorIndex(int ? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;

            VendorRepository _iCustomerService = new VendorRepository();
            var lst_Vendor = _iCustomerService.GetList_VendorPagingAll(pageNum, 10);
            return View(lst_Vendor);
        }

        public ActionResult VendorCreate()
        {
            AccountRepository _iAccountService = new AccountRepository();
            Account accOnline = (Account)Session["Account"];
            ViewBag.CreatedBy = _iAccountService.Get_AccountById(accOnline.AccountId);

            Vendor ven = new Vendor();
            ven.VendorId = 0;
            ven.IsDeleted = false;
            ven.IsActive = true;
            ven.DateCreated = DateTime.Now;
            this.LoadDataVendorForm(0);
            return View(ven);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VendorCreate(Vendor ven)
        {
            if (this.ValidateVendorForm(ven,null))
            {
                try
                {
                    VendorRepository _iVendorService = new VendorRepository();
                    Account accOnline = (Account)Session["Account"];
                    string pass = _iVendorService.CreatePassword(5);
                    AuthenticationKeyRepository _SecureAuthenticationRepository = new AuthenticationKeyRepository();
                    var secure = _SecureAuthenticationRepository.GetList_AuthenticationKeyAll().OrderByDescending(a => a.AuthenticationKeyId).FirstOrDefault();
                    ven.Password = Security.MD5Encrypt_Custom(pass, secure.HashToken, secure.SaltToken);
               
                    ven.CreatedBy = accOnline.AccountId;
                    long newVendorID = _iVendorService.Insert(ven);
                    if (newVendorID != -1)
                    {
                        string token = _iVendorService.GetPassChekingById(newVendorID);
                        var url_verify = GlobalVariables.HostNamePublicEX + Url.Action("ActiveVendorRegister", "Vendor", new { cId = newVendorID.ToString(), p = token });
                        _iVendorService.SendMail_Vendor(_iVendorService.GetById((long)newVendorID), url_verify, pass, 1);//Gửi mail xác nhận đăng ký tự động
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("VendorIndex", "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            this.LoadDataVendorForm((long)ven.VendorId);
            return View(ven);
        }
        public ActionResult VendorEdit(long ? id)
        {
            if (id == null)
            {
                return RedirectToAction("VendorIndex");
            }
            Vendor ven = new Vendor();
            VendorRepository _iVendorService = new VendorRepository();
            ven = _iVendorService.Get_VendorById(id ?? 0);
            this.LoadDataVendorForm(id??0);
            ViewBag.SideBarMenu = "VendorIndex";
            return View(ven);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult VendorEdit(Vendor _ven, HttpPostedFileBase vendorLogoFile)
        {
            if (this.ValidateVendorForm(_ven, vendorLogoFile))
            {
                try
                {
                    VendorRepository _iVendorService = new VendorRepository();
                    Account accOnline = (Account)Session["Account"];
                    _ven.ModifiedBy = accOnline.AccountId;
                  
                    if (_iVendorService.Update(_ven))
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        return RedirectToAction("VendorIndex", "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            this.LoadDataVendorForm(_ven.VendorId);
            return View(_ven);
        }
      

        #endregion

        #region VendorAddress
        public ActionResult VendorAddressIndex(long ? _venId)
        {
            if (_venId == null)
            {
                return RedirectToAction("VendorIndex");
            }
            VendorAddress venA = new VendorAddress();
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
            venA = _iVendorAddressService.Get_VendorAddressById(_venId ?? 0);

            ViewBag.SideBarMenu = "VendorAddressIndex";
            return View(venA);
        }

        public ActionResult VendorAddressCreate(long ? id)
        {
            if (id == null)
            {
                return RedirectToAction("VendorIndex");
            }
            AccountRepository _iAccountService = new AccountRepository();
            Account accOnline = (Account)Session["Account"];
            ViewBag.CreatedBy = _iAccountService.Get_AccountById(accOnline.AccountId);

            VendorAddress venA = new VendorAddress();
            venA.VendorId = (long)id;
            venA.IsDeleted = false;
            venA.IsActive = true;
            venA.DateCreated = DateTime.Now;

            return View(venA);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VendorAddressCreate(VendorAddress venA)
        {
            if (this.ValidateVendorAddressForm(venA))
            {
                try
                {
                    VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
                    Account accOnline = (Account)Session["Account"];
                    venA.CreatedBy = accOnline.AccountId;
                    

                    if (_iVendorAddressService.Insert(venA)!=-1)
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        TempData["tabActive"] = "tabthree";
                        return RedirectToAction("CustomerEdit/" + venA.VendorId.ToString(), "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            this.LoadDataVendorAddressForm((long)venA.VendorId);
            return View(venA);
        }
        public ActionResult VendorAddressEdit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("VendorIndex");
            }
            VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
            VendorAddress venA = _iVendorAddressService.Get_VendorAddressById((long)id);
            LoadDataVendorAddressForm((long)id);
            return View(venA);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VendorAddressEdit(VendorAddress venA)
        {
            if (this.ValidateVendorAddressForm(venA))
            {
                try
                {
                    VendorAddressRepository _iVendorService = new VendorAddressRepository();
                    Account accOnline = (Account)Session["Account"];
                    venA.ModifiedBy = accOnline.AccountId;

                    if (_iVendorService.Update(venA))
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        TempData["tabActive"] = "tabthree";
                        return RedirectToAction("VendorEdit/"+venA.VendorId, "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            LoadDataVendorAddressForm((long)venA.VendorId);
            return View(venA);
        }
        #endregion

        #region VendorCard
        public ActionResult VendorCardIndex(long _venC)
        {
            return View();
        }

        public ActionResult VendorCardCreate(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("VendorIndex");
            }
            AccountRepository _iAccountService = new AccountRepository();
            Account accOnline = (Account)Session["Account"];
            ViewBag.CreatedBy = _iAccountService.Get_AccountById(accOnline.AccountId);

            VendorCard venC = new VendorCard();
            venC.VendorId = (long)id;
            venC.IsDeleted = false;
            venC.IsActive = true;
            venC.DateCreated = DateTime.Now;
            this.LoadDataVendorCardForm((long)id);
            return View(venC);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VendorCardCreate(VendorCard venC)
        {
            if (this.ValidateVendorCardForm(venC))
            {
                try
                {
                    VendorCardRepository _iVendorCardService = new VendorCardRepository();
                    Account accOnline = (Account)Session["Account"];
                    venC.CreatedBy = accOnline.AccountId;


                    if (_iVendorCardService.Insert(venC) != -1)
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        TempData["tabActive"] = "tabfour";
                        return RedirectToAction("VendorEdit/" + venC.VendorId.ToString(), "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error
            this.LoadDataVendorCardForm((long)venC.VendorId);
            return View(venC);
        }

        public ActionResult VendorCardEdit(long? id)
        {
            if (id == null)
            {
                return RedirectToAction("VendorIndex");
            }
            VendorCardRepository _iVendorCardService = new VendorCardRepository();
            VendorCard venA = _iVendorCardService.Get_VendorCardsById((long)id);
            LoadDataVendorCardForm((long)id);
            return View(venA);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult VendorCardEdit(VendorCard venC)
        {
            if (this.ValidateVendorCardForm(venC))
            {
                try
                {
                    VendorCardRepository _iVendorCardService = new VendorCardRepository();
                    Account accOnline = (Account)Session["Account"];
                    venC.ModifiedBy = accOnline.AccountId;

                    if (_iVendorCardService.Update(venC))
                    {
                        TempData["ResponseMessage"] = 1; //1: Success
                        TempData["tabActive"] = "tabfour";
                        return RedirectToAction("VendorEdit/" + venC.VendorId, "CIS");
                    }
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = 0; //0: Error
                    TempData["ResponseTextMessage"] = ex.Message; //ErrorText
                }
            }

            TempData["ResponseMessage"] = 0; //0: Error

            
            LoadDataVendorCardForm((long)venC.VendorId);
            return View(venC);
        }
        #endregion

        #region Staff

        public ActionResult StaffIndex(int? page)
        {
            int pageNum = (page ?? 1);
            ViewBag.page = page;

            AccountRepository _iAccountService = new AccountRepository();

            var lst_Account = _iAccountService.GetList_AccountAll(pageNum, 10);

            return View(lst_Account);
        }

        public ActionResult StaffCreate()
        {
            LoadAccountFormPage(0);

            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult StaffCreate(Account accountCollection)
        {
            if (ValidateAccountFormPage(accountCollection) == true)
            {
                AccountRepository _iAccountService = new AccountRepository();

                long accountId = _iAccountService.Insert(accountCollection);
                if (accountId != -1)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("StaffIndex", "CIS");
                }
                else
                {
                    ModelState.AddModelError("InsertStatus", "There was an error occurs !!");
                    LoadAccountFormPage(0);

                    return View(accountCollection);
                }
            }
            else
            {
                LoadAccountFormPage(0);

                return View(accountCollection);
            }
        }

        public ActionResult StaffEdit(long id)
        {
            AccountRepository _iAccountService = new AccountRepository();

            var account = _iAccountService.Get_AccountById(id);

            LoadAccountFormPage(id);

            return View(account);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult StaffEdit(Account accountCollection)
        {
            if (ValidateAccountFormPage(accountCollection) == true)
            {
                AccountRepository _iAccountService = new AccountRepository();

                bool updateStatus = _iAccountService.Update(accountCollection);
                if (updateStatus == true)
                {
                    TempData["StatusMessage"] = "Success"; // Success
                    return RedirectToAction("StaffIndex", "CIS");
                }
                else
                {
                    ModelState.AddModelError("UpdateStatus", "There was an error occurs !!");
                    LoadAccountFormPage(0);

                    return View(accountCollection);
                }
            }
            else
            {
                LoadAccountFormPage(0);

                return View(accountCollection);
            }
        }
        #endregion

        #endregion

        #region Common Method (key:CM):

        #region Customer
        private bool ValidateCustomerForm(Customer _cus)
        {
            bool valid = true;
            if (_cus.FirstName == null)
            {
                ModelState.AddModelError("FirstName", "First Name is empty !!");
                 valid = false;
            }
            if (_cus.LastName == null)
            {
                ModelState.AddModelError("LastName", "Last Name is empty !!");
                valid = false;
            }

            return valid;
        }
        #endregion

        #region Customer Card
        private void LoadDataCustomerForm(long _cusId, string ptype, int pageNum, int pageSize, string ProductNameOrder, string DateOrder, int? Delivery, int? Payment, int? IsDeleted)
        {
            try
            {
                CustomerRepository _iCustomerService = new CustomerRepository();
                CustomerCardRepository _iVerdorCardService = new CustomerCardRepository();
                ContractRepository _iContractService = new ContractRepository();
                AccountRepository _iAccountService = new AccountRepository();
                Customer ven = _iCustomerService.GetById(_cusId);
                ProductTypeRepository _iProductTypeService = new ProductTypeRepository();
                ProductRepository _iProductService = new ProductRepository();

                Payment = Payment ?? -1;
                Delivery = Delivery ?? -1;
                IsDeleted = IsDeleted ?? 0;
                OrderRepository _iOrderService = new OrderRepository();
                bool? isP = Payment == -1 ? (bool?)null : Payment == 0 ? false : true;
                bool? isD = Delivery == -1 ? (bool?)null : Delivery == 0 ? false : true;
                ViewBag.OrderCustomer = _iOrderService.GetAll((long)_cusId, IsDeleted == 1 ? true : false, true).Where(o => o.IsPaymentConfirmed == isP && o.IsDeliveryConfirmed == isD).ToList();

                //đếm số lượng hóa đơn đã giao dịch thành công
                ViewBag.SuccessfulTradingCount = _iOrderService.GetAll((long)_cusId, false, true).Where(o => o.IsPaymentConfirmed == true && o.IsDeliveryConfirmed == true).ToList().Count;
                //đếm số lượng hóa đơn chưa xử lí
                ViewBag.NotProcessedCount = _iOrderService.GetAll((long)_cusId, false, true).Where(o => o.IsPaymentConfirmed == (bool?)null && o.IsDeliveryConfirmed == (bool?)null).ToList().Count;
                //đếm số lượng hóa đơn đã từng phát sinh với customer này
                ViewBag.PaymentFails = _iOrderService.GetAll((long)_cusId, false, true).Where(o => o.IsPaymentConfirmed == false && o.IsDeliveryConfirmed == (bool?)null).ToList().Count;
            

                IPagedList<Product> lst_ProductByStore;

                if (ptype == "All")
                {
                    lst_ProductByStore = _iProductService.GetList_Product_WishList_CustomerId((long)_cusId, pageNum, pageSize, ProductNameOrder, DateOrder);
                    TempData["tabActive"] = "tabtwo";
                }
                else
                {
                    if (ptype == null)
                    {
                        lst_ProductByStore = _iProductService.GetList_Product_WishList_CustomerId((long)_cusId, pageNum, pageSize, ProductNameOrder, DateOrder);
                    }
                    else
                    {
                        lst_ProductByStore = _iProductService.GetList_Product_WishList_CustomerId_ProductType((long)_cusId, ptype, pageNum, pageSize, ProductNameOrder, DateOrder);
                        TempData["tabActive"] = "tabtwo";
                    }
                }
                ViewBag.ProductPager = lst_ProductByStore;

                ViewBag.list_ProductType = _iProductTypeService.GetList_ProductTypeAll();
                if (_cusId != 0)
                {
                    ViewBag.CustomerCardsList = _iVerdorCardService.GetList_CustomerCard_CustomerId(_cusId);
                }

                ViewBag.ContractList = _iContractService.GetAllContract().Where(x => x.IsDeleted == false & x.IsActive == true);

                Account accOnline = (Account)Session["Account"];
                ViewBag.CreatedBy = _iAccountService.Get_AccountById((long)accOnline.AccountId);

            }
            catch
            {
                ViewBag.ModifiedBy = ViewBag.ModifiedBy ?? new Account();
                ViewBag.CreatedBy = ViewBag.CreatedBy ?? new Account();
            }

        }
        private bool ValidateCustomerCardForm(CustomerCard _cusC)
        {
            bool valid = true;
            if (_cusC.CardHolderName == null || _cusC.CardHolderName == "")
            {
                ModelState.AddModelError("CardHolderName", "Card Holder Name is empty !!");
                valid = false;
            }
            if (_cusC.CardNumber == 0)
            {
                ModelState.AddModelError("CardNumber", "Card Number is empty !!");
                valid = false;
            }
            /*if (_cusC.CardTypeId == null)
            {
                ModelState.AddModelError("CardTypeId", "Card Type is empty !!");
                valid = false;
            }*/
            if (_cusC.CardNumber == 0)
            {
                ModelState.AddModelError("BankId", "Bank is empty !!");
                valid = false;
            }
            return valid;
        }
        #endregion

        #region Vendor
        private bool ValidateVendorForm(Vendor _ven, HttpPostedFileBase vendorLogoFile)
        {
            bool valid = true;

            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
            int MaxSize = 500;
            if (vendorLogoFile != null)
            {
                if (!AllowedFileExtensions.Contains(vendorLogoFile.FileName.Substring(vendorLogoFile.FileName.LastIndexOf('.')).ToLower()))
                {
                    ModelState.AddModelError("giftBannerFileType", "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions));
                    valid = false;
                }
                if (vendorLogoFile.ContentLength / 1024 > MaxSize)
                {
                    ModelState.AddModelError("giftBannerFileMaxSize", "Your Photo is too large, maximum allowed size is : " + MaxSize.ToString() + "KB");
                    valid = false;
                }
            }
            if (_ven.VendorFullName == null)
            {
                ModelState.AddModelError("VendorFullName", "First Vendor Full Name is empty !!");
                valid = false;
            }
            if (_ven.VendorEmail == null)
            {
                ModelState.AddModelError("VendorEmail", "Last Vendor Email is empty !!");
                valid = false;
            }

            if (_ven.VendorAddresses == null)
            {
                ModelState.AddModelError("VendorAddresses", "Last Vendor Addresses is empty !!");
                valid = false;
            }

            if (_ven.VendorCards == null)
            {
                ModelState.AddModelError("VendorCards", "Last Vendor Cards is empty !!");
                valid = false;
            }

            if (_ven.CompanyName == null)
            {
                ModelState.AddModelError("CompanyName", "Last Company Name is empty !!");
                valid = false;
            }

            if (_ven.ContractId == null)
            {
                ModelState.AddModelError("ContractCode", "You should select one contract !!");
                valid = false;
            }
            VendorRepository _iVendorService = new VendorRepository();
            var tmp = _iVendorService.GetList_VendorAll().Where(_ => _.VendorEmail == _ven.VendorEmail).ToList();
            if (tmp.Count>0 && _ven.VendorId == 0)
            {
                ModelState.AddModelError("VendorEmail", "This email is already exists, please choose a different one!!");
                valid = false;
            }
            return valid;
        }

        private void LoadDataVendorForm(long _venId)
        {
            try
            {
                VendorRepository _iVendorService = new VendorRepository();
                VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
                VendorCardRepository _iVerdorCardService = new VendorCardRepository();
                ContractRepository _iContractService = new ContractRepository();
                AccountRepository _iAccountService = new AccountRepository();
                Vendor ven = _iVendorService.Get_VendorById(_venId);

                if (_venId != 0)
                {
                    ViewBag.VendorAddressList = _iVendorAddressService.GetList_VendorAddress_VendorId(_venId);
                    ViewBag.VendorCardsList = _iVerdorCardService.GetList_VendorCard_VendorId(_venId);
                }

                ViewBag.ContractList = _iContractService.GetAllContract().Where(x => x.IsDeleted == false & x.IsActive == true);
                ViewBag.LogoFile = ven.LogoFile??"";
                Account accOnline = (Account)Session["Account"];
                ViewBag.CreatedBy = _iAccountService.Get_AccountById((long)accOnline.AccountId);
                ViewBag.ModifiedBy = _iAccountService.Get_AccountById((long)ven.ModifiedBy);
            }
            catch
            {
                ViewBag.ModifiedBy = ViewBag.ModifiedBy ?? new Account();
                ViewBag.CreatedBy = ViewBag.CreatedBy ?? new Account();
            }
        }
        [HttpPost]
        public ActionResult ResendMailActiveVendor(long?id)
        {
            List<string> error = new List<string>();

            try
            {
                VendorRepository _iVendorService = new VendorRepository();
                Vendor ven = _iVendorService.GetById((long)id);
                string pass = _iVendorService.CreatePassword(5);
                AuthenticationKeyRepository _SecureAuthenticationRepository = new AuthenticationKeyRepository();
                var secure = _SecureAuthenticationRepository.GetList_AuthenticationKeyAll().OrderByDescending(a => a.AuthenticationKeyId).FirstOrDefault();
                ven.Password = Security.MD5Encrypt_Custom(pass, secure.HashToken, secure.SaltToken);
                ven.Token = _iVendorService.CreatePassword(10);
                if (_iVendorService.Update(ven))
                {
                    var url_verify = GlobalVariables.HostNamePublicEX + Url.Action("ActiveVendorRegister", "Vendor", new { cId = ven.VendorId, p = ven.Token });
                    _iVendorService.SendMail_Vendor(_iVendorService.GetById((long)ven.VendorId), url_verify, pass, 1);//Gửi mail xác nhận đăng ký tự động
                    TempData["ResponseMessage"] = 1; //1: Success
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                error.Add("Resend mail fails.");
            }
            catch (Exception ex)
            {
                error.Add(ex.Message);
            }
            return Json(new { success = false, quantity = -1, error = error }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region VendorAddress
        private bool ValidateVendorAddressForm(VendorAddress _venA)
        {
            bool valid = true;
            if (_venA.LocationName == null)
            {
                ModelState.AddModelError("LocationName", "Location Name is empty !!");
                valid = false;
            }
            if (_venA.Mobilephone == null)
            {
                ModelState.AddModelError("Mobilephone", "Mobilephone is empty !!");
                valid = false;
            }

            if (_venA.Telephone == null)
            {
                ModelState.AddModelError("Telephone", "Telephone is empty !!");
                valid = false;
            }

            if (_venA.Address == null)
            {
                ModelState.AddModelError("Address", "Address is empty !!");
                valid = false;
            }

            if (_venA.Ward == null)
            {
                ModelState.AddModelError("Ward", "Ward is empty !!");
                valid = false;
            }

            if (_venA.District == null)
            {
                ModelState.AddModelError("District", "Ward is empty !!");
                valid = false;
            }
            if (_venA.City == null)
            {
                ModelState.AddModelError("City", "City is empty !!");
                valid = false;
            }

            if (_venA.Email == null)
            {
                ModelState.AddModelError("Email", "Email is empty !!");
                valid = false;
            }

            return valid;
        }

        private void LoadDataVendorAddressForm(long _venId)
        {
            try
            {
                VendorRepository _iVendorService = new VendorRepository();
                VendorAddressRepository _iVendorAddressService = new VendorAddressRepository();
                AccountRepository _iAccountService = new AccountRepository();

                VendorAddress ven = _iVendorAddressService.Get_VendorAddressById(_venId);
                ViewBag.CreatedBy = _iAccountService.Get_AccountById((long)ven.CreatedBy);
                ViewBag.ModifiedBy = _iAccountService.Get_AccountById((long)ven.ModifiedBy);
            }
            catch
            {
                ViewBag.ModifiedBy = ViewBag.ModifiedBy ?? new Account();
                ViewBag.CreatedBy = ViewBag.CreatedBy ?? new Account();
            }
        }
        #endregion

        #region VendorCard
        private bool ValidateVendorCardForm(VendorCard _venC)
        {
            bool valid = true;
            if (_venC.CardHolderName == null || _venC.CardHolderName == "")
            {
                ModelState.AddModelError("CardHolderName", "Card Holder Name is empty !!");
                valid = false;
            }
            if (_venC.CardNumber == 0)
            {
                ModelState.AddModelError("CardNumber", "Card Number is empty !!");
                valid = false;
            }
            if (_venC.CardTypeId == null )
            {
                ModelState.AddModelError("CardTypeId", "Card Type is empty !!");
                valid = false;
            }
            if (_venC.CardNumber == 0)
            {
                ModelState.AddModelError("BankId", "Bank is empty !!");
                valid = false;
            }
            return valid;
        }
        private void LoadDataVendorCardForm(long _venId)
        {
            try
            {
                VendorRepository _iVendorService = new VendorRepository();
                VendorCardRepository _iVendorAddressService = new VendorCardRepository();
                CardTypeRepository _iCardTypeService = new CardTypeRepository();
                BankRepository _iBankService = new BankRepository();
                AccountRepository _iAccountService = new AccountRepository();

                VendorCard ven = _iVendorAddressService.Get_VendorCardsById(_venId);
                ViewBag.CardType = _iCardTypeService.GetAll(false, true);
                ViewBag.Bank = _iBankService.GetAll(false, true);
                ViewBag.CreatedBy = _iAccountService.Get_AccountById((long)ven.CreatedBy);
                ViewBag.ModifiedBy = _iAccountService.Get_AccountById((long)ven.ModifiedBy);             
            }
            catch
            {
                ViewBag.ModifiedBy = ViewBag.ModifiedBy ?? new Account();
                ViewBag.CreatedBy = ViewBag.CreatedBy ?? new Account();
            }
        }
        #endregion

        #region Staff
        private void LoadAccountFormPage(long accountId)
        {
            DepartmentRepository _iDepartmentService = new DepartmentRepository();
            DepartmentGroupRepository _iDepartmentGroupService = new DepartmentGroupRepository();
            OrgRoleRepository _iOrgRoleService = new OrgRoleRepository();
            AccountRepository _iAccountService = new AccountRepository();

            ViewBag.list_Department = _iDepartmentService.GetList_DepartmentAll_IsActive_IsDelete(true, false);
            ViewBag.list_DepartmentGroup = _iDepartmentGroupService.GetList_DepartmentGroupAll_IsActive_IsDeleted(true, false);
            ViewBag.list_OrgRole = _iOrgRoleService.GetList_OrgRoleAll_IsActive_IsDeleted(true, false);
            ViewBag.list_Account = _iAccountService.GetList_AccountAll_IsDeleted(false);
        }

        private bool ValidateAccountFormPage(Account accountCollection)
        {
            bool valid = true;
            AccountRepository _iAccountService = new AccountRepository();
            var lst_Account = _iAccountService.GetList_AccountAll();
            if (accountCollection.StaffId == null)
            {
                ModelState.AddModelError("StaffId", "StaffID  is empty or don't enough 6 digits number !!");
                valid = false;
            }
            if (accountCollection.FullName == null)
            {
                ModelState.AddModelError("FullName", "FullName  is empty !!");
                valid = false;
            }
            if (accountCollection.Email == null)
            {
                ModelState.AddModelError("Email", "Email  is empty !!");
                valid = false;
            }
            if (accountCollection.StaffId != null || accountCollection.Email != null)
            {
                foreach (var item in lst_Account)
                {
                    if (item.AccountId != accountCollection.AccountId)
                    {
                        if (accountCollection.StaffId == item.StaffId.Substring(2) && accountCollection.AccountId == 0)
                        {
                            ModelState.AddModelError("StaffId", "StaffId  is exist !!");
                            valid = false;
                        }
                        else if (accountCollection.StaffId == item.StaffId)
                        {
                            ModelState.AddModelError("StaffId", "StaffId  is exist !!");
                            valid = false;
                        }
                        if (accountCollection.Email == item.Email)
                        {
                            ModelState.AddModelError("Email", "Email  is exist !!");
                            valid = false;
                        }
                    }
                }
            }
            return valid;
        }


        public JsonResult LoadDDL_DepartmentGroup(long departmentId)
        {
            try
            {
                DepartmentGroupRepository _iDepartmentGroupService = new DepartmentGroupRepository();

                var ddl_DepartmentGroup = new SelectList(_iDepartmentGroupService.GetList_DepartmentGroup_DepartmentId(departmentId), "DepartmentGroupId", "DepartmentGroupName");
                return Json(new { ddl_DepartmentGroup = ddl_DepartmentGroup, Success = true, Message = "OK!" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false, Message = "An error occurred, please try later!" });
            }
        }
        #endregion

        #endregion


        private string CreateNewName(string extension, string typeMedia)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + typeMedia + "_" + Guid.NewGuid().ToString() + extension;
        }
    }
    /*
    class myOrderObject
    {
        public myOrderObject()
        {
            OrderId = 0;
            TransactionStatusCode = "";
            PaymentTypeCode = "";
            OrderTypeCode = "";
            ReceiverName = "";
            ReceiverPhone = "";
            OrderCode = "";
        }

        public long OrderId { get; set; }
        public string TransactionStatusCode { get; set; }
        public string PaymentTypeCode { get; set; }
        public string OrderTypeCode { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string OrderCode { get; set; }
    }
        
    */

    public class myTotalOrderObject
    {
        public myTotalOrderObject()
        {
            OrderId = 0;
            ProductStockCode = "";
            ProductName = "";
            Quantity = 0;
            Price = 0;
            Total = 0;
           /* CustomerName = "";
            CustomerEmail = "";
            CustomerPhone = "";
            BankId = 0;
            CardTypeId = 0;
            CardNumber = "";
            CardHolderName = "";
            ShipToAddress = "";
            ShipToDistrict = "";
            ShipToCity = "";
            DueTransactionTime = "";
            OrderDescription = "";
            TotalProduct = "";*/
        }

        public long OrderId { get; set; }
        public string ProductStockCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }        
        /*public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public long BankId { get; set; }
        public long CardTypeId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToDistrict { get; set; }
        public string ShipToCity { get; set; }
        public string DueTransactionTime { get; set; }
        public string OrderDescription { get; set; }
        public string TotalProduct { get; set; }*/
    }

}
