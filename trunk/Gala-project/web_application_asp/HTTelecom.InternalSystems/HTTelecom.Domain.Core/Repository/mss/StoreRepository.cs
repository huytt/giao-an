using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.ExClass;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class StoreRepository
    {
        public IPagedList<Store> GetList_StoreAll(int pageNum, int pageSize)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Store = _data.Store.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);


                    return lst_Store;
                }
                catch
                {
                    return new PagedList<Store>(new List<Store>(), 1, pageSize);
                }
            }
        }

        public IPagedList<Store> GetList_StoreAll(int pageNum, int pageSize, List<long> listStoreId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Store = new List<Store>();
                    foreach (var item in listStoreId)
                    {
                        lst_Store.Add(this.Get_StoreById(item));
                    }


                    return lst_Store.ToPagedList(pageNum, pageSize);
                }
                catch
                {
                    return new PagedList<Store>(new List<Store>(), 1, pageSize);
                }
            }
        }
        public IList<Store> GetList_StoreAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Store.OrderByDescending(a => a.DateCreated).ToList();
                }
                catch
                {
                    return new List<Store>();
                }
            }
        }

        public IList<Store> GetList_StoreAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Store.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Store>();
                }
            }
        }

        public IList<Store> GetList_StoreAll_ShowIsMall(bool ShowInMallPage, bool isDeleted, bool isActived, bool isVerified)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Store.Where(a => a.ShowInMallPage == ShowInMallPage && a.IsDeleted == isDeleted && a.IsActive == isActived && a.IsVerified == isVerified).ToList();
                }
                catch
                {
                    return new List<Store>();
                }
            }
        }

        public IList<Store> GetList_StoreAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Store.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Store>();
                }
            }
        }
        public IList<Store> GetList_StoreAll_IsDeleted_IsActive(bool IsDeleted, bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Store.Where(a => a.IsDeleted == IsDeleted)
                                      .Where(b => b.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Store>();
                }
            }
        }

        public Store Get_StoreById(long StoreId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Store.Find(StoreId);
                }
                catch
                {
                    return null;
                }
            }
        }

        //public Store Get_StoreByCode(string StoreCode)
        //{
        //    using (MSS_DBEntities _data = new MSS_DBEntities())
        //    {
        //        return _data.Stores.Where(a => a.StoreCode == StoreCode).FirstOrDefault();
        //    }
        //}

        public long Insert(Store Store)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Store.Alias = Generates.generateAlias(Store.StoreName);
                    Store.DateCreated = DateTime.Now;
                    Store.VisitCount = 0;

                    _data.Store.Add(Store);
                    _data.SaveChanges();

                    return Store.StoreId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool UpdateVerified(long storeId, bool isVerified)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Store store = _data.Store.Where(x => x.StoreId == storeId).FirstOrDefault();

                    if (isVerified == true)
                    {
                        store.DateVerified = DateTime.Now;
                    }
                    store.IsVerified = isVerified;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public Tuple<bool, string> UpdateActive(long storeId, bool isActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Store store = _data.Store.Where(x => x.StoreId == storeId).FirstOrDefault();

                    store.IsActive = isActive;
                    // edit bay vannl 29/07/2015
                    if (store.StoreCode == null && store.IsActive == true)
                    {
                        store.StoreCode = (int.Parse((_data.Store.Max(x => x.StoreCode))) + 1).ToString("000000");
                    }
                    _data.SaveChanges();

                    return new Tuple<bool, string>(true, store.StoreCode);
                }
                catch
                {
                    return new Tuple<bool, string>(false, String.Empty);
                }
            }
        }

        public bool Update(Store Store)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Store StoreToUpdate = new Store();
                    StoreToUpdate = _data.Store.Where(x => x.StoreId == Store.StoreId).FirstOrDefault();

                    ///////////////////////////////////////////
                    StoreToUpdate.VendorId = Store.VendorId ?? StoreToUpdate.VendorId;
                    // Edit by Vannl 29/07/2015
                    if (Store.IsActive == true && Store.StoreCode == null)
                    {
                        StoreToUpdate.StoreCode = (int.Parse((_data.Store.Max(x => x.StoreCode))) + 1).ToString("000000");
                    }
                    StoreToUpdate.StoreName = Store.StoreName ?? StoreToUpdate.StoreName;
                    StoreToUpdate.StoreComplexName = Store.StoreComplexName ?? StoreToUpdate.StoreComplexName;
                    StoreToUpdate.Description = Store.Description ?? StoreToUpdate.Description;
                    StoreToUpdate.Alias = Generates.generateAlias(Store.StoreName);
                    StoreToUpdate.Keywords = Store.Keywords ?? StoreToUpdate.Keywords;
                    StoreToUpdate.OnlineDate = Store.OnlineDate ?? StoreToUpdate.OnlineDate;
                    StoreToUpdate.OfflineDate = Store.OfflineDate ?? StoreToUpdate.OfflineDate;
                    StoreToUpdate.ShowInMallPage = Store.ShowInMallPage ?? StoreToUpdate.ShowInMallPage;
                    StoreToUpdate.MetaTitle = Store.MetaTitle ?? StoreToUpdate.MetaTitle;
                    StoreToUpdate.MetaKeywords = Store.MetaKeywords ?? StoreToUpdate.MetaKeywords;
                    StoreToUpdate.MetaDescription = Store.MetaDescription ?? StoreToUpdate.MetaDescription;

                    StoreToUpdate.ModifiedBy = Store.ModifiedBy ?? StoreToUpdate.ModifiedBy;
                    StoreToUpdate.DateModified = DateTime.Now;
                    StoreToUpdate.IsVerified = Store.IsVerified ?? StoreToUpdate.IsVerified;
                    if (Store.IsVerified == true)
                    {
                        StoreToUpdate.DateVerified = DateTime.Now;
                    }
                    StoreToUpdate.IsActive = Store.IsActive ?? StoreToUpdate.IsActive;
                    StoreToUpdate.IsDeleted = Store.IsDeleted ?? StoreToUpdate.IsDeleted;
                    ///////////////////////////////////////////

                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IList<Store> SearchStore(string keywords)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    IList<Store> lst_Store = new List<Store>();
                    string wordASCII = Generates.ConvertUnicodeToASCII(keywords).ToLower();
                    var querylst_Store = _data.Store.ToList();
                    lst_Store = querylst_Store.FindAll(
                        delegate(Store store)
                        {
                            string storeCode = "s" + store.StoreCode.ToLower();
                            if (Generates.ConvertUnicodeToASCII(store.StoreName.ToLower()).Contains(wordASCII) || Generates.ConvertUnicodeToASCII(storeCode).Contains(wordASCII))
                                return true;
                            else
                                return false;
                        }
                    );
                    //}
                    return lst_Store;
                }
                catch { return new List<Store>(); }
            }
        }

    }
}
