using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class StoreRepository
    {
        public Store GetById(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Store.Find(id);

            }
            catch
            {
                return null;
            }
        }

        public Store GetByVendorId(long vendorId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                var tmp = _data.Store.Where(_ => _.VendorId == vendorId).ToList();
                if (tmp.Count > 0)
                {
                    return tmp[0];
                }
                return null;

            }
            catch
            {
                return null;
            }
        }
        public bool CheckStoreOnline(long id)
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var store = _data.Store.Where(n => n.StoreId == id && n.IsVerified == true && n.IsDeleted == false && n.IsActive == true && n.OnlineDate.HasValue == true && n.OfflineDate.HasValue == true).FirstOrDefault();
                if (store == null)
                    return false;
                if ((toDay - store.OnlineDate.Value).TotalMinutes >= 0 && (store.OfflineDate.Value - toDay).TotalMinutes >= 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void VisitCount(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                Store st = _data.Store.Find(id);
                if (st != null)
                {
                    if (st.VisitCount == null)
                        st.VisitCount = 0;
                    st.VisitCount++;
                    _data.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public List<Store> GetStoreOrther(long id)
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.Store.Where(n => n.IsVerified == true && n.IsDeleted == false && n.IsActive == true && n.OnlineDate.HasValue == true && n.OfflineDate.HasValue == true).ToList();
                var lstStore = new List<Store>();
                foreach (var item in lst)
                    //if ((toDay - item.OnlineDate.Value).TotalMinutes >= 0 && (item.OfflineDate.Value - toDay).TotalMinutes >= 0)
                    lstStore.Add(item);
                var index = lstStore.FindIndex(n => n.StoreId == id);
                if (index == null || index == -1)
                    return new List<Store>() { new Store(), new Store() };
                else
                {
                    if (index == 0)
                        return new List<Store>() { new Store(), lstStore[index + 1] };
                    else if (index == lstStore.Count - 1)
                        return new List<Store>() { lstStore[index - 1], new Store() };
                    else return new List<Store>() { lstStore[index - 1], lstStore[index + 1] };
                }
            }
            catch
            {
                return new List<Store>() { new Store(), new Store() };
            }
        }

        public List<Store> GetAll()
        {
            try
            {
                var toDay = DateTime.Now;
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.Store.Where(n => n.IsVerified == true && n.IsDeleted == false && n.IsActive == true && n.OnlineDate.HasValue == true && n.OfflineDate.HasValue == true).ToList();
                return lst;
            }
            catch
            {
                return new List<Store>() { new Store(), new Store() };
            }
        }

    }
}
