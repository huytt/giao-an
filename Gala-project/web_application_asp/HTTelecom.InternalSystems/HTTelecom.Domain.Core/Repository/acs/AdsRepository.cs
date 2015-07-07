using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.acs;
using PagedList;
using PagedList.Mvc;
namespace HTTelecom.Domain.Core.Repository.acs
{
    public class AdsRepository
    {
        public IPagedList<Ad> GetList_AdsAll(int pageNum, int pageSize)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_Product = _data.Ads.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);


                    return lst_Product;
                }
                catch
                {
                    return new PagedList<Ad>(new List<Ad>(), 1, pageSize);
                }
            }
        }

        public IList<Ad> GetList_AdsAll()
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    return _data.Ads.OrderBy(a => a.DateCreated).ToList();
                }
                catch
                {
                    return new List<Ad>();
                }
            }
        }
        public List<Ad> GetList_AdsAll(bool IsDeleted, bool IsActive)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    return _data.Ads.Where(n => n.IsActive == IsActive && n.IsDeleted == IsDeleted).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }
        public List<Ad> GetList_AdsAll(bool IsLocked, bool IsActive, bool IsDelete)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    return _data.Ads.Where(n => n.IsLocked == IsLocked && n.IsActive == IsActive && n.IsDeleted == IsDelete).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }
        public Ad GetById(long id)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    return _data.Ads.Find(id);
                }
                catch
                {
                    return null;
                }
            }
        }

        public Ad GetByCode(string ReservationCode)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    return _data.Ads.Where(n => n.ReservationCode == ReservationCode).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }

        public Ad GetByAds_adsContentId(long _adsContentId)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    return _data.Ads.Where(a => a.AdsContentId == _adsContentId).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }
        
        public long Create(Ad model)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    model.ReservationCode = GetCode();
                    _data.Ads.Add(model);
                    _data.SaveChanges();
                    return model.AdsId;
                }
                catch
                {
                    return -1;
                }
            }
        }

        private string GetCode()
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                Random rd = new Random();
                var date = DateTime.Now;
                var x = "MRF" + string.Format("{0:00}", date.Year.ToString().Substring(date.Year.ToString().Length - 2)) + string.Format("{0:00}", date.Month) + string.Format("{0:00}", date.Day) + "-" + string.Format("{0:000000}", rd.Next(0, 100000));
                var lst = _data.Ads.Where(n => n.ReservationCode.ToUpper().IndexOf(x.ToUpper()) == 0).ToList();
                if (lst.Count > 0)
                    return GetCode();
                return x;
            }
        }

        public bool UpdateLock(string Code, bool IsLock)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    var ad = _data.Ads.Where(n => n.ReservationCode == Code).FirstOrDefault();
                    if (ad == null)
                        return false;
                    if (ad.IsActive == true && ad.IsDeleted == false && ad.IsLocked == false)
                    {
                        ad.IsLocked = true;
                        _data.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateIsActive(long adsId, bool IsActive)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    var ad = _data.Ads.Where(n => n.AdsId == adsId).FirstOrDefault();

                    ad.IsActive = IsActive;
                    _data.SaveChanges();
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
