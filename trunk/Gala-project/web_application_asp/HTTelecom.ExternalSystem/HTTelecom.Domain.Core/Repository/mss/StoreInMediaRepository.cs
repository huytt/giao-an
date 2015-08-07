using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
namespace HTTelecom.Domain.Core.Repository.mss
{
    public class StoreInMediaRepository
    {
        public List<StoreInMedia> GetAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.StoreInMedia.ToList();
            }
        }
        public List<StoreInMedia> GetByHome()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var toDay = DateTime.Now;
                var lst = _data.StoreInMedia.Where(n => n.Store.IsVerified == true
                && n.Store.IsDeleted == false
                && n.Store.IsActive == true
                && n.Store.OnlineDate.HasValue == true
                 && n.Store.OfflineDate.HasValue == true
                 && n.Media.MediaType.MediaTypeCode == "MALL-2"
                 && n.Store.ShowInMallPage == true
                 && n.Media.IsActive == true && n.Media.IsDeleted == false
                ).Include(n => n.Store).Include(n => n.Media).ToList();
                var data = lst.Where(
                    n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0
                        && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0
                        ).ToList();
                //data = lst;
                return data;
            }
        }

        #region forApp
        public List<StoreInMedia> GetByHomeApp()
        {
            MSS_DBEntities _data = new MSS_DBEntities();
            {
                var toDay = DateTime.Now;
                var lst = _data.StoreInMedia.Where(n => n.Store.IsVerified == true
                && n.Store.IsDeleted == false
                && n.Store.IsActive == true
                && n.Store.OnlineDate.HasValue == true
                 && n.Store.OfflineDate.HasValue == true
                 && n.Media.MediaType.MediaTypeCode == "MALL-2"
                 && n.Store.ShowInMallPage == true
                 && n.Media.IsActive == true && n.Media.IsDeleted == false
                ).Include(n => n.Store).Include(n => n.Media).ToList();
                var data = lst.Where(
                    n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0
                        && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0
                        ).ToList();
                //data = lst;
                return data;
            }
        }
        #endregion
        public List<StoreInMedia> GetByBannerMall()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var toDay = DateTime.Now;
                var lst = _data.StoreInMedia.Where(
                    n => n.Store.IsVerified == true
                && n.Store.IsDeleted == false
                && n.Store.IsActive == true
                && n.Store.OnlineDate.HasValue == true
                 && n.Store.OfflineDate.HasValue == true
                 && n.Media.MediaType.MediaTypeCode == "MALL-2"
                 && n.Store.ShowInBannerMall != null
                 && n.Store.ShowInBannerMall == true
                 && n.Media.IsActive == true && n.Media.IsDeleted == false
                ).Include(n => n.Media).Include(n => n.Store).ToList();
                var data = lst.Where(
                    n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0
                        && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0
                        ).ToList();
                //data = lst;
                return data;
            }
        }
        public List<StoreInMedia> GetAllStore()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var toDay = DateTime.Now;
                var lst = _data.StoreInMedia.Where(n => n.Store.IsVerified == true
                && n.Store.IsDeleted == false
                && n.Store.IsActive == true
                && n.Store.OnlineDate.HasValue == true
                 && n.Store.OfflineDate.HasValue == true
                 && n.Media.MediaType.MediaTypeCode == "MALL-2"
                 && n.Media.IsActive == true && n.Media.IsDeleted == false
                ).Include(n => n.Media).Include(n => n.Store).ToList();
                //var data = lst.Where(n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                var data = lst;
                return data;
            }
        }

        public List<StoreInMedia> GetByStoreId(long id)
        {
            var toDay = DateTime.Now;
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.StoreInMedia.Where(n => n.StoreId == id).Include(n => n.Media).Include(n => n.Store).Include(n => n.Media.MediaType).ToList();
            }
        }
    }
}
