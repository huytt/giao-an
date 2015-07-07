using HTTelecom.Domain.Core.DataContext.cis;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class AdsCustomerRepository
    {
        private CIS_DBEntities _data;
        public AdsCustomerRepository()
        {
            _data = new CIS_DBEntities();
        }

        public long Create(AdsCustomer adsCustomer)
        {
            try
            {
                _data.AdsCustomers.Add(adsCustomer);
                _data.SaveChanges();
                return adsCustomer.AdsCustomerId;
            }
            catch
            {
                return -1;
            }
        }

        public AdsCustomer GetById(long AdsCustomerId)
        {
            try
            {

                return _data.AdsCustomers.Find(AdsCustomerId);
            }
            catch
            {
                return null;
            }
        }

        public IPagedList<AdsCustomer> GetList_AdsCustomerPagingAll(int pageNum, int pageSize)
        {
            using (CIS_DBEntities _data = new CIS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    var lst_AdsCustomer = _data.AdsCustomers.OrderByDescending(a => a.DateCreated).ToPagedList(pageNum, pageSize);

                    foreach (var item in lst_AdsCustomer)
                    {
                        item.Password = null;
                    }
                    return lst_AdsCustomer;
                }
                catch
                {
                    return new PagedList<AdsCustomer>(new List<AdsCustomer>(), 1, pageSize);
                }
            }
        }
    }
}
