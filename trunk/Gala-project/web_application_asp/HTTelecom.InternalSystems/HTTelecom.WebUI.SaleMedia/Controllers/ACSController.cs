using HTTelecom.Domain.Core.Repository.acs;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.WebUI.SaleMedia.Filters;
using HTTelecom.WebUI.SaleMedia.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HTTelecom.WebUI.SaleMedia.Controllers
{
    [SessionLoginFilter]
    public class ACSController : Controller
    {
        #region Declaration
        private BankRepository _BankRepository;
        private AdsCategoryRepository _AdsCategoryRepository;
        private AdsTypeRepository _AdsTypeRepository;
        private CounterCardRepository _CounterCardRepository;
        private AdsCustomerCardRepository _AdsCustomerCardRepository;
        private AdsCustomerRepository _AdsCustomerRepository;
        private ContractRepository _ContractRepository;
        private AdsContentRepository _AdsContentRepository;
        private AdsRepository _AdsRepository;

        public ACSController()
        {
            _AdsRepository = new AdsRepository();
            _AdsContentRepository = new AdsContentRepository();
            _ContractRepository = new ContractRepository();
            _AdsCustomerRepository = new AdsCustomerRepository();
            _BankRepository = new BankRepository();
            _AdsTypeRepository = new AdsTypeRepository();
            _AdsCategoryRepository = new AdsCategoryRepository();
            _CounterCardRepository = new CounterCardRepository();
            _AdsCustomerCardRepository = new AdsCustomerCardRepository();
        }
        #endregion

        #region Action Method (key:AM):
        #region 1. Action Method: Create
        public ActionResult Create()
        {
            loadCreateAd();
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdViewModel model, FormCollection formData, HttpPostedFileBase LogoFilePath, HttpPostedFileBase ImageFilePath)
        {
            try
            {
                if (checkCreate(model, LogoFilePath, ImageFilePath) == true)
                {
                    //AdsContent-Logo
                    var name = SaveImage(LogoFilePath);
                    model.AdsContent.LogoFilePath = name;
                   
                    //AdsContent-Image
                    var name2 = SaveImage(ImageFilePath);
                    model.AdsContent.ImageFilePath = name2;
                    
                    var CounterCardId = _CounterCardRepository.GetByBankId(model.CounterCard.BankId);
                    //AdsCustomer
                    model.AdsCustomer.IsActive = true;
                    model.AdsCustomer.IsDeleted = false;
                    model.AdsCustomer.DateCreated = DateTime.Now;
                    model.AdsCustomer.DateModified = DateTime.Now;
                    model.AdsCustomer.CreatedBy = 1;
                    long idCustomer = _AdsCustomerRepository.Create(model.AdsCustomer);
                    //AdsCustomerCard
                    model.AdsCustomerCard.DateCreated = DateTime.Now;
                    model.AdsCustomerCard.DateModified = DateTime.Now;
                    model.AdsCustomerCard.IsActive = true;
                    model.AdsCustomerCard.IsDeleted = false;
                    model.AdsCustomerCard.CreatedBy = 1;
                    model.AdsCustomerCard.AdsCustomerId = idCustomer;
                    _AdsCustomerCardRepository.Create(model.AdsCustomerCard);
                    //Contract
                    model.Contract.DateCreated = DateTime.Now;
                    model.Contract.DateModified = DateTime.Now;
                    model.Contract.CreatedBy = 1;
                    model.Contract.ResponsibleClientId = 1;
                    model.Contract.ResponsibleStaffId = 1;
                    model.Contract.ResponsibleClientId = 1;
                    model.Contract.IsActive = true;
                    model.Contract.IsDeleted = false;
                    long idContract = _ContractRepository.Create(model.Contract);
                    //AdsContent
                    model.AdsContent.DateCreated = DateTime.Now;
                    long idAdsContent = _AdsContentRepository.Create(model.AdsContent);
                    //Ads
                    model.ad.DateCreated = DateTime.Now;
                    model.ad.DateModified = DateTime.Now;
                    model.ad.IsActive = true;
                    model.ad.IsDeleted = false;
                    model.ad.IsLocked = false;
                    model.ad.ContractId = idContract;
                    model.ad.AdsCustomerId = idCustomer;
                    model.ad.CounterCardId = CounterCardId;
                    model.ad.AdsContentId = idAdsContent;
                    model.ad.Views = 0;
                    model.ad.ClickThroughs = 0;
                    model.ad.OfflineDate = DateTime.Now;
                    model.ad.DateModified = DateTime.Now;
                    model.ad.CreatedBy = 1;
                    model.ad.ModifiedBy = 1;
                    long id = _AdsRepository.Create(model.ad);
                    TempData.Add("ad", model.ad.ReservationCode);
                    return RedirectToAction("Create", "TTS");
                }
                loadCreateAd();
                return View(model);
            }
            catch
            {
                loadCreateAd();
                return View(model);
            }

        }
        #endregion
        #endregion

        #region Common Method (key:CM):
        #region 1. Common Method: loadCreateAd
        private void loadCreateAd()
        {
            ViewBag.ListBank = _BankRepository.GetAll(false, true);
            ViewBag.lstAdsCategory = _AdsCategoryRepository.GetAll(false, true);
            ViewBag.lstType = _AdsTypeRepository.GetAll(false, true);
            ViewBag.lstCounterCard = _CounterCardRepository.GetAll(false, true);
        }
        #endregion

        #region 2. Common Method: SaveImage
        private string SaveImage(HttpPostedFileBase file)
        {
            string server = string.Empty;
            server = ImageUploadsFolder;
            String nameImage = CreateNewName(".jpg");
            var fullName = Path.Combine(server, nameImage);
            file.SaveAs(fullName);
            return nameImage;
        }
        #endregion

        #region 3. Common Method: CreateNewName
        private string CreateNewName(string str)
        {
            return DateTime.Now.ToString("yyyyMMdd") + "_" + Guid.NewGuid().ToString() + str;
        }
        #endregion

        #region 4. Common Method: checkCreate
        private bool checkCreate(AdViewModel model, HttpPostedFileBase LogoFilePath, HttpPostedFileBase ImageFilePath)
        {
            try
            {
                bool valid = true;
                var time = DateTime.Now;

                //---------------------Customer Contact Information
                if (model.AdsCustomer.CompanyName == null || model.AdsCustomer.CompanyName.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsCustomer.CompanyName", "Customer Contact Information - Company Name required");
                    valid = false;
                }
                if (model.AdsCustomer.RepresentativeName == null || model.AdsCustomer.RepresentativeName.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsCustomer.RepresentativeName", "Customer Contact Information - Representative Name required");
                    valid = false;
                }
                if (model.AdsCustomer.Address == null || model.AdsCustomer.Address.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsCustomer.Address", "Customer Contact Information - Address required");
                    valid = false;
                }
                if (model.AdsCustomer.Phone == null || model.AdsCustomer.Phone.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsCustomer.Phone", "Customer Contact Information - Phone required");
                    valid = false;
                }
                if (model.AdsCustomer.Email == null || model.AdsCustomer.Email.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsCustomer.Email", "Customer Contact Information - Email required");
                    valid = false;
                }
               
                //---------------------Customer Bank Information
                if (model.AdsCustomerCard.BankId == null || model.AdsCustomerCard.BankId == 0)
                {
                    ModelState.AddModelError("AdsCustomerCard.BankId", "Customer Bank Information - Bank required");
                    valid = false;
                }
                if (model.AdsCustomerCard.CardHolderName == null || model.AdsCustomerCard.CardHolderName.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsCustomerCard.CardHolderName", "Customer Bank Information - Account Name required");
                    valid = false;
                }
                long cardNumberCustomer;
                bool IsDTCardNumberCustomer = long.TryParse(model.AdsCustomerCard.CardNumber.ToString(), out cardNumberCustomer);
                if (model.AdsCustomerCard.CardNumber == null || model.AdsCustomerCard.CardNumber.ToString().Trim().Length == 0 || IsDTCardNumberCustomer == false)
                {
                    ModelState.AddModelError("AdsCustomerCard.CardNumber", "Customer Bank Information - Account Number required and must be number");
                    valid = false;
                }

                //---------------------Contract Information
                if (model.Contract.ContractCode == null || _ContractRepository.CheckCode(model.Contract.ContractCode) == false)
                {
                    ModelState.AddModelError("Contract.ContractCode", "Contract Information - Contract Identity required & must be existed");
                    valid = false;
                }
                if (model.Contract.ContractDate == null || DateTime.TryParse(model.Contract.ContractDate.Value.ToString(), out time) == false)
                {
                    ModelState.AddModelError("Contract.ContractDate", "Contract Information - Contract Date required");
                    valid = false;
                }
                decimal deposit;
                bool IsDTDepositAmount = Decimal.TryParse(model.Contract.DepositAmount.ToString(), out deposit);
                if (model.Contract.DepositAmount == null || model.Contract.DepositAmount < 0 || IsDTDepositAmount == false)
                {
                    ModelState.AddModelError("Contract.DepositAmount", "Contract Information - Deposit Amount must be a number and larger than 0");
                    valid = false;
                }

                //---------------------Media Payload Information
                if (model.ad.OnlineDate == null || DateTime.TryParse(model.ad.OnlineDate.Value.ToString(), out time) == false)
                {
                    ModelState.AddModelError("ad.OnlineDate", "Contract Information - Start Online Date required");
                    valid = false;
                }
                long period;
                bool IsDTPeriod = long.TryParse(model.ad.PeriodOnline.ToString(), out period);
                if (model.ad.PeriodOnline == null || model.ad.PeriodOnline < 0 || IsDTPeriod == false)
                {
                    ModelState.AddModelError("ad.PeriodOnline", "Contract Information - Period required and must be a number(count by days)");
                    valid = false;
                }
                if (model.ad.AdsTypeId == null || model.ad.AdsTypeId == 0)
                {
                    ModelState.AddModelError("ad.AdsTypeId", "Media Payload Information - Charge Type required");
                    valid = false;
                }
                
                //---------------------Media Content Information
                if (model.AdsContent.AdsHeader == null || model.AdsContent.AdsHeader.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsContent.AdsHeader", "Media Content Information - Header required");
                    valid = false;
                }
                if (model.AdsContent.AdsTitle == null || model.AdsContent.AdsTitle.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsContent.AdsTitle", "Media Content Information - Title required");
                    valid = false;
                }
                if (model.AdsContent.LinkSite == null || model.AdsContent.LinkSite.Trim().Length == 0)
                {
                    ModelState.AddModelError("AdsContent.LinkSite", "Media Content Information - Web Link required");
                    valid = false;
                }
                if (LogoFilePath == null || LogoFilePath.ContentLength <= 0 && LogoFilePath.ContentType.IndexOf("image") < 0)
                {
                    ModelState.AddModelError("AdsContent.LogoFilePath", "Media Content Information - Logo required");
                    valid = false;
                }
                if (ImageFilePath == null || ImageFilePath.ContentLength <= 0 && LogoFilePath.ContentType.IndexOf("image") < 0)
                {
                    ModelState.AddModelError("AdsContent.ImageFilePath", "Media Content Information - Image required");
                    valid = false;
                }
                
                //---------------------Counter Account Information
                if (model.CounterCard.BankId == null || model.CounterCard.BankId == 0)
                {
                    ModelState.AddModelError("CounterCard.BankId", "Counter Account Information - Bank required");
                    valid = false;
                }
                if (model.CounterCard.CardHolderName == null)
                {
                    ModelState.AddModelError("CounterCard.CardHolderName", "Counter Account Information - Account Name required");
                    valid = false;
                }
                long cardNumberCounter;
                bool IsDTCardNumberCounter = long.TryParse(model.CounterCard.CardNumber.ToString(), out cardNumberCounter);
                if (model.CounterCard.CardNumber == null || model.CounterCard.CardNumber.ToString().Trim().Length == 0 || IsDTCardNumberCounter == false)
                {
                    ModelState.AddModelError("CounterCard.CardNumber", "Counter Account Information - Account Number required and must be number");
                    valid = false;
                }

                return valid;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 5. Common Method: ImageUploadsFolder
        private string ImageUploadsFolder
        {
            get
            {
                return Path.Combine(Server.MapPath("/Medias/Images"));
            }
        }
        #endregion

        #region 6. Common Method: SetMesage
        //private void SetMesage(string error, string complete)
        //{
        //    if (error != null)
        //    {
        //        TempData.Add("MessageError", error);
        //    }
        //    if (complete != null)
        //    {
        //        TempData["MessageComplete"] = complete;
        //    }
        //}
        #endregion
        #endregion       
    }
}
