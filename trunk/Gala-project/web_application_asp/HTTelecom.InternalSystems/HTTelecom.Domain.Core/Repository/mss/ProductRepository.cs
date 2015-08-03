using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.ExClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Globalization;
using PagedList;
using HTTelecom.Domain.Core.Repository.cis;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductRepository
    {
        public IPagedList<Product> GetList_ProductAll(int pageNum, int pageSize)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Product = _data.Product.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);


                    return lst_Product;
                }
                catch
                {
                    return new PagedList<Product>(new List<Product>(), 1, pageSize);
                }
            }
        }
        public IList<Product> GetList_ProductAll_ShowInStore(long storeId, bool ShowInStore)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Product.Where(a => a.StoreId == storeId && a.ShowInStorePage == ShowInStore).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IList<Product> GetList_ProductAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Product.OrderBy(a => a.DateCreated).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IPagedList<Product> GetList_Product_ProductTypeCode(string productTypeCode, int pageNum, int pageSize)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Product = _data.Product.Where(a => a.ProductTypeCode == productTypeCode)
                                                   .OrderBy(b => b.DateCreated)
                                                   .ToPagedList(pageNum, pageSize);

                    return lst_Product;
                }
                catch
                {
                    return new PagedList<Product>(new List<Product>(), 1, pageSize);
                }
            }
        }

        public IList<Product> GetList_Product_ProductTypeCode(string productTypeCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Product.Where(a => a.ProductTypeCode == productTypeCode).OrderBy(b => b.ProductName).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IPagedList<Product> GetList_Product_StoreId(long storeId, int pageNum, int pageSize)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Product = _data.Product.Where(a => a.StoreId == storeId).OrderBy(b => b.DateCreated).ToPagedList(pageNum, pageSize);

                    return lst_Product;
                }
                catch
                {
                    return new PagedList<Product>(new List<Product>(), 1, pageSize);
                }
            }
        }

        public IPagedList<Product> GetList_Product_WishList_CustomerId(long CustomerId, int pageNum, int pageSize, string ProductNameOrder, string DateOrder)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                ProductNameOrder = ProductNameOrder ?? "";
                ProductNameOrder = ProductNameOrder == "DESC" || ProductNameOrder == "ACS" || ProductNameOrder == "" ? ProductNameOrder : "ACS";

                DateOrder = DateOrder ?? "";
                DateOrder = DateOrder == "DESC" || DateOrder == "ACS" || DateOrder == "" ? DateOrder : "ACS";
                WishlistRepository _iWishlistsService = new WishlistRepository();
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;

                List<Product> lst_Product = new List<Product>();
                List<Wishlist> lst_wishlist = _iWishlistsService.GetAll(false, true).Where(w => w.CustomerId == CustomerId).ToList();

                try
                {
                    foreach (Wishlist item in lst_wishlist)
                    {
                        lst_Product.Add(_data.Product.Find(item.ProductId));
                    }

                    if (ProductNameOrder == "DESC")
                    {
                        return lst_Product.OrderByDescending(b => b.ProductName).ToPagedList(pageNum, pageSize);
                    }
                    if (ProductNameOrder == "ACS")
                    {
                        return lst_Product.OrderBy(b => b.ProductName).ToPagedList(pageNum, pageSize);
                    }

                    if (DateOrder == "DESC")
                    {
                        return lst_Product.OrderByDescending(b => b.DateCreated).ToPagedList(pageNum, pageSize);
                    }
                    if (DateOrder == "ACS")
                    {
                        return lst_Product.OrderBy(b => b.DateCreated).ToPagedList(pageNum, pageSize);
                    }

                    return lst_Product.OrderBy(b => b.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<Product>(new List<Product>(), 1, pageSize);
                }
            }
        }

        public List<Product> GetList_Product_CustomerId(long CustomerId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                WishlistRepository _iWishlistsService = new WishlistRepository();
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;

                List<Product> lst_Product = new List<Product>();
                List<Wishlist> lst_wishlist = _iWishlistsService.GetAll(false, true).Where(w => w.CustomerId == CustomerId).ToList();

                try
                {
                    foreach (Wishlist item in lst_wishlist)
                    {
                        lst_Product.Add(_data.Product.Find(item.ProductId));
                    }

                    return lst_Product.OrderBy(b => b.DateCreated).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IList<Product> GetList_Product_StoreId(long storeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Product.Where(a => a.StoreId == storeId).OrderBy(b => b.DateCreated).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IPagedList<Product> GetList_Product_StoreId_ProductTypeCode(long storeId, string productTypeCode, int pageNum, int pageSize)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Product
                        .Where(a => a.StoreId == storeId)
                        .Where(c => c.ProductTypeCode == productTypeCode)
                        .OrderBy(b => b.DateCreated).ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<Product>(new List<Product>(), 1, pageSize);
                }
            }
        }

        public IList<Product> GetList_Product_StoreId_ProductTypeCode(long storeId, string productTypeCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Product
                        .Where(a => a.StoreId == storeId)
                        .Where(c => c.ProductTypeCode == productTypeCode)
                        .OrderBy(b => b.ProductName).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IList<Product> GetList_ProductAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Product.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public IList<Product> GetList_ProductAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Product.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }
        public IList<Product> GetList_ProductAll_IsVerified(bool IsVerified)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Product.Where(a => a.IsVerified == IsVerified).ToList();
                }
                catch
                {
                    return new List<Product>();
                }
            }
        }

        public Product Get_ProductById(long ProductId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Product.Find(ProductId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public Product Get_ProductByStockCode(string ProductStockCode)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    return _data.Product.Where(a => a.ProductStockCode == ProductStockCode).FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }

        }

        public long Insert(Product Product)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Product.Alias = Generates.generateAlias(Product.ProductName);
                    Product.DateCreated = DateTime.Now;
                    Product.VisitCount = 0;
                    Product.Size = Product.Size ?? "52"; // OneSize
                    Product.SizeGlobal = Product.SizeGlobal ?? "5"; // SizeGlobal of OneSize
                    _data.Product.Add(Product);
                    _data.SaveChanges();
                    Product.GroupProductId = Product.GroupProductId == null ? Product.ProductId : Product.GroupProductId;
                    _data.SaveChanges();
                    return Product.ProductId;
                }
                catch
                {
                    return -1;
                }

            }
        }
        public bool UpdateVerified(long productId, bool isVerified)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Product product = _data.Product.Where(x => x.ProductId == productId).FirstOrDefault();

                    if (isVerified == true)
                    {
                        product.DateVerified = DateTime.Now;
                    }
                    product.IsVerified = isVerified;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public Tuple<bool, string> UpdateActive(long productId, bool isActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Product product = _data.Product.Where(x => x.ProductId == productId).FirstOrDefault();

                    product.IsActive = isActive;
                    // Edit by Vannl 31/07/2015
                    if (product.ProductCode == null)
                    {
                        product.ProductCode = (int.Parse(_data.Product.Max(x => x.ProductCode)) + 1).ToString("00000000");
                    }
                    _data.SaveChanges();
                    return new Tuple<bool, string>(true, product.ProductCode);
                }
                catch { return new Tuple<bool, string>(false, String.Empty); }
            }
        }

        public bool Update(Product Product)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Product ProductToUpdate;
                    ProductToUpdate = _data.Product.Where(x => x.ProductId == Product.ProductId).FirstOrDefault();
                    ProductToUpdate.StoreId = Product.StoreId ?? ProductToUpdate.StoreId;
                    // Edit by Vannl 31/07/2015
                    if (ProductToUpdate.IsActive == true && Product.ProductCode == null)
                    {
                        ProductToUpdate.ProductCode = (int.Parse(_data.Product.Max(x => x.ProductCode)) + 1).ToString("00000000");
                    }
                    ProductToUpdate.BrandId = Product.BrandId ?? ProductToUpdate.BrandId;
                    ProductToUpdate.ProductStatusCode = Product.ProductStatusCode ?? ProductToUpdate.ProductStatusCode;
                    ProductToUpdate.ProductTypeCode = Product.ProductTypeCode ?? ProductToUpdate.ProductTypeCode;
                    //ProductToUpdate.ProductStockCode = Product.ProductStockCode;
                    ProductToUpdate.ProductName = Product.ProductName ?? ProductToUpdate.ProductName;
                    ProductToUpdate.Alias = Generates.generateAlias(Product.ProductName) ?? ProductToUpdate.Alias;

                    ProductToUpdate.ProductComplexName = Product.ProductComplexName ?? ProductToUpdate.ProductComplexName;
                    ProductToUpdate.Keywords = Product.Keywords ?? ProductToUpdate.Keywords;
                    ProductToUpdate.RetailPrice = Product.RetailPrice ?? ProductToUpdate.RetailPrice;
                    ProductToUpdate.PromotePrice = Product.PromotePrice ?? ProductToUpdate.PromotePrice;
                    ProductToUpdate.MobileOnlinePrice = Product.MobileOnlinePrice ?? ProductToUpdate.MobileOnlinePrice;
                    ProductToUpdate.ProductOutLine = Product.ProductOutLine;
                    ProductToUpdate.ProductSpecification = Product.ProductSpecification;
                    ProductToUpdate.ProductDetail = Product.ProductDetail;
                    ProductToUpdate.ProductTermService = Product.ProductTermService;
                    ProductToUpdate.ShowInStorePage = Product.ShowInStorePage ?? ProductToUpdate.ShowInStorePage;
                    ProductToUpdate.MetaTitle = Product.MetaTitle ?? ProductToUpdate.MetaTitle;
                    ProductToUpdate.MetaKeywords = Product.MetaKeywords ?? ProductToUpdate.MetaKeywords;
                    ProductToUpdate.MetaDescription = Product.MetaDescription ?? ProductToUpdate.MetaDescription;

                    ProductToUpdate.SizeGlobal = Product.SizeGlobal;
                    ProductToUpdate.Size = Product.Size;
                    ProductToUpdate.Colour = Product.Colour;
                    ProductToUpdate.GroupProductId = Product.GroupProductId;
                    ProductToUpdate.IsWeight = Product.IsWeight;
                    ProductToUpdate.Weight = Product.IsWeight != null && Product.IsWeight == true ? Product.Weight : 0;

                    ProductToUpdate.DateModified = DateTime.Now;
                    if (Product.IsVerified == true)
                    {
                        ProductToUpdate.DateVerified = Product.DateVerified;
                    }
                    ProductToUpdate.ModifiedBy = Product.ModifiedBy ?? ProductToUpdate.ModifiedBy;
                    ProductToUpdate.IsVerified = Product.IsVerified ?? ProductToUpdate.IsVerified;
                    ProductToUpdate.IsActive = Product.IsActive ?? ProductToUpdate.IsActive;
                    ProductToUpdate.IsDeleted = Product.IsDeleted ?? ProductToUpdate.IsDeleted;

                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IList<Product> SearchProduct(string keywords)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    IList<Product> lst_Product = new List<Product>();
                    //if (searchProductId == true)
                    //{
                    //    string wordASCII = Generates.ConvertUnicodeToASCII(keywords).ToLower();
                    //    var querylst_Product = _data.Product.ToList();
                    //    lst_Product = querylst_Product.FindAll(
                    //        delegate(Product product)
                    //        {
                    //            string productCode = "p" + product.ProductCode.ToLower();
                    //            if (Generates.ConvertUnicodeToASCII(productCode).Contains(wordASCII) || Generates.ConvertUnicodeToASCII(product.ProductStockCode.ToLower()).Contains(wordASCII))
                    //                return true;
                    //            else
                    //                return false;
                    //        }
                    //    );
                    //}
                    //else if (searchStockCode == true)
                    //{
                    //    string wordASCII = Generates.ConvertUnicodeToASCII(keywords).ToLower();
                    //    var querylst_Product = _data.Product.ToList();
                    //    lst_Product = querylst_Product.FindAll(
                    //        delegate(Product product)
                    //        {
                    //            string stockCode = product.ProductStockCode.ToLower();
                    //            if (Generates.ConvertUnicodeToASCII(stockCode).Contains(wordASCII))
                    //                return true;
                    //            else
                    //                return false;
                    //        }
                    //    );
                    //}
                    //else
                    //{
                    string wordASCII = Generates.ConvertUnicodeToASCII(keywords).ToLower();
                    var querylst_Product = _data.Product.ToList();
                    lst_Product = querylst_Product.FindAll(
                        delegate(Product product)
                        {
                            string productCode = "p" + product.ProductCode.ToLower();
                            if (Generates.ConvertUnicodeToASCII(product.ProductName.ToLower()).Contains(wordASCII) || Generates.ConvertUnicodeToASCII(productCode).Contains(wordASCII) || Generates.ConvertUnicodeToASCII(product.ProductStockCode.ToLower()).Contains(wordASCII))
                                return true;
                            else
                                return false;
                        }
                    );
                    //}
                    return lst_Product;
                }
                catch { return new List<Product>(); }
            }
        }

        public bool GetStoreClose(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                var toDay = DateTime.Now;
                var rs =
                    (from a in _data.Store
                     join b in _data.Product
                     on a.StoreId equals b.StoreId
                     where b.ProductId == id
                     select a).FirstOrDefault();
                if (rs.IsVerified == true && rs.IsDeleted == false && rs.OnlineDate != null && rs.OfflineDate != null && (toDay - rs.OnlineDate.Value).TotalMinutes >= 0 && (rs.OfflineDate.Value - toDay).TotalMinutes >= 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }

        public Product GetById(long keyword)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Product.Find(keyword);
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetListProductByListId(List<long> lst)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                List<Product> lstProduct = new List<Product>();
                foreach (var item in lst)
                {
                    lstProduct.Add(_data.Product.Find(item));
                }
                return lstProduct;
            }
            catch
            {
                return new List<Product>();
            }
        }

        public IPagedList<Product> GetList_Product_WishList_CustomerId_ProductType(long CustomerId, string ptype, int pageNum, int pageSize, string ProductNameOrder, string DateOrder)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                ProductNameOrder = ProductNameOrder ?? "DECS";
                ProductNameOrder = ProductNameOrder == "DECS" || ProductNameOrder == "ACS" ? ProductNameOrder : "DESC";

                DateOrder = DateOrder ?? "";
                DateOrder = DateOrder == "DESC" || DateOrder == "ACS" ? DateOrder : "ACS";
                WishlistRepository _iWishlistsService = new WishlistRepository();
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;

                List<Product> lst_Product = new List<Product>();
                List<Wishlist> lst_wishlist = _iWishlistsService.GetAll(false, true).Where(w => w.CustomerId == CustomerId).ToList();

                try
                {
                    foreach (Wishlist item in lst_wishlist)
                    {
                        lst_Product.Add(_data.Product.Find(item.ProductId));
                    }

                    if (ProductNameOrder == "DESC")
                    {
                        return lst_Product.Where(p => p.ProductTypeCode == ptype).OrderByDescending(b => b.ProductName).ToPagedList(pageNum, pageSize);
                    }
                    else
                    {
                        return lst_Product.Where(p => p.ProductTypeCode == ptype).OrderBy(b => b.ProductName).ToPagedList(pageNum, pageSize);
                    }

                    if (DateOrder == "DESC")
                    {
                        return lst_Product.Where(p => p.ProductTypeCode == ptype).OrderByDescending(b => b.DateCreated).ToPagedList(pageNum, pageSize);
                    }
                    if (DateOrder == "ACS")
                    {
                        return lst_Product.Where(p => p.ProductTypeCode == ptype).OrderBy(b => b.DateCreated).ToPagedList(pageNum, pageSize);
                    }

                    return lst_Product.Where(p => p.ProductTypeCode == ptype).OrderBy(b => b.DateCreated).ToPagedList(pageNum, pageSize);

                }
                catch
                {
                    return new PagedList<Product>(new List<Product>(), 1, pageSize);
                }
            }
        }
    }
}
