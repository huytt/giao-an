using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Entity;
namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductRepository
    {
        public List<Product> GetListByCategory(long CategoryId)
        {
            var toDay = DateTime.Now;
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var lstProductInCategory = _data.ProductInCategory.Where(n => n.CategoryId == CategoryId && n.IsDeleted == false && n.IsActive == true).ToList();
                var lstPr = new List<long>();
                foreach (var item in lstProductInCategory)
                    lstPr.Add(Convert.ToInt64(item.ProductId));
                var lst = (from a in _data.Product
                           join b in _data.Store
                           on a.StoreId equals b.StoreId
                           where
                          b.IsVerified == true && b.IsDeleted == false && b.IsActive == true && b.OnlineDate.HasValue == true && b.OfflineDate.HasValue == true
                          && (a.IsActive == true && a.IsDeleted == false && a.IsVerified == true && lstPr.Contains(a.ProductId))
                           select a).ToList();
                lst = lst.Where(n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).GroupBy(n => n.GroupProductId).Select(n => n.First()).ToList();
                ProductInMediaRepository _ProductInMediaRepository = new ProductInMediaRepository();
                var List = new List<Product>();
                foreach (var item in lst)
                {
                    if (item.GroupProductId != null)
                    {
                        var productInMedia = _ProductInMediaRepository.GetByGroup(Convert.ToInt64(item.GroupProductId), "STORE-3").FirstOrDefault();
                        if (productInMedia != null)
                        {
                            List.Add(item);
                        }
                    }

                }
                return List;
            }
        }
        public List<Product> GetByStore(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var lst = _data.Product.Where(n => n.StoreId == id && n.IsDeleted == false && n.IsVerified == true && n.IsActive == true).ToList();
                lst = lst.GroupBy(n => n.GroupProductId).Select(g => g.First()).ToList();
                return lst;
            }

        }
        public Product GetById(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Product.Include(n => n.Store).Where(n => n.ProductId == id).FirstOrDefault();
            }

        }
        public List<Product> GetBySearch(long cate, long brand, string q)
        {
            CategoryRepository _CategoryRepository = new CategoryRepository();
            List<Category> lstcate = new List<Category>();
            if (cate > 0)
                lstcate = _CategoryRepository.GetListChildrenCategoryByCategoryId(cate).OrderBy(n => n.OrderNumber).ToList();
            List<long?> LstCategory = new List<long?>();
            LstCategory.Add(cate);

            string category = String.Empty;

            foreach (var item in lstcate)
            {
                LstCategory.Add(item.CategoryId);
                category += item.CategoryId.ToString() + " ";
            }
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var data_rs = _data.spSearchProductPriority(q, brand, category.Trim()).ToList();
                var lst = new List<Product>();
                var toDay = DateTime.Now;
                //n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0
                foreach (var item in data_rs)
                {
                    if ((toDay - item.OnlineDate.Value).TotalMinutes >= 0 && (item.OfflineDate.Value - toDay).TotalMinutes >= 0)
                        lst.Add(new Product() { ProductId = item.ProductId });
                }

                #region Remove
                //if (cate == 0)
                //    if (brand == 0)
                //    {
                //        if (q == "")
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true
                //                   select b).ToList();
                //        else
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true
                //                   && (b.ProductName.ToUpper().IndexOf(q.ToUpper()) >= 0 || b.ProductComplexName.ToUpper().Contains(q.ToUpper()) || b.Alias.ToUpper().Contains(q.ToUpper()))
                //                   select b).ToList();
                //    }
                //    else
                //    {
                //        if (q == "")
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true && b.BrandId == brand
                //                   select b).ToList();
                //        else
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true && b.BrandId == brand
                //                   && (b.ProductName.ToUpper().IndexOf(q.ToUpper()) >= 0 || b.ProductComplexName.ToUpper().Contains(q.ToUpper()) || b.Alias.ToUpper().Contains(q.ToUpper()))
                //                   select b).ToList();
                //    }
                //else
                //    if (brand == 0)
                //    {
                //        if (q == "")
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true
                //                   && LstCategory.Contains(c.CategoryId)
                //                   select b).ToList();
                //        else
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true
                //                   && (b.ProductName.ToUpper().Contains(q.ToUpper()) || b.ProductComplexName.ToUpper().Contains(q.ToUpper()) || b.Alias.ToUpper().Contains(q.ToUpper())) && LstCategory.Contains(c.CategoryId)
                //                   select b).ToList();
                //    }
                //    else
                //    {
                //        if (q == "")
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true
                //                   && LstCategory.Contains(c.CategoryId) && b.BrandId == brand
                //                   select b).ToList();
                //        else
                //            lst = (from a in _data.Store
                //                   join b in _data.Product
                //                   on a.StoreId equals b.StoreId
                //                   join c in _data.ProductInCategory
                //                   on b.ProductId equals c.ProductId
                //                   where a.IsVerified == true && a.IsDeleted == false && a.IsActive == true && a.OnlineDate.HasValue == true && a.OfflineDate.HasValue == true && b.IsActive == true && b.IsDeleted == false && b.IsVerified == true && b.BrandId == brand
                //                   && (b.ProductName.ToUpper().Contains(q.ToUpper()) || b.ProductComplexName.ToUpper().Contains(q.ToUpper()) || b.Alias.ToUpper().Contains(q.ToUpper())) && LstCategory.Contains(c.CategoryId)
                //                   select b).ToList();
                //    }

                //lst = lst.Where(n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                #endregion
                return lst;
            }
        }
        public void VisitCount(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var item = _data.Product.Find(id);
                item.VisitCount = item.VisitCount == null ? 1 : item.VisitCount + 1;
                _data.SaveChanges();
            }
        }
        public List<Product> GetByBrand(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var lst = _data.Product.Where(n => n.BrandId == id && n.IsActive == true && n.IsDeleted == false && n.Store.IsActive == true && n.Store.IsDeleted == false).ToList();
                return lst;

            }
        }
        public Product GetByCode(string code)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Product.Where(n => n.ProductCode == code).Include(n => n.Store).FirstOrDefault();
            }

        }
        public Product GetByStockCode(string StockCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Product.Where(n => n.ProductStockCode == StockCode).Include(n => n.Store).FirstOrDefault();
            }
        }
        public List<Product> GetColour(long GroupProductId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Product.Where(n => n.GroupProductId == GroupProductId).Include(n => n.Store).ToList();
            }
        }

        public List<Tuple<long,long, long,int>> GetListProductPaidAndTotalCountByVendorId(long VendorId)
        {
            using(OPS_DBEntities _data = new OPS_DBEntities())
            {
                ProductRepository _iProductService = new ProductRepository();
                List<Tuple<long, long, long, int>> result = new List<Tuple<long, long, long, int>>();
                try
                {                   
                    var tmp = _data.sp_GetProductListPaidAndTotalByVendor(VendorId);
                    //var tmp = _data.sp_GetProductListPaidAndTotalByStore(45);
                    foreach (var item in tmp)
                    {
                        result.Add(new Tuple<long, long, long, int>( (long)item.ProductId,(long)0, (long)item.StoreId, (int)item.TotalCount));
                    }
                    return result;
                }
                catch
                {
                    return null;
                }
              
            }
           
        }
    }
}
