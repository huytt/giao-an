using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class StoreInMediaRepository
    {
        public IList<StoreInMedia> GetList_StoreInMediaAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.StoreInMedia.ToList();
                }
                catch
                {
                    return new List<StoreInMedia>();
                }
            }
        }

        public IList<StoreInMedia> GetList_StoreInMedia_StoreId(long storeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.StoreInMedia.Where(a => a.StoreId == storeId).ToList();
                }
                catch
                {
                    return new List<StoreInMedia>();
                }
            }
        }

        public StoreInMedia Get_StoreInMediaById(long StoreInMediaId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.StoreInMedia.Find(StoreInMediaId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(StoreInMedia StoreInMedia)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.StoreInMedia.Add(StoreInMedia);
                    _data.SaveChanges();

                    return StoreInMedia.StoreInMediaId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(StoreInMedia StoreInMedia)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    StoreInMedia StoreInMediaToUpdate;
                    StoreInMediaToUpdate = entities.StoreInMedia.Where(x => x.StoreInMediaId == StoreInMedia.StoreInMediaId).FirstOrDefault();
                    StoreInMediaToUpdate = StoreInMedia;
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
