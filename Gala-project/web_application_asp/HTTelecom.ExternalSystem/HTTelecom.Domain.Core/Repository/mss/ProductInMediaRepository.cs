using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInMediaRepository
    {
        public List<ProductInMedia> GetAll()
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.ProductInMedia.ToList();
            }
            catch
            {
                return new List<ProductInMedia>();
            }
        }

        public List<ProductInMedia> GetByHome()
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.ProductInMedia.Where(n =>
                    n.Product.Store.IsVerified == true && n.Product.IsVerified == true
                    && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
                    && n.Product.Store.IsActive == true && n.Product.IsActive == true
                    && n.Product.Store.OnlineDate.HasValue == true
                     && n.Product.Store.OfflineDate.HasValue == true
                     && n.Media.MediaType.MediaTypeCode == "STORE-3"
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
            catch
            {
                return new List<ProductInMedia>();
            }
        }

        public List<ProductInMedia> GetByProduct(long ProductId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.ProductInMedia.Where(n => n.ProductId == ProductId).ToList();
            }
            catch
            {
                return new List<ProductInMedia>();
            }
        }

        public List<ProductInMedia> GetByHaveBrand()
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.ProductInMedia.Where(n => n.Product.BrandId != null && n.Product.BrandId > 0 && n.Product.IsActive == true && n.Product.IsVerified == true && n.Product.IsDeleted == false && n.Product.Store.IsActive == true && n.Product.Store.IsDeleted == false && n.Product.Store.IsVerified == true && n.Product.Store.OnlineDate.HasValue == true && n.Product.Store.OfflineDate.HasValue == true && n.Media.IsActive == true && n.Media.IsDeleted == false && n.Media.MediaType.MediaTypeCode == "STORE-3").ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                return lst;
            }
            catch
            {
                return new List<ProductInMedia>();
            }
        }

        internal bool ProductError(Product model)
        {
            try
            {
                List<string> lstError = new List<string>();
                var error = false;
                StoreRepository _StoreRepository = new StoreRepository();
                if (model == null || _StoreRepository.CheckStoreOnline(Convert.ToInt64(model.StoreId)) == false)
                {
                    error = true;
                }
                if (model.IsVerified == false || model.IsDeleted == true || model.IsActive == false)
                {
                    error = true;
                }
                return error;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public List<ProductInMedia> GetByAvaiable()
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.ProductInMedia.Where(n =>
                    n.Product.Store.IsVerified == true && n.Product.IsVerified == true
                    && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
                    && n.Product.Store.IsActive == true && n.Product.IsActive == true
                    && n.Product.Store.OnlineDate.HasValue == true
                     && n.Product.Store.OfflineDate.HasValue == true
                     && n.Media.MediaType.MediaTypeCode == "STORE-3"
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
            catch
            {
                return new List<ProductInMedia>();
            }
        }

        public List<ProductInMedia> GetBySale()
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.ProductInMedia.Where(n =>
                    n.Product.Store.IsVerified == true && n.Product.IsVerified == true
                    && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
                    && n.Product.Store.IsActive == true && n.Product.IsActive == true
                    && n.Product.Store.OnlineDate.HasValue == true
                     && n.Product.Store.OfflineDate.HasValue == true
                     && n.Media.MediaType.MediaTypeCode == "STORE-3"
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
            catch
            {
                return new List<ProductInMedia>();
            }
        }
    }
}
