using BotDetect.Web.UI.Mvc;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.Repository.ops;
using HTTelecom.Domain.Core.ExClass;
using HTTelecom.WebUI.eCommerce.Filters;
using HTTelecom.WebUI.eCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HTTelecom.WebUI.eCommerce.Common;
namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class CustomerController : Controller
    {
        [WhitespaceFilter]
        public PartialViewResult Index()
        {
            if (Session["sessionGala"] == null)
            {
                var Account = (Customer)Session["sessionGala"];
                ViewBag.Account = Account;
            }
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Index", "Customer");
            return PartialView();
        }
        [NoSessionFilter]
        [WhitespaceFilter]
        public ActionResult Login(string ur)
        {

            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Login", "Customer");
            ViewBag.ur = ur;
            return View();
        }
        [HttpPost, NoSessionFilter]
        [WhitespaceFilter]
        public ActionResult Login(SignInModel model, FormCollection formData)
        {
            #region load
            CustomerRepository _CustomerRepository = new CustomerRepository();
            #endregion
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("Login", "Customer");
            ViewBag.ur = formData["ur"];
            var errorSignUp = validateSignIn(model);
            if (errorSignUp == true)
                if (Request.IsAjaxRequest())
                    return Json(new { flag = false, Message = JsonConvert.SerializeObject((List<String>)TempData["gMessageCurrent"]) });
                else
                    return View(model);
            var Customer = _CustomerRepository.Login(model.email, model.password);
            if (Customer == null)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { flag = false, Message = JsonConvert.SerializeObject(new List<string>() { (string)ViewBag.multiRes.GetString("alert_input_login_fail", ViewBag.CultureInfo) }) });
                SetMessageCurrent(new List<string>() { (string)ViewBag.multiRes.GetString("alert_input_login_fail", ViewBag.CultureInfo) });
                return View(model);
            }
            else
            {
                if (Customer.IsActive == false)
                {
                    if (Request.IsAjaxRequest())
                        return Json(new { flag = false, Message = JsonConvert.SerializeObject(new List<string>() { "notActive" }) });
                    TempData["gMessageCurrent"] = "notActive";
                    return View("ReportRegister", Customer);
                }
                Session.Add("sessionGala", Customer);
                FormsAuthentication.SetAuthCookie(".ASPXAUTH", false);
                HttpCookie aspxauth = Request.Cookies[".ASPXAUTH"];
                aspxauth.Expires = DateTime.Now.AddDays(10d);
                Response.Cookies.Add(aspxauth);
                var returnUrl = formData["ur"];
                if (Request.IsAjaxRequest())
                    return Json(new { flag = true, Message = "[]" });
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }
        }
        [NoSessionFilter]
        [WhitespaceFilter]
        public ActionResult SignUp()
        {
            MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
            Private.LoadBegin(Session, ViewBag);
            SignUpModel model = new SignUpModel();
            ViewBag.u = Url.Action("SignUp", "Customer");
            ViewBag.gender = "male";
            return View(model);
        }

        [HttpPost, NoSessionFilter]
        [CaptchaValidation("CaptchaCode", "GalagalaCaptcha", "CAPTCHA INCORRECT !")]
        public ActionResult SignUp(SignUpModel model, FormCollection data)
        {
            #region load
            CustomerRepository _CustomerRepository = new CustomerRepository();
            #endregion
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("SignUp", "Customer");
            ViewBag.gender = data["gender"] != "male" ? "female" : data["gender"];
            var errorSignUp = validateSignUp(model);
            ViewBag.chkAgree = data["chkAgree"];
            if (data["chkAgree"] == null || data["chkAgree"] != "on")
            {
                ModelState.AddModelError("chkAgree", (string)ViewBag.multiRes.GetString("alert_check_agree_sign_up", ViewBag.CultureInfo)); errorSignUp = true;
            }
            if (errorSignUp)
            {
                MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
                return View(model);
            }
            var checkEmail = _CustomerRepository.CheckEmail(model.email);
            if (checkEmail == true)
            {
                MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
                ModelState.AddModelError("email", (string)ViewBag.multiRes.GetString("alert_email_exist", ViewBag.CultureInfo));
                //SetMessageCurrent(new List<string> { "EMAIL EXIST!" });
                return View(model);
            }
            if (MvcCaptcha.IsCaptchaSolved("GalagalaCaptcha") == false)
            {
                MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
                ModelState.AddModelError("CaptchaCode", (string)ViewBag.multiRes.GetString("alert_captch_not_match", ViewBag.CultureInfo));
                //SetMessageCurrent(new List<string> { "CAPTCHA NOT MATCH!" });
                return View(model);
            }
            Customer _Cus = new Customer();
            _Cus.Email = model.email;
            //_Cus.Address = "";//_Cus.Career= "";//_Cus.CustomerRoleId = "";//_Cus.DateActive= "";
            _Cus.FirstName = model.firstname;
            //_Cus.LastIpAddress= "";
            _Cus.LastName = model.lastname;
            //_Cus.InBlackList= "";//_Cus.Phone= "";
            _Cus.Gender = ViewBag.gender;
            _Cus.DateCreated = DateTime.Now;
            _Cus.DateModified = DateTime.Now;
            _Cus.DateModifiedPassword = DateTime.Now;
            _Cus.GiftPoint = 0;
            _Cus.IsActive = false;
            _Cus.IsDeleted = false;
            _Cus.Password = model.password;
            _Cus.Address = model.address;
            _Cus.Phone = model.phone;
            _Cus.SecureAuthenticationId = 1;
            _Cus.Xoney = 0;
            _Cus.DateOfBirth = model.DateOfBirth;
            _CustomerRepository.Create(_Cus);
            //Send email
            string temp_pass = _CustomerRepository.GetPassChekingById(_Cus.CustomerId);
            //get host link
            //string url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
            //thêm vào controllers và action :D
            //url_verify += "/Customer/ActiveCustomerRegister/?cId=" + _Cus.CustomerId + "&p=" + temp_pass;
            var url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("ActiveCustomerRegister", "Customer", new { cId = _Cus.CustomerId, p = temp_pass });
            _CustomerRepository.SendMail_Customer(_Cus, url_verify, "", 1);//Gửi mail xác nhận đăng ký tự động
            //EndSend
            TempData["gMessageCurrent"] = "register";
            return View("ReportRegister");
        }
        [WhitespaceFilter]
        public ActionResult ActiveCustomerRegister(long cId, string p)//khi customer đăng kí xong họ sẽ đi đến đường dẫn kích hoạt tài khoản
        {
            Private.LoadBegin(Session, ViewBag);
            CustomerRepository _iCustomerService = new CustomerRepository();
            Customer cus = _iCustomerService.GetById(cId);
            if (cus.IsActive == true)//Tài khoản đã đăng ký và kích hoạt.
            {
                TempData["gMessageCurrent"] = "activated";
                return View("ReportRegister");
            }

            if (p != null)
            {
                if (p != _iCustomerService.GetPassChekingById(cId))
                {
                    TempData["gMessageCurrent"] = "passCheckingWrong";
                    return View("ReportRegister");
                }
            }
            else
            {
                TempData["gMessageCurrent"] = "passCheckingNull";
                return View("ReportRegister");
            }

            _iCustomerService.isActiveCustomer(cId, true);
            TempData["gMessageCurrent"] = "active";
            return View("ReportRegister");
        }
        [WhitespaceFilter]
        public ActionResult ReportRegister()
        {
            ViewBag.u = Url.Action("ReportRegister", "Customer");
            Private.LoadBegin(Session, ViewBag);
            return View();
        }
        [WhitespaceFilter]
        public ActionResult ReportForgotPassword()
        {
            ViewBag.u = Url.Action("ReportForgotPassword", "Customer");
            Private.LoadBegin(Session, ViewBag);
            return View();
        }
        [WhitespaceFilter]
        public ActionResult ForgotPassword()
        {
            MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
            Private.LoadBegin(Session, ViewBag);
            ForgotModel model = new ForgotModel();
            ViewBag.u = Url.Action("ForgotPassword", "Customer");
            return View(model);
        }
        [HttpPost, CaptchaValidation("CaptchaCode", "GalagalaCaptcha", "CAPTCHA INCORRECT !")]
        [WhitespaceFilter]
        public ActionResult ForgotPassword(ForgotModel model)
        {
            CustomerRepository _CustomerRepository = new CustomerRepository();
            Private.LoadBegin(Session, ViewBag);
            //validate 
            var errorSignUp = validateForgotModel(model);
            if (errorSignUp == true)//không có lỗi
                return View(model);
            //kiểm tra sự tồn tại của mail này trong DB
            var checkEmail = _CustomerRepository.CheckEmail(model.email);
            if (checkEmail == false)
            {
                MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
                ModelState.AddModelError("email", (string)ViewBag.multiRes.GetString("alert_email_not_exist", ViewBag.CultureInfo));
                return View(model);
            }

            if (MvcCaptcha.IsCaptchaSolved("GalagalaCaptcha") == false)
            {
                MvcCaptcha.ResetCaptcha("GalagalaCaptcha");
                ModelState.AddModelError("CaptchaCode", (string)ViewBag.multiRes.GetString("alert_captch_not_match", ViewBag.CultureInfo));
                return View(model);
            }
            //nếu check mail thành công => send mail
            Customer tmp = new Customer();
            tmp.CustomerId = _CustomerRepository.GetByEmail(model.email).CustomerId;
            tmp.Email = model.email;
            string temp_pass = _CustomerRepository.GetPassChekingById(_CustomerRepository.GetByEmail(model.email).CustomerId);
            //get host link
            //string url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
            ////thêm vào controllers và action :D
            //url_verify += "/Customer/ResetCustomerPassword/?email=" + model.email + "&p=" + temp_pass;
            var url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("ResetCustomerPassword", "Customer", new { email = model.email, p = temp_pass });
            _CustomerRepository.SendMail_Customer(tmp, url_verify, "", 2);
            TempData["gMessageCurrent"] = "ActiveChangePassword";
            return View("ReportForgotPassword");
        }
        public ActionResult ResetCustomerPassword(string email, string p)//khi customer yêu cầu reset password
        {
            try
            {
                Private.LoadBegin(Session, ViewBag);
                CustomerRepository _iCustomerService = new CustomerRepository();
                Customer cus = _iCustomerService.GetByEmail(email);
                if (cus.IsActive == false)//Tài khoản chưa kích hoạt.[nếu người dùng này chưa kích hoạt tài khoản thì không được thay đổi pass]
                {
                    TempData["gMessageCurrent"] = "notActive";
                    return View("ReportForgotPassword");
                }
                if (_iCustomerService.GetPassChekingById(_iCustomerService.GetByEmail(email).CustomerId) != p)// passchecking error
                {
                    TempData["gMessageCurrent"] = "errorPassChecking";
                    return View("ReportForgotPassword");
                }
                //send mail: gửi password mới về cho người dùng
                //Send email
                //get host link
                //string url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
                //thêm vào controllers và action :D
                //url_verify += "/Customer/ChangePassword/";
                //_iCustomerService.SendMail_Customer(cus, url_verify,new_pass, 3);//Gửi mail xác nhận thay đổi password
                //EndSend
                //TempData["gMessageCurrent"] = "passchanged";
                //đẩy qua trang changeForgotPassword luôn
                return View("ChangeForgotPassword", new ChangeForgotPasswordModel() { cId = cus.CustomerId });
            }
            catch
            {
                TempData["gMessageCurrent"] = "error";
                return View("ReportForgotPassword");
            }
        }
        [WhitespaceFilter]
        public ActionResult ChangeForgotPassword(long cId, string p)
        {
            ChangeForgotPasswordModel model = new ChangeForgotPasswordModel();
            model.cId = cId;
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("ChangeForgotPassword", "Customer");
            return View(model);
        }
        [HttpPost]
        [WhitespaceFilter]
        public ActionResult ChangeForgotPassword(ChangeForgotPasswordModel model, long cId, string p)
        {
            CustomerRepository _iCustomerService = new CustomerRepository();
            Customer cus = _iCustomerService.GetById(cId);
            Private.LoadBegin(Session, ViewBag);
            ViewBag.u = Url.Action("ChangeForgotPassword", "Customer");
            //code here
            if (model.password != model.rePassword)
            {
                ModelState.AddModelError("rePassword", (string)ViewBag.multiRes.GetString("alert_repassword_not match", ViewBag.CultureInfo));
                return View(model);
            }
            if (_iCustomerService.UpdateForgotPassword(cId, model.key, model.rePassword) == -1)
            {
                ModelState.AddModelError("key", (string)ViewBag.multiRes.GetString("alert_key_not_match", ViewBag.CultureInfo));
                return View(model);
            }
            TempData["gMessageCurrent"] = "changeSuccess";
            return View("ReportForgotPassword");
        }
        public ActionResult ReportResendMailRegister(long? cId)
        {
            ViewBag.u = Url.Action("ReportRegister", "Customer");
            Private.LoadBegin(Session, ViewBag);
            CustomerRepository _CustomerRepository = new CustomerRepository();
            string temp_pass = _CustomerRepository.GetPassChekingById((long)cId);
            //get host link
            //string url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port;
            ////thêm vào controllers và action :D
            //url_verify += "/Customer/ActiveCustomerRegister/?cId=" + cId.ToString() + "&p=" + temp_pass;
            var url_verify = Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + Url.Action("ActiveCustomerRegister", "Customer", new { cId = cId.ToString(), p = temp_pass });
            _CustomerRepository.SendMail_Customer(_CustomerRepository.GetById((long)cId), url_verify, "", 1);//Gửi mail xác nhận đăng ký tự động
            TempData["gMessageCurrent"] = "ReportResendMailRegister";
            return View("ReportRegister");
        }
        private bool validateSignIn(SignInModel model)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                if (model.email == null || model.email.Trim().Length == 0)
                {
                    ModelState.AddModelError("email", (string)ViewBag.multiRes.GetString("alert_email_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.password == null || model.password.Trim().Length == 0 || model.password.Length < 6 || model.password.Length > 20)
                {
                    ModelState.AddModelError("password", (string)ViewBag.multiRes.GetString("alert_password_between_6_to_20", ViewBag.CultureInfo));
                    error = true;
                }
                if (error == true)
                {
                    SetMessage(lstError);
                    return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                SetMessage(new List<string>() { ex.Message });
                return true;
            }
        }
        private bool validateSignUp(SignUpModel model)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                if (model.firstname == null || model.firstname.Trim().Length == 0 || model.firstname.Trim().Length > 30)
                {
                    ModelState.AddModelError("firstname", (string)ViewBag.multiRes.GetString("alert_firstname_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.lastname == null || model.lastname.Trim().Length == 0 || model.lastname.Trim().Length > 30)
                {
                    ModelState.AddModelError("lastname", (string)ViewBag.multiRes.GetString("alert_lastname_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.phone == null || model.phone.Trim().Length == 0 || model.phone.Trim().Length > 50)
                {
                    ModelState.AddModelError("phone", (string)ViewBag.multiRes.GetString("alert_phone_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.address == null || model.address.Trim().Length == 0)
                {
                    ModelState.AddModelError("address", (string)ViewBag.multiRes.GetString("alert_address_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.email == null || model.email.Trim().Length == 0 || model.email.Trim().Length > 50)
                {
                    ModelState.AddModelError("email", (string)ViewBag.multiRes.GetString("alert_email_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.password == null || model.password.Trim().Length == 0 || model.password.Length < 6 || model.password.Length > 20)
                {
                    ModelState.AddModelError("password", (string)ViewBag.multiRes.GetString("alert_password_between_6_to_20", ViewBag.CultureInfo));
                    error = true;
                }
                if (model.rePassword == null || model.rePassword.Trim().Length == 0 || model.rePassword != model.password)
                {
                    ModelState.AddModelError("rePassword", (string)ViewBag.multiRes.GetString("alert_repassword_not_match", ViewBag.CultureInfo));
                    error = true;
                }
                var date = DateTime.Now;
                if (model.DateOfBirth == null || date.Year - model.DateOfBirth.Value.Year < 5 || date.Year - model.DateOfBirth.Value.Year >= 100)
                {
                    ModelState.AddModelError("DateOfBirth", (string)ViewBag.multiRes.GetString("alert_birthdate_null", ViewBag.CultureInfo));
                    error = true;
                }
                if (error == true)
                {
                    SetMessageCurrent(lstError);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                SetMessageCurrent(new List<string>() { ex.Message });
                return true;
            }
        }
        private bool validateForgotModel(ForgotModel model)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                if (model.email == null || model.email.Trim().Length == 0)
                {
                    ModelState.AddModelError("email", (string)ViewBag.multiRes.GetString("alert_email_null", ViewBag.CultureInfo));
                    error = true;
                }
                else
                    error = false;
                return error;
            }
            catch (Exception ex)
            {
                SetMessage(new List<string>() { ex.Message });
                return true;
            }
        }
        [SessionLoginFilter]
        [WhitespaceFilter]
        public ActionResult Profile()
        {
            Private.LoadBegin(Session, ViewBag);
            var acc = (Customer)Session["sessionGala"];
            #region load
            CustomerRepository _CustomerRepository = new CustomerRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            TransactionStatusRepository _TransactionStatusRepository = new TransactionStatusRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            #endregion
            var Orders = _OrderRepository.GetByCustomer(acc.CustomerId);
            var Products = new List<Product>();
            foreach (var item in Orders)
                if (item.OrderDetail != null)
                    foreach (var itemDetail in item.OrderDetail)
                        if (Products.Where(n => n.ProductId == itemDetail.ProductId).ToList().Count == 0)
                            Products.Add(_ProductRepository.GetById(itemDetail.ProductId));
            ViewBag.Products = Products;
            ViewBag.Orders = Orders.OrderByDescending(n => n.DateCreated).ToList();
            ViewBag.TransactionStatus = _TransactionStatusRepository.GetAll(false);
            var account = _CustomerRepository.GetById(acc.CustomerId);
            ViewBag.gender = account.Gender;
            ViewBag.u = Url.Action("Profile", "Customer");
            return View(account);
        }
        [HttpPost, SessionLoginFilter]
        [WhitespaceFilter]
        public ActionResult Profile(Customer model, FormCollection data)
        {
            try
            {
                ViewBag.u = Url.Action("Profile", "Customer");
                ViewBag.gender = data["gender"].ToLower() != "male" ? "female" : data["gender"].ToLower();
                model.Gender = ViewBag.gender;
                Private.LoadBegin(Session, ViewBag);
                #region checkError
                var regex = new Regex(@"^\d+$");
                var error = false;
                var lst = new List<string>();
                if (model.FirstName == null || model.FirstName.Length == 0 || model.FirstName.Length > 30)
                { ModelState.AddModelError("FirstName", (string)ViewBag.multiRes.GetString("alert_firstname_null", ViewBag.CultureInfo)); error = true; }
                if (model.LastName == null || model.LastName.Length == 0 || model.LastName.Length > 30)
                { ModelState.AddModelError("LastName", (string)ViewBag.multiRes.GetString("alert_lastname_null", ViewBag.CultureInfo)); error = true; }
                if (model.Address == null || model.Address.Length == 0 || model.Address.Length > 200)
                { ModelState.AddModelError("Address", (string)ViewBag.multiRes.GetString("alert_address_null", ViewBag.CultureInfo)); error = true; }
                if (model.Phone == null || model.Phone.Length == 0 || model.Phone.Length > 30 || !regex.IsMatch(model.Phone))
                { ModelState.AddModelError("Phone", (string)ViewBag.multiRes.GetString("alert_input_phone_null", ViewBag.CultureInfo)); error = true; }
                var date = DateTime.Now;
                if (model.DateOfBirth == null || date.Year - model.DateOfBirth.Value.Year < 5 || date.Year - model.DateOfBirth.Value.Year >= 100)
                { ModelState.AddModelError("DateOfBirth", (string)ViewBag.multiRes.GetString("alert_birthdate_null", ViewBag.CultureInfo)); error = true; }
                #endregion
                var acc = (Customer)Session["sessionGala"];
                #region load
                CustomerRepository _CustomerRepository = new CustomerRepository();
                ProductRepository _ProductRepository = new ProductRepository();
                TransactionStatusRepository _TransactionStatusRepository = new TransactionStatusRepository();
                OrderRepository _OrderRepository = new OrderRepository();
                OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
                #endregion
                if (error == true)
                {
                    //SetMessageCurrent(lst);
                    var Orders = _OrderRepository.GetByCustomer(acc.CustomerId);
                    var Products = new List<Product>();
                    foreach (var item in Orders)
                        if (item.OrderDetail != null)
                            foreach (var itemDetail in item.OrderDetail)
                                if (Products.Where(n => n.ProductId == itemDetail.ProductId).ToList().Count == 0)
                                    Products.Add(_ProductRepository.GetById(itemDetail.ProductId));
                    ViewBag.Products = Products;
                    ViewBag.Orders = Orders.OrderByDescending(n => n.DateCreated).ToList();
                    ViewBag.TransactionStatus = _TransactionStatusRepository.GetAll(false);
                    return View(model);
                }
                var account = _CustomerRepository.GetById(acc.CustomerId);
                _CustomerRepository.Edit(acc.CustomerId, model);
                acc.FirstName = model.FirstName;
                acc.LastName = model.LastName;
                acc.Phone = model.Phone;
                acc.Address = model.Address;
                acc.DateOfBirth = model.DateOfBirth;
                acc.Gender = model.Gender;
                Session["sessionGala"] = acc;
                return RedirectToAction("Profile");
            }
            catch
            {
                return RedirectToAction("Profile");
            }
        }
        //
        public ActionResult TransactionHistory()
        {
            #region load
            CustomerRepository _CustomerRepository = new CustomerRepository();
            ProductRepository _ProductRepository = new ProductRepository();
            TransactionStatusRepository _TransactionStatusRepository = new TransactionStatusRepository();
            OrderRepository _OrderRepository = new OrderRepository();
            OrderDetailRepository _OrderDetailRepository = new OrderDetailRepository();
            #endregion
            var acc = (Customer)Session["sessionGala"];
            var id = 1;
            var Orders = _OrderRepository.GetByCustomer(id);
            var Products = new List<Product>();
            foreach (var item in Orders)
            {
                if (item.OrderDetail != null)
                    foreach (var itemDetail in item.OrderDetail)
                        if (Products.Where(n => n.ProductId == itemDetail.ProductId).ToList().Count == 0)
                            Products.Add(_ProductRepository.GetById(itemDetail.ProductId));
            }
            ViewBag.Products = Products;
            ViewBag.Orders = Orders.OrderByDescending(n => n.DateCreated).ToList();
            ViewBag.TransactionStatus = _TransactionStatusRepository.GetAll(false);
            Private.LoadBegin(Session, ViewBag);
            return View();
        }
        [HttpPost, SessionLoginFilter]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string reNewPassword)
        {
            var sessionObject = (SessionObject)Session["sessionObject"];
            ViewBag.currentLanguage = sessionObject == null ? "vi" : sessionObject.lang;
            #region
            ResourceManager multiRes = new ResourceManager("HTTelecom.WebUI.eCommerce.Common.Lang", Assembly.GetExecutingAssembly());
            CultureInfo ci = new CultureInfo(ViewBag.currentLanguage);
            #endregion
            ViewBag.multiRes = multiRes;
            ViewBag.CultureInfo = ci;
            var pass = Uri.UnescapeDataString(oldPassword);
            var newPass = Uri.UnescapeDataString(newPassword);
            var reNewPass = Uri.UnescapeDataString(reNewPassword);
            var lst = new List<string>();
            if (pass == null || pass.Length == 0 || pass.Length < 6 || pass.Length > 20)
                lst.Add((string)ViewBag.multiRes.GetString("alert_password_between_6_to_20", ViewBag.CultureInfo));
            if (pass == null || pass.Length == 0 || pass.Length < 6 || pass.Length > 20)
                lst.Add((string)ViewBag.multiRes.GetString("alert_password_between_6_to_20", ViewBag.CultureInfo));
            if (reNewPass == null || pass == null || reNewPass != newPass)
                lst.Add((string)ViewBag.multiRes.GetString("alert_repassword_not_match", ViewBag.CultureInfo));
            if (lst.Count > 0)
                return Json(JsonConvert.SerializeObject(new { error = true, value = lst }));
            var acc = (Customer)Session["sessionGala"];
            CustomerRepository _CustomerRepository = new CustomerRepository();
            var rs = _CustomerRepository.UpdatePassword(acc.CustomerId, pass, newPass);
            if (rs == 1)
                return Json(JsonConvert.SerializeObject(new { error = false, value = (string)ViewBag.multiRes.GetString("alert_change_complete", ViewBag.CultureInfo) }));
            else
            {
                lst.Add((string)ViewBag.multiRes.GetString("alert_change_fail", ViewBag.CultureInfo));
                return Json(JsonConvert.SerializeObject(new { error = true, value = lst }));
            }
        }
        [HttpPost, ValidateInput(false), SessionLoginFilter]
        public ActionResult UploadImageAvatar(HttpPostedFileBase file, long customerId)
        {
            Private.LoadBegin(Session, ViewBag);
            string createCategorysFolder = Path.Combine(Server.MapPath("~/Media"), "Customer");
            if (!System.IO.Directory.Exists(createCategorysFolder)) Directory.CreateDirectory(createCategorysFolder);
            string createCategoryFolder = Path.Combine(createCategorysFolder, "C" + customerId);// create folder Product
            if (!System.IO.Directory.Exists(createCategoryFolder)) Directory.CreateDirectory(createCategoryFolder);
            if (file != null)
            {
                List<string> list_error = ValidateImageAvatar(file);
                if (list_error.Count > 0)
                {
                    var str = "";
                    foreach (var item in list_error)
                        str += item + "\n";
                    //ModelState.AddModelError("UploadImage", str);
                    SetMessageCurrent(list_error);
                    return RedirectToAction("Profile");
                }
                //===============================================
                String nameImage = CreateNewName(Path.GetExtension(file.FileName));
                string fullPathName = Path.Combine(createCategoryFolder, nameImage); //path full
                ImageHelper.UploadImage(file, fullPathName);
                //===============================================
                string Url_avatar = "/Media/Customer/C" + customerId + "/" + nameImage;
                CustomerRepository _iCustomerService = new CustomerRepository();
                if (_iCustomerService.UpdateAvatar(customerId, Url_avatar))
                {
                    var cus = _iCustomerService.GetById(customerId);
                    var sessCus = (Customer)Session["sessionGala"];
                    sessCus.AvatarPhotoUrl = cus.AvatarPhotoUrl;
                    Session["sessionGala"] = sessCus;
                    return RedirectToAction("Profile");
                }
            }
            SetMessageCurrent(new List<string>() { (string)ViewBag.multiRes.GetString("alert_file_not_exist", ViewBag.CultureInfo) });
            return RedirectToAction("Profile");
        }
        private List<string> ValidateImageAvatar(HttpPostedFileBase file)
        {
            long maxSize = 512;
            List<string> list_error = new List<string>();
            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };
            if (file != null)
            {
                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    list_error.Add((string)ViewBag.multiRes.GetString("alert_file_type_upload", ViewBag.CultureInfo) + string.Join(", ", AllowedFileExtensions));

                }
                if (file.ContentLength / 1024 > maxSize)
                {
                    list_error.Add((string)ViewBag.multiRes.GetString("alert_file_large", ViewBag.CultureInfo) + maxSize + "KB");

                }
            }
            return list_error;
        }
        private string CreateNewName(string extension)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + Guid.NewGuid().ToString() + extension;
        }
        [SessionLoginFilter]
        public ActionResult Logout()
        {
            Session.Remove("sessionGala");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CheckEmail(string email)
        {
            #region load
            CustomerRepository _CustomerRepository = new CustomerRepository();
            if (_CustomerRepository.CheckEmail(email) == true)
            {
                return Json("true");
            }
            #endregion
            return Json("false");
        }
        private void SetMessage(List<string> message)
        {
            if (TempData["gMessage"] != null) TempData.Remove("gMessage");
            TempData.Add("gMessage", message);
        }
        private void SetMessageCurrent(List<string> message)
        {
            if (TempData["gMessageCurrent"] != null) TempData.Remove("gMessageCurrent");
            TempData.Add("gMessageCurrent", message);
        }
    }
}
