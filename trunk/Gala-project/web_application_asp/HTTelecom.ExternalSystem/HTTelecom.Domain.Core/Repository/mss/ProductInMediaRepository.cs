using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ProductInMediaRepository
    {
        public List<ProductInMedia> GetAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.ProductInMedia.ToList();
            }
        }

        public List<ProductInMedia> GetByHome()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var toDay = DateTime.Now;
                var lst = _data.ProductInMedia.Where(n =>
                    n.Product.Store.IsVerified == true && n.Product.IsVerified == true
                    && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
                    && n.Product.Store.IsActive == true && n.Product.IsActive == true
                    && n.Product.Store.OnlineDate.HasValue == true
                     && n.Product.Store.OfflineDate.HasValue == true
                     && n.Media.MediaType.MediaTypeCode == "STORE-3"
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).Include(n => n.Media.MediaType).Include(n => n.Media).Include(n => n.Product).Include(n => n.Product.Store).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
        }


        #region app
        public List<ProductInMedia> GetByHomeApp()
        {
            MSS_DBEntities _data = new MSS_DBEntities();
            {
                var toDay = DateTime.Now;
                var lst = _data.ProductInMedia.Where(n =>
                    n.Product.Store.IsVerified == true && n.Product.IsVerified == true
                    && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
                    && n.Product.Store.IsActive == true && n.Product.IsActive == true
                    && n.Product.Store.OnlineDate.HasValue == true
                     && n.Product.Store.OfflineDate.HasValue == true
                     && n.Media.MediaType.MediaTypeCode == "STORE-3"
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).Include(n => n.Media.MediaType).Include(n => n.Media).Include(n => n.Product).Include(n => n.Product.Store).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
        }
        #endregion

        public List<ProductInMedia> GetByProduct(long ProductId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.ProductInMedia.Where(n => n.ProductId == ProductId).Include(n => n.Media.MediaType).Include(n => n.Media).Include(n => n.Product).Include(n => n.Product.Store).ToList();
            }
        }

        public List<ProductInMedia> GetByHaveBrand()
        {
            var toDay = DateTime.Now;
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var lst = _data.ProductInMedia.Where(n => n.Product.BrandId != null && n.Product.BrandId > 0 && n.Product.IsActive == true && n.Product.IsVerified == true && n.Product.IsDeleted == false && n.Product.Store.IsActive == true && n.Product.Store.IsDeleted == false && n.Product.Store.IsVerified == true && n.Product.Store.OnlineDate.HasValue == true && n.Product.Store.OfflineDate.HasValue == true && n.Media.IsActive == true && n.Media.IsDeleted == false && n.Media.MediaType.MediaTypeCode == "STORE-3").ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                return lst;
            }
        }

        internal bool ProductError(Product model)
        {
            List<string> lstError = new List<string>();
            var error = false;
            StoreRepository _StoreRepository = new StoreRepository();
            if (model == null || _StoreRepository.CheckStoreOnline(Convert.ToInt64(model.StoreId)) == false || model.IsVerified == false || model.IsDeleted == true || model.IsActive == false)
                error = true;
            return error;
        }

        public List<ProductInMedia> GetByAvaiable()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var toDay = DateTime.Now;
                var lst = _data.ProductInMedia.Where(n =>
                    n.Product.Store.IsVerified == true && n.Product.IsVerified == true
                    && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
                    && n.Product.Store.IsActive == true && n.Product.IsActive == true
                    && n.Product.Store.OnlineDate.HasValue == true
                     && n.Product.Store.OfflineDate.HasValue == true
                     && n.Media.MediaType.MediaTypeCode == "STORE-3"
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).Include(n => n.Media.MediaType).Include(n => n.Media).Include(n => n.Product).Include(n => n.Product.Store).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
        }

        public List<ProductInMedia> GetBySale()
        {
            var toDay = DateTime.Now;
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var lst = _data.ProductInMedia.Where(n =>
               n.Product.Store.IsVerified == true && n.Product.IsVerified == true
               && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
               && n.Product.Store.IsActive == true && n.Product.IsActive == true
               && n.Product.Store.OnlineDate.HasValue == true
                && n.Product.Store.OfflineDate.HasValue == true
                && n.Media.MediaType.MediaTypeCode == "STORE-3"
                && n.Media.IsActive == true && n.Media.IsDeleted == false
               )
               .Include(n => n.Media.MediaType).Include(n => n.Media).Include(n => n.Product).Include(n => n.Product.Store).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
        }

        public List<ProductInMedia> GetByGroup(long GroupProductId, string MediaTypeCode)
        {
            var toDay = DateTime.Now;
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var lst = _data.ProductInMedia.Where(n =>
               n.Product.Store.IsVerified == true && n.Product.IsVerified == true
               && n.Product.Store.IsDeleted == false && n.Product.IsDeleted == false
               && n.Product.Store.IsActive == true && n.Product.IsActive == true
               && n.Product.GroupProductId == GroupProductId
               && n.Product.Store.OnlineDate.HasValue == true
                && n.Product.Store.OfflineDate.HasValue == true
                && n.Media.MediaType.MediaTypeCode == MediaTypeCode
                && n.Media.IsActive == true && n.Media.IsDeleted == false
               ).Include(m => m.Media.MediaType).Include(m => m.Media).Include(m => m.Product).Include(m => m.Product.Store).ToList();
                lst = lst.Where(n => (toDay - n.Product.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Product.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                lst = lst.GroupBy(n => n.Product.GroupProductId).Select(n => n.First()).ToList();
                return lst;
            }
        }
    }
}
