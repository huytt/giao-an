using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductRepository
    {
        public List<Product> GetListByCategory(long CategoryId)
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
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
                lst = lst.Where(n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                return lst.ToList();
            }
            catch
            {
                return new List<Product>();
            }
        }

        public List<Product> GetByStore(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.Product.Where(n => n.StoreId == id && n.IsDeleted == false && n.IsVerified == true && n.IsActive == true).ToList();
                lst = lst.GroupBy(n => n.GroupProductId).Select(g => g.First()).ToList();
                return lst;
            }
            catch
            {
                return new List<Product>();
            }
        }

        public Product GetById(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Product.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetBySearch(long cate, long brand, string q)
        {
            try
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


                MSS_DBEntities _data = new MSS_DBEntities();

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
            catch
            {
                return new List<Product>();
            }
        }

        public void VisitCount(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                var item = _data.Product.Find(id);
                item.VisitCount = item.VisitCount == null ? 1 : item.VisitCount + 1;
                _data.SaveChanges();
            }
            catch
            { }
        }

        public List<Product> GetByBrand(long id)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    var lst = _data.Product.Where(n => n.BrandId == id && n.IsActive == true && n.IsDeleted == false && n.Store.IsActive == true && n.Store.IsDeleted == false).ToList();
                    return lst;

                }
            }
            catch
            {
                return new List<Product>();
            }
        }
        public Product GetByCode(string code)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Product.Where(n => n.ProductCode == code).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public Product GetByStockCode(string StockCode)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Product.Where(n => n.ProductStockCode == StockCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Product> GetColour(long GroupProductId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Product.Where(n => n.GroupProductId == GroupProductId).ToList();
            }
            catch
            {
                return new List<Product>();
            }
        }
    }
}
