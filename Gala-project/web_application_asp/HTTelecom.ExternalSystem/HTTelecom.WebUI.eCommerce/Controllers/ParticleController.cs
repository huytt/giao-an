using HTTelecom.Domain.Core.DataContext.ams;
using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.ams;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.WebUI.eCommerce.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using PagedList;
using HTTelecom.Domain.Core.Common;
namespace HTTelecom.WebUI.eCommerce.Controllers
{
    public class ParticleController : Controller
    {
        //
        #region Home Page
        [HttpGet]
        public ActionResult s_all()
        {
            // GET: /Store/All
            if (Request.IsAjaxRequest())
            {
                StoreInMediaRepository _StoreInMediaRepository = new StoreInMediaRepository();
                var lstStore = _StoreInMediaRepository.GetByHome().Distinct().OrderByDescending(n => n.Store.DateCreated).ToList();
                var rs = Private.ConvertListStore(lstStore, Url);
                return Json(new { data = rs }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult b_top()
        {
            // GET: /Brand/Hang dau
            if (Request.IsAjaxRequest())
            {
                BrandRepository _BrandRepository = new BrandRepository();
                MediaRepository _MediaRepository = new MediaRepository();
                var lstBrand = _BrandRepository.GetAll(false, true);
                List<Tuple<Brand, Media, Media>> Brands = new List<Tuple<Brand, Media, Media>>();
                foreach (var item in lstBrand)
                {
                    var mediaLogo = new Media();
                    var mediaBanner = new Media();
                    if (item.LogoMediaId != null && item.LogoMediaId > 0)
                        mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.LogoMediaId));
                    else mediaLogo = null;
                    if (item.BannerMediaId != null && item.BannerMediaId > 0)
                        mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.BannerMediaId));
                    else mediaBanner = null;
                    Brands.Add(new Tuple<Brand, Media, Media>(item, mediaLogo, mediaBanner));
                }
                var rs = Private.ConvertListBrand(Brands, Url);
                return Json(new { data = rs }, JsonRequestBehavior.AllowGet);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult p(int type, long? id, long? sId, long? bId)
        {
            //type 1 : san pham moi nhat
            //type 2 : san pharm noi bat
            //     3  : san pham ban chay
            //type 4 : san pham vua xem
            if (Request.IsAjaxRequest())
            {
                type = type == null ? 1 : type;
                ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
                var lstProduct = new List<ProductInMedia>();
                var rs = new List<ProductInMedia>();
                switch (Convert.ToInt32(type))
                {
                    case 1:
                        //Trang Product
                        //Danh sach San pham mua nhieu nhat, chua filter
                        lstProduct = _productInMediaRepository.GetBySale();

                        rs = lstProduct.OrderByDescending(n => n.Product.ProductName).Take(10).ToList();
                        break;
                    case 2:
                        //Trang Product
                        lstProduct = _productInMediaRepository.GetByHome().OrderByDescending(n => n.Product.DateCreated).ToList();
                        rs = lstProduct.OrderBy(n => n.Product.ProductName).Take(10).ToList();
                        break;
                    case 3:
                        //Trang Product
                        //Danh sach moi nhat
                        lstProduct = _productInMediaRepository.GetByHome().OrderByDescending(n => n.Product.DateCreated).ToList();
                        rs = lstProduct.Take(10).ToList();
                        break;
                    case 4:
                        //Trang Product
                        //Danh sach San pham da xem [count: 10]
                        var sessionObject = (SessionObject)Session["sessionObject"];
                        var lstViewd = sessionObject == null ? new List<long>() : sessionObject.ListProduct;
                        List<ProductInMedia> lstProductView = new List<ProductInMedia>();
                        ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                        foreach (var item in lstViewd)
                        {
                            var itemProduct = _ProductInMediaRepository.GetByProduct(item);
                            var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                            if (itemPro != null)
                                lstProductView.Add(itemPro);
                        }
                        rs = lstProductView;
                        break;
                    case 5:
                        //Trang Product
                        //Danh sach San pham cung Category
                        if (id == null)
                        {
                            rs = new List<ProductInMedia>();
                            break;
                        }
                        Common.Common _common = new Common.Common();
                        ProductRepository _ProductRepository = new ProductRepository();
                        ProductInCategoryRepository _ProductInCategoryRepository = new ProductInCategoryRepository();
                        var lst_Product = new List<Product>();
                        var lstProductCategoryOther = _ProductInCategoryRepository.GetByProduct(Convert.ToInt64(id));
                        foreach (var item in lstProductCategoryOther)
                            lst_Product.AddRange(_ProductRepository.GetListByCategory(Convert.ToInt64(item.CategoryId)).Take(10).ToList());
                        var newListProductByCategory = _common.GetRandom(lst_Product, 30);
                        List<ProductInMedia> lstProductCategory = new List<ProductInMedia>();
                        foreach (var item in newListProductByCategory)
                        {
                            var itemProduct = _productInMediaRepository.GetByProduct(Convert.ToInt64(item.ProductId));
                            var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                            if (itemPro != null)
                                lstProductCategory.Add(itemPro);
                        }
                        rs = lstProductCategory;
                        break;
                    case 6:
                        //Trang Product
                        //Danh sach San pham noi bat cua Store 
                        if (sId == null || id == null)
                        {
                            rs = new List<ProductInMedia>();
                            break;
                        }
                        ProductRepository _productRepository = new ProductRepository();
                        var lstProductOther = _productRepository.GetByStore(Convert.ToInt64(sId));
                        List<ProductInMedia> lst = new List<ProductInMedia>();
                        foreach (var item in lstProductOther)
                        {
                            var itemProduct = _productInMediaRepository.GetByProduct(item.ProductId);
                            var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                            if (itemPro != null && itemPro.ProductId != id)
                                lst.Add(itemPro);
                        }
                        rs = lst.Take(10).ToList();
                        break;
                    case 7:
                        //Trang Brand

                        if (bId == null)
                        {
                            rs = new List<ProductInMedia>();
                            break;
                        }
                        if (bId == 0)
                        {
                            //Goi y san pham
                            Common.Common cm = new Common.Common();
                            var lst_ProductHome = _productInMediaRepository.GetByHome();
                            rs = cm.GetRandom(lstProduct, 10);
                            break;
                        }
                        else
                        {
                            //San pham ban chay cua Brand
                        }
                        break;
                    default:
                        rs = new List<ProductInMedia>();
                        break;
                }
                var data = Private.ConvertListProduct(rs, Url);
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        #endregion

        #region Layout
        [HttpGet]
        public ActionResult c_index(string lang)
        {
            #region load
            CategoryRepository _CategoryRepository = new CategoryRepository();
            Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
            MediaRepository _MediaRepository = new MediaRepository();
            lang = lang == null ? "vi" : lang;
            #endregion
            var LstCate = _CategoryRepository.GetAllAndProductCount(true, false);
            List<Tuple<Category, int, Media, Media>> lst = new List<Tuple<Category, int, Media, Media>>();
            foreach (var item in LstCate)
            {
                var mediaLogo = new Media();
                if (item.Item1.LogoMediaId != null && item.Item1.LogoMediaId > 0) mediaLogo = _MediaRepository.GetById(Convert.ToInt64(item.Item1.LogoMediaId));
                else mediaLogo = null;
                var mediaBanner = new Media();
                if (item.Item1.BannerMediaId != null && item.Item1.BannerMediaId > 0) mediaBanner = _MediaRepository.GetById(Convert.ToInt64(item.Item1.BannerMediaId));
                else mediaBanner = null;
                var cate_mutil = new Category_MultiLang();
                cate_mutil = _Category_MultiLangRepository.GetByLanguage(item.Item1.CategoryId, lang);
                item.Item1.CategoryName = cate_mutil != null ? cate_mutil.CategoryName : item.Item1.CategoryName;
                lst.Add(new Tuple<Category, int, Media, Media>(item.Item1, item.Item2, mediaLogo, mediaBanner));
            }
            var rs = Private.ConvertListCategory(lst, Url);
            var lv1 = rs.Where(n => n.CateLevel == 0).OrderBy(n => n.OrderNumber).ToList();
            var lv2 = rs.Where(n => n.CateLevel == 1).OrderBy(n => n.OrderNumber).ToList();
            var lv3 = rs.Where(n => n.CateLevel == 2).OrderBy(n => n.OrderNumber).ToList();
            return Json(new { cate_1 = lv1, cate_2 = lv2, cate_3 = lv3 }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Category
        [HttpPost]
        public ActionResult category_filter(long? category, int? step, int? typeSearch, string lang)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    #region request
                    typeSearch = typeSearch == null ? 0 : typeSearch;
                    step = step == null ? 0 : step;
                    lang = lang == null ? "vi" : lang;
                    category = category == null ? 0 : category;
                    ProductRepository _ProductRepository = new ProductRepository();
                    ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
                    CategoryRepository _CategoryRepository = new CategoryRepository();
                    List<ProductInMedia> lst = new List<ProductInMedia>();
                    var model = _CategoryRepository.GetById(Convert.ToInt64(category));

                    #region Product
                    List<Tuple<Category, int>> ListTotalCategory = _CategoryRepository.GetAllAndProductCount(model.CategoryId, ViewBag.currentLanguage);
                    //ListTotalCategory = ListTotalCategory.Where(n => n.Item1.ParentCateId == model.CategoryId).ToList();
                    #region Product
                    var lstProduct = _ProductRepository.GetListByCategory(model.CategoryId);
                    foreach (var item in ListTotalCategory)
                        lstProduct.AddRange(_ProductRepository.GetListByCategory(item.Item1.CategoryId));
                    #endregion
                    #region ListProductMedia
                    foreach (var item in lstProduct)
                    {
                        var itemProduct = _productInMediaRepository.GetByProduct(item.ProductId);
                        var itemPro = itemProduct.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                        if (itemPro != null)
                            lst.Add(itemPro);
                    }
                    lst = lst.GroupBy(n => n.ProductId).Select(g => g.First()).ToList();
                    var ListProduct = Private.ConvertListProduct(lst.Skip(Convert.ToInt32(step)*20).Take(20).ToList(), Url);
                    return Json(new { data = ListProduct });
                    #endregion

                    //var lstProduct = _ProductRepository.GetListByCategory(Convert.ToInt64(category));
                    //var lstCategoryChildren = new List<Category>();
                    //lstCategoryChildren = _CategoryRepository.GetListChildrenCategoryByCategoryId(Convert.ToInt64(category)).Where(n => n.IsActive == true && n.IsDeleted == false).ToList();
                    //foreach (var item in lstCategoryChildren)
                    //{
                    //    lstProduct.AddRange(_ProductRepository.GetListByCategory(item.CategoryId));
                    //}
                    #endregion
                    //var lstProductInMedia = new List<Tuple<ProductInMedia, double>>();
                    //foreach (var item in lstProduct)
                    //{
                    //    var lstItemPro = _productInMediaRepository.GetByProduct(item.ProductId);
                    //    if (lstItemPro.Count > 0)
                    //    {
                    //        var itemPro = lstItemPro.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                    //        if (itemPro != null)
                    //            lstProductInMedia.Add(new Tuple<ProductInMedia, double>(itemPro, itemPro.Product.PromotePrice != null && itemPro.Product.PromotePrice != 0 ? Convert.ToDouble(itemPro.Product.PromotePrice) : Convert.ToDouble(itemPro.Product.MobileOnlinePrice)));
                    //    }
                    //}
                    //if (step != null && step < 10)
                    //{
                    //    var lst = lstProductInMedia.Skip((Convert.ToInt32(step) * 10)).Take(10).ToList();
                    //    return Json(new { data = Private.ConvertListProduct(lst, Url) });
                    //}
                    //else
                    //    return Json(new { });
                    #endregion
                }
                else
                    return Json(new { });
            }
            catch
            {
                return Json(new { });
            }
        }
                    #endregion
        #region Barnd
        [HttpPost]
        public ActionResult brand_filter(long? brand, int? step, int? typeSearch)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    #region request
                    typeSearch = typeSearch == null ? 0 : typeSearch;
                    step = step == null ? 0 : step;
                    brand = brand == null ? 0 : brand;
                    ProductRepository _ProductRepository = new ProductRepository();
                    ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
                    var lstProduct = _ProductRepository.GetByBrand(Convert.ToInt64(brand));
                    var lstProductInMedia = new List<Tuple<ProductInMedia, double>>();
                    foreach (var item in lstProduct)
                    {
                        var lstItemPro = _productInMediaRepository.GetByProduct(item.ProductId);
                        if (lstItemPro.Count > 0)
                        {
                            var itemPro = lstItemPro.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                            if (itemPro != null)
                                lstProductInMedia.Add(new Tuple<ProductInMedia, double>(itemPro, itemPro.Product.PromotePrice != null && itemPro.Product.PromotePrice != 0 ? Convert.ToDouble(itemPro.Product.PromotePrice) : Convert.ToDouble(itemPro.Product.MobileOnlinePrice)));
                        }
                    }
                    if (step != null && step < 10)
                    {
                        var lst = lstProductInMedia.Skip((Convert.ToInt32(step) * 10)).Take(10).ToList();
                        return Json(new { data = Private.ConvertListProduct(lst, Url) });
                    }
                    else
                        return Json(new { });
                    #endregion
                }
                else
                    return Json(new { });
            }
            catch
            {
                return Json(new { });
            }
        }
        #endregion

        #region Search
        [HttpGet]
        public ActionResult search_index(string q)
        {
            ProductRepository _ProductRepository = new ProductRepository();
            var lstProduct = _ProductRepository.GetBySearch(0, 0, q.Decode()).GroupBy(n => n.ProductId).Select(g => g.First()).Take(10).ToList();
            var lst = new List<Product>();
            foreach (var item in lstProduct)
            {
                lst.Add(_ProductRepository.GetById(item.ProductId));
            }
            return Json(new { data = Private.ConvertListProductSimple(lst, Url) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult search_filter(long? cate, long? brand, int? step, string q, int? typeSearch)
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    #region request
                    typeSearch = typeSearch == null ? 0 : typeSearch;
                    step = step == null ? 0 : step;
                    brand = brand == null ? 0 : brand;
                    cate = cate == null ? 0 : cate;
                    ProductRepository _ProductRepository = new ProductRepository();
                    ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
                    var lstProduct = _ProductRepository
                        .GetBySearch(Convert.ToInt64(cate), Convert.ToInt64(brand), q.Decode())
                        .GroupBy(n => n.ProductId).Select(g => g.First()).ToList();
                    if (step == 0 && ((lstProduct.Count > 20 && q.Length >= 3 && q.Length <= 5) || (lstProduct.Count > 10 && q.Length > 5) || (lstProduct.Count > 1 && q.Length > 10)))
                    {
                        #region add Keyword
                        SearchKeywordRepository _SearchKeywordRepository = new SearchKeywordRepository();
                        if (_SearchKeywordRepository.IsExist(q.Trim()))
                            _SearchKeywordRepository.EditHitCount(q);
                        else
                        {
                            SearchKeyword sK = new SearchKeyword();
                            sK.DateCreated = DateTime.Now;
                            sK.DateModified = DateTime.Now;
                            sK.HitCount = 1;
                            sK.IsDeleted = false;
                            sK.Keyword = q.Trim().ToUpper();
                            _SearchKeywordRepository.Create(sK);
                        }
                        #endregion
                    }
                    var lstProductInMedia = new List<Tuple<ProductInMedia, double>>();
                    foreach (var item in lstProduct)
                    {
                        var lstItemPro = _productInMediaRepository.GetByProduct(item.ProductId);
                        if (lstItemPro.Count > 0)
                        {
                            var itemPro = lstItemPro.Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3" && n.Media.IsActive == true && n.Media.IsDeleted == false).FirstOrDefault();
                            if (itemPro != null)
                                lstProductInMedia.Add(new Tuple<ProductInMedia, double>(itemPro, itemPro.Product.PromotePrice != null && itemPro.Product.PromotePrice != 0 ? Convert.ToDouble(itemPro.Product.PromotePrice) : Convert.ToDouble(itemPro.Product.MobileOnlinePrice)));
                        }
                    }
                    typeSearch = typeSearch == null ? 0 : typeSearch;
                    ViewBag.typeSearch = typeSearch;
                    switch (typeSearch)
                    {
                        case 0:
                            //DeCu
                            lstProductInMedia = lstProductInMedia.OrderByDescending(n => n.Item1.Product.VisitCount).ToList();
                            break;
                        case 1:
                            //Ban Chay
                            lstProductInMedia = lstProductInMedia.OrderByDescending(n => n.Item1.Product.VisitCount).ToList();
                            break;
                        case 2:
                            //New
                            lstProductInMedia = lstProductInMedia.OrderByDescending(n => n.Item1.Product.DateCreated).ToList();
                            break;
                        case 3:
                            //Gia giam
                            lstProductInMedia = lstProductInMedia.OrderByDescending(n => n.Item2).ToList();
                            break;
                        case 4:
                            //Gia tang
                            lstProductInMedia = lstProductInMedia.OrderBy(n => n.Item2).ToList();
                            break;
                        default:
                            break;
                    }
                    if (step != null && step < 10)
                    {
                        var lst = lstProductInMedia.Skip((Convert.ToInt32(step) * 10)).Take(10).ToList();
                        return Json(new { data = Private.ConvertListProduct(lst, Url) });
                    }
                    else
                        return Json(new { });
                    #endregion
                }
                else
                    return Json(new { });
            }
            catch
            {
                return Json(new { });
            }
        }
        #endregion

        #region Filter Product
        public ActionResult ProductAll(string _type, int? _step, string _sort, int? _orderby)
        {
            //1 .Sản phẩm bán chạy
            //2 .Sản phẩm nổi bật
            //3 .Sản phẩm mới
            //4 .Đề cử
            if (Request.IsAjaxRequest())
            {
                _type = _type == "" || _type == null ? "SELLING" : _type;
                _sort = _sort == "" || _sort == null || !(_sort == "desc" || _sort == "asc") ? "desc" : _sort;
                _orderby = _orderby == null || !(_orderby >= 1 && _orderby <= 5) ? 1 : _orderby;//có 5 kiểu sort
                _step = _step ?? 0;
                ProductInMediaRepository _productInMediaRepository = new ProductInMediaRepository();
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                ProductInPriorityRepository _iProductIsPriorityService = new ProductInPriorityRepository();
                var lstProduct = _productInMediaRepository.GetByHome().OrderByDescending(n => n.Product.DateCreated).ToList();
                var rs = new List<ProductInMedia>();
                switch (_type)
                {
                    case "SELLING":
                        var _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(1);
                        foreach (var item in _lstProductInPriority)
                        {
                            var itemProduct = _ProductInMediaRepository.GetByProduct(Convert.ToInt64(item.ProductId))
                                .Where(n => n.Media.MediaType.MediaTypeCode == "STORE-3").FirstOrDefault();
                            if (itemProduct != null && Private.CheckProductAvailabel(itemProduct))
                                rs.Add(itemProduct);
                        }
                        break;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "21":
                        break;
                    default:
                        rs = new List<ProductInMedia>();
                        break;
                }
                var data = Private.ConvertListProduct(rs, Url);
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
        public PartialViewResult Login(string ur)
        {
            ViewBag.ur = ur;
            if (Request.IsAjaxRequest())
            {
                loadCompoment();
                return PartialView();
            }
            else return PartialView(null);
        }
        public PartialViewResult SignUp()
        {
            if (Request.IsAjaxRequest())
            {
                loadCompoment();
                return PartialView();
            }
            else return PartialView(null);
        }
        public void loadCompoment()
        {
            var sessionObject = (SessionObject)Session["sessionObject"];
            if (sessionObject == null)
            {
                sessionObject = new SessionObject();
                sessionObject.lang = "vi";
                sessionObject.ListCart = new List<Tuple<long, int, long>>();
                sessionObject.ListProduct = new List<long>();
                sessionObject.PaymentProduct = new List<Tuple<long, int, long, long, long>>();
                Session.Add("sessionObject", sessionObject);
            }
            sessionObject = (SessionObject)Session["sessionObject"];
            ViewBag.currentLanguage = sessionObject.lang;
            #region
            ResourceManager multiRes = new ResourceManager("HTTelecom.WebUI.eCommerce.Common.Lang", Assembly.GetExecutingAssembly());
            CultureInfo ci = new CultureInfo(ViewBag.currentLanguage);
            #endregion
            ViewBag.multiRes = multiRes;
            ViewBag.CultureInfo = ci;
        }
        #region  load Ajax
        //1. Load Menu Left
        //2. Load Province..
        //3. Load District By ProvinceId
        //4. Load Review By ProductId
        //5. Load ProductInMedia By ProductId
        [HttpGet]
        public ActionResult GetProducts(string _type, int? _step, string _sort, int? _orderby)
        {
            _type = _type == "" || _type == null ? "SELLING" : _type;
            _sort = _sort == "" || _sort == null || !(_sort == "desc" || _sort == "asc") ? "desc" : _sort;
            _orderby = _orderby == null || !(_orderby >= 1 && _orderby <= 5) ? 1 : _orderby;//có 5 kiểu sort
            _step = _step ?? 0;

            List<string> Error = new List<string>();
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            var lstProduct = _iProductInMediaService.GetByHome().GroupBy(i => i.ProductId).Select(g => g.First()).OrderByDescending(n => n.Product.DateCreated).ToList();
            var ProductHots = lstProduct.OrderBy(n => n.Product.ProductName).Take(10).ToList();
            var ProductBuys = lstProduct.OrderByDescending(n => n.Product.ProductName).Take(10).ToList();


            return Json(new { IsSuccess = false, error = Error }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProduct(string _type, int? _step, string _sort, int? _orderby, int _curCount)
        {
            /*
             * 1.Type1: Selling Products(bán chạy)    ->"Selling"
             * 2.Type2: New Products(mới)             ->"New"
             * 3.Type3: Highlights Products(nổi bật)  ->"Highlights"
             * 4.Type4: Products Nomination(đề cử)    ->"Monination"
             */
            /*
             * 1.Sort1: Ascend      ->"asc"
             * 2.Sort2: Descending  ->"desc"
             */
            /*
             * 1._sortType 1: sort ProductId
             * 2._sortType 2: sort ProductName
             * 3._sortType 3: sort StoreId
             * 4._sortType 4: sort StoreName
             * 5._sortType 5: sort MobileOnlinePrice
             */

            //Kiểm tra đối số
            _type = _type == "" || _type == null ? "SELLING" : _type;
            _sort = _sort == "" || _sort == null || !(_sort == "desc" || _sort == "asc") ? "desc" : _sort;
            _orderby = _orderby == null || !(_orderby >= 1 && _orderby <= 5) ? 1 : _orderby;//có 5 kiểu sort
            _step = _step ?? 0;
            _curCount = _curCount == null ? 10 : _curCount;

            //Load Object
            List<string> Error = new List<string>();
            ProductInPriorityRepository _iProductIsPriorityService = new ProductInPriorityRepository();
            ProductInMediaRepository _iProductInMediaService = new ProductInMediaRepository();
            MediaRepository _iMediaService = new MediaRepository();
            MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
            long MediaTypeId = _iMediaTypeService.GetByTypeCode("STORE-3").MediaTypeId;//lấy hình kiểu Product-Highlight-Banner
            IList<ProductInPriority> _lstProductInPriority = new List<ProductInPriority>();
            List<ProductInMedia> lstProductInMedia_Result = new List<ProductInMedia>();
            List<ProductObject> _lstpObj = new List<ProductObject>();

            //Tat ca Product avaiable
            var lstProduct = _iProductInMediaService.GetByAvaiable();
            try
            {
                switch (_type.ToUpper())
                {
                    case "SELLING":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(1);
                        break;
                    case "NEW":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(3);
                        break;
                    case "HIGHTLIGHTS":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(2);
                        break;
                    case "NOMINATION":
                        _lstProductInPriority = _iProductIsPriorityService.GetList_ProductInPriorityWithType(4);
                        break;
                    default:
                        Error.Add("Error: Types error.");
                        break;
                }
                //Xử lí add ProductInMedia
                foreach (var pip in _lstProductInPriority)
                {
                    foreach (var pim in _iProductInMediaService.GetByProduct((long)pip.ProductId))
                    {
                        Media media = _iMediaService.GetById((long)pim.MediaId);
                        if (media != null && media.MediaTypeId == MediaTypeId //MediaTypeId lúc này là "STORE-3" đã lấy id ở trên
                            && media.IsDeleted == false && media.IsActive == true)
                        {
                            lstProductInMedia_Result.Add(pim);
                            var itemTemp = lstProduct.Where(n => n.ProductId == pim.ProductId).FirstOrDefault();
                            if (itemTemp != null)
                                lstProduct.Remove(itemTemp);
                        }
                    }
                }
                lstProductInMedia_Result.AddRange(lstProduct);
                var lst = Private.ConvertListProduct(lstProductInMedia_Result, Url);
                //sắp xếp
                switch (_orderby)
                {
                    case 1:
                        lst = Utils.Sort(lst, n => n.ProductId, _sort);
                        break;
                    case 2:
                        lst = Utils.Sort(lst, n => n.ProductName, _sort);
                        break;
                    case 3:
                        lst = Utils.Sort(lst, n => n.StoreId, _sort);
                        break;
                    case 4:
                        lst = Utils.Sort(lst, n => n.StoreName, _sort);
                        break;
                    case 5:
                        lst = Utils.Sort(lst, n => n.MobileOnlinePrice, _sort);
                        break;
                    default:
                        lst = Utils.Sort(lst, n => n.ProductId, _sort);
                        break;
                }

                _lstpObj = _step == -1 ? lst.Take((int)_curCount).ToList() :
                    lst.Skip(10).Skip(Convert.ToInt32(_step) * 10).Take(10).ToList();

                if (Error.Count == 0)
                {
                    return Json(new { IsSuccess = true, Count = _lstpObj.Count, ListProductObject = _lstpObj }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Error.Add("Error: " + ex.Message);
            }
            return Json(new { IsSuccess = false, error = Error }, JsonRequestBehavior.AllowGet);
        }
        // 6 . Load Store/All ajax {_numColumn, _sortBy, _order, _step}   //1 - 10
        public ActionResult GetStore(int? _step, int? _sortBy, string _order, int? _curCount)
        {
            List<string> Error = new List<string>();
            try
            {
                _curCount = _curCount == null ? 6 : _curCount;
                _step = _step == null ? -1 : _step;
                _order = _order != null ? _order : "";
                _sortBy = _sortBy != null ? _sortBy : 0;

                StoreRepository _iStoreService = new StoreRepository();
                List<StoreObject> lstStoreObj = new List<StoreObject>();
                StoreInMediaRepository _iStoreInMediaService = new StoreInMediaRepository();
                var lstStore = _iStoreInMediaService.GetAllStore();
                //MediaTypeRepository _iMediaTypeService = new MediaTypeRepository();
                //long MediaTypeId = _iMediaTypeService.GetByTypeCode("MALL-2").MediaTypeId;//lấy hình kiểu 
                //MediaRepository _iMediaService = new MediaRepository();
                //Xử lí add ProductInMedia
                foreach (var item in lstStore)
                {
                    StoreObject sobj = new StoreObject();
                    sobj.StoreId = item.Store.StoreId;
                    sobj.StoreName = item.Store.StoreName;
                    sobj.StoreCode = item.Store.StoreCode;
                    sobj.Alias = item.Store.Alias;
                    sobj.MediaName = item.Media.MediaName;
                    sobj.Url = item.Media.Url;
                    sobj.DateVerified = (DateTime)item.Store.DateVerified;
                    sobj.Link = Url.Action("Index", "Store", new { id = sobj.StoreId, urlName = sobj.Alias });

                    lstStoreObj.Add(sobj);
                }

                //sắp xếp
                switch (_sortBy)
                {
                    case 0:
                        // Result = Utils.Sort(Result, n => n.StoreId, _order);
                        break;
                    case 1:
                        lstStoreObj = Utils.Sort(lstStoreObj, n => n.StoreName, _order);
                        break;
                    case 2:
                        lstStoreObj = Utils.Sort(lstStoreObj, n => n.DateVerified, _order);
                        break;
                }

                var Result = _step == -1 ? lstStoreObj.Take((int)_curCount).ToList() :
                    lstStoreObj.Skip(6).Skip(Convert.ToInt32(_step) * 4).Take(4).ToList();

                if (Error.Count == 0)
                {
                    return Json(new { IsSuccess = true, count = Result.Count, ListStoreObject = Result }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Error.Add("Error: " + ex.Message);
            }
            return Json(new { IsSuccess = false, error = Error }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetShippingFee(long? ProvinceId, string lstProduct)//xử lí ajax
        {
            List<string> error = new List<string>();
            if (ProvinceId == null || lstProduct == "")
            {
                error.Add(" Province is null.");
                Json(new { success = false, shippingfee = "0 đ", error = error }, JsonRequestBehavior.AllowGet);
            }
            if (Request.IsAjaxRequest())
            {
                ProductRepository _ProductRepository = new ProductRepository();
                try
                {
                    ResourceManager multiRes = new ResourceManager("HTTelecom.WebUI.eCommerce.Common.Lang", Assembly.GetExecutingAssembly());
                    var sessionObject = (SessionObject)Session["sessionObject"];
                    CultureInfo ci = new CultureInfo(sessionObject.lang);
                    string a = lstProduct;
                    dynamic data_lstProduct = Newtonsoft.Json.Linq.JObject.Parse(a);
                    List<Tuple<long, int>> lstProductId = new List<Tuple<long, int>>();
                    decimal totalPrice = 0;
                    foreach (var item in data_lstProduct.lstProduct)
                    {
                        long tmp_ProductId = 0;
                        int tmp_Quantity = 0;
                        int Quantity = int.TryParse(item.Quantity.ToString(), out tmp_Quantity) ? Convert.ToInt32(item.Quantity.ToString()) : 1;
                        if (long.TryParse(item.ProductId.ToString(), out tmp_ProductId))
                        {
                            var productItem = (Product)_ProductRepository.GetById(Convert.ToInt64(item.ProductId.ToString()));
                            double? price = productItem.PromotePrice != 0 ? productItem.PromotePrice : productItem.MobileOnlinePrice;
                            totalPrice += Convert.ToDecimal(price * Quantity);
                            lstProductId.Add(Tuple.Create(tmp_ProductId, Quantity));
                        }
                    }
                    ShipRepository _ShipRepository = new ShipRepository();
                    var ship = ProvinceId == null ? null : _ShipRepository.GetByTarget(Convert.ToInt64(ProvinceId), "2");
                    if (ship == null)
                        return Json(new
                        {
                            success = false,
                            shippingfee = string.Format("{0:0,0 đ}", 0),
                            error = "ship error"
                        }, JsonRequestBehavior.AllowGet);
                    Private.GetMutilLanguage(ViewBag, Session);
                    if (totalPrice >= ship.FreeShip)
                        return Json(new
                   {
                       success = true,
                       //Giá công kềnh
                       shippingfee = 0,
                       shippingfee_write = string.Format("{0:0,0 đ}", 0),
                       //Giá Ship duy nhất
                       ship = ship.Price,
                       ship_write = string.Format("{0:0,0 đ}", ship.Price),
                       //Giá ShipFree duy nhất
                       shipfree = ship.FreeShip,
                       shipfree_write = string.Format("{0:0,0 đ}", ship.FreeShip),
                       message = multiRes.GetString("alert_shipping_free_1", ViewBag.CultureInfo) + string.Format("{0:0,0 đ}", totalPrice) + "</br>" + multiRes.GetString("alert_shipping_free_2", ViewBag.CultureInfo),
                       //Ship + Cồng kềnh
                       total_ship = 0,
                       total_ship_write = string.Format("{0:0,0 đ}", 0),
                       //Giá + Ship + Cồng kềnh
                       totalPrice = totalPrice,
                       totalPrice_write = string.Format("{0:0,0 đ}", totalPrice),
                       error = error
                   }, JsonRequestBehavior.AllowGet);
                    WeightRepository _WeightRepository = new WeightRepository();
                    decimal shippingfee = _WeightRepository.GetShippingFee((long)ProvinceId, lstProductId);
                    //Phí giao hàng: 10000 đ (Phí giao hàng: ,Phí cồng kenh : )
                    //ViewBag.multiRes.GetString("addtocart", ViewBag.CultureInfo)
                    //"Phí giao hàng:" + (shippingfee+ ship.Price)
                    var ship_write = (ship.Price == null || ship.Price == 0) ? multiRes.GetString("alert_shipping_free_FREE", ViewBag.CultureInfo) : string.Format("{0:0,0 đ}", ship.Price);
                    return Json(new
               {
                   success = true,
                   //Giá công kềnh
                   shippingfee = shippingfee,
                   shippingfee_write = string.Format("{0:0,0 đ}", shippingfee),
                   //Giá ship duy nhất
                   ship = ship.Price,
                   ship_write = string.Format("{0:0,0 đ}", ship.Price),
                   //Giá ShipFree duy nhất
                   shipfree = ship.FreeShip,
                   shipfree_write = string.Format("{0:0,0 đ}", ship.FreeShip),
                   message = multiRes.GetString("alert_shipping_free_1", ViewBag.CultureInfo)
                   + string.Format("{0:0,0 đ}", totalPrice)
                   + "</br> " + multiRes.GetString("alert_shipping_free_transport_fee", ViewBag.CultureInfo)
                   + string.Format("{0:0,0 đ}", (ship.Price + shippingfee))
                   + " [ " + multiRes.GetString("alert_shipping_free_delivery_charges", ViewBag.CultureInfo) + ship_write
                   + ", " + multiRes.GetString("alert_shipping_free_free_bulky", ViewBag.CultureInfo) + string.Format("{0:0,0 đ}", shippingfee) + " ]",
                   // Ship + Cồng kềnh
                   total_ship = ship.Price + shippingfee,
                   total_ship_write = string.Format("{0:0,0 đ}", ship.Price + shippingfee),
                   //Giá + Ship + Cồng kềnh
                   totalPrice = totalPrice + shippingfee + ship.Price,
                   totalPrice_write = string.Format("{0:0,0 đ}", totalPrice + shippingfee + ship.Price),
                   error = error
               }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    error.Add("Error: " + ex.Message);
                }
                return Json(new
                {
                    success = false,
                    shippingfee = string.Format("{0:0,0 đ}", 0),
                    error = error
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
                shippingfee = string.Format("{0:0,0 đ}", 0),
                error = error
            }, JsonRequestBehavior.AllowGet);
        }


        #region for Vendor
        [HttpGet]
        public ActionResult GetStoreByVendor()
        {
            #region Statistic ProductAll
            ProductRepository _ProductRepository = new ProductRepository();
            var ListProduct = _ProductRepository.GetByStore(49)
                .GroupBy(n => n.GroupProductId)
                .Select(g => g.First())
                .OrderBy(n => n.DateCreated).ToList();
            var lstData = new List<object>();
            var data = new[] { 1, 2 };
            var rd = new Random();
            foreach (var item in ListProduct)
            {
                lstData.Add(new[] { Convert.ToDateTime(item.DateCreated).ToDateTimeUTC(), Convert.ToInt64(item.VisitCount) });
            }
            #endregion
            return Json(lstData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBrandByVendor()
        {
            #region Statistic ProductAll
            ProductRepository _ProductRepository = new ProductRepository();
            var ListProduct = _ProductRepository.GetByStore(49)
                .GroupBy(n => n.GroupProductId)
                .Select(g => g.First())
                .OrderBy(n => n.DateCreated).ToList();
            var lstData = new List<object>();
            var data = new[] { 1, 2 };
            var rd = new Random();
            foreach (var item in ListProduct)
            {
                lstData.Add(new[] { Convert.ToDateTime(item.DateCreated).ToDateTimeUTC(), Convert.ToInt64(item.VisitCount) });
            }
            #endregion
            return Json(lstData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductByVendor()
        {
            #region Statistic ProductAll
            ProductRepository _ProductRepository = new ProductRepository();
            var ListProduct = _ProductRepository.GetByStore(49)
                .GroupBy(n => n.GroupProductId)
                .Select(g => g.First())
                .OrderBy(n => n.DateCreated).ToList();
            var lstData = new List<object>();
            var data = new[] { 1, 2 };
            var rd = new Random();
            foreach (var item in ListProduct)
            {
                lstData.Add(new[] { Convert.ToDateTime(item.DateCreated).ToDateTimeUTC(), Convert.ToInt64(item.VisitCount) });
            }
            #endregion
            return Json(lstData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOrderByVendor()
        {
            #region Statistic ProductAll
            ProductRepository _ProductRepository = new ProductRepository();
            var ListProduct = _ProductRepository.GetByStore(49)
                .GroupBy(n => n.GroupProductId)
                .Select(g => g.First())
                .OrderBy(n => n.DateCreated).ToList();
            var lstData = new List<object>();
            var data = new[] { 1, 2 };
            var rd = new Random();
            foreach (var item in ListProduct)
            {
                lstData.Add(new[] { Convert.ToDateTime(item.DateCreated).ToDateTimeUTC(), Convert.ToInt64(item.VisitCount) });
            }
            #endregion
            return Json(lstData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLogisticByVendor()
        {
            #region Statistic ProductAll
            ProductRepository _ProductRepository = new ProductRepository();
            var ListProduct = _ProductRepository.GetByStore(49)
                .GroupBy(n => n.GroupProductId)
                .Select(g => g.First())
                .OrderBy(n => n.DateCreated).ToList();
            var lstData = new List<object>();
            var data = new[] { 1, 2 };
            var rd = new Random();
            foreach (var item in ListProduct)
            {
                lstData.Add(new[] { Convert.ToDateTime(item.DateCreated).ToDateTimeUTC(), Convert.ToInt64(item.VisitCount) });
            }
            #endregion
            return Json(lstData, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #endregion
    }
}
