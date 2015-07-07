using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class StoreInMediaRepository
    {
        public List<StoreInMedia> GetAll()
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.StoreInMedia.ToList();
            }
            catch
            {
                return new List<StoreInMedia>();
            }
        }
        public List<StoreInMedia> GetByHome()
        {
            try
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
                    ).ToList();
                    var data = lst.Where(
                        n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0
                            && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0
                            ).ToList();
                    //data = lst;
                    return data;
                }
            }
            catch
            {
                return new List<StoreInMedia>();
            }
        }
        public List<StoreInMedia> GetAllStore()
        {
            try
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
                     && n.Media.IsActive == true && n.Media.IsDeleted == false
                    ).ToList();
                    //var data = lst.Where(n => (toDay - n.Store.OnlineDate.Value).TotalMinutes >= 0 && (n.Store.OfflineDate.Value - toDay).TotalMinutes >= 0).ToList();
                    var data = lst;
                    return data;
                }
            }
            catch
            {
                return new List<StoreInMedia>();
            }
        }

        public List<StoreInMedia> GetByStoreId(long id)
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.StoreInMedia.Where(n => n.StoreId == id).ToList();
                return lst;
            }
            catch
            {
                return new List<StoreInMedia>();
            }
        }
    }
}
