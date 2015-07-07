using HTTelecom.Domain.Core.DataContext.acs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.acs
{
    public class AdsRepository
    {
        public List<Ads> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                ACS_DBEntities _data = new ACS_DBEntities();
                return _data.Ads.Where(n => n.IsActive == IsActive && n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Ads>();
            }
        }

        public Ads GetById(long id)
        {
            try
            {
                ACS_DBEntities _data = new ACS_DBEntities();
                return _data.Ads.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public List<Ads> GetByAdsCustomer(long AdsCustomerId)
        {
            try
            {
                ACS_DBEntities _data = new ACS_DBEntities();
                return _data.Ads.Where(n => n.AdsCustomerId == AdsCustomerId).ToList();
            }
            catch
            {
                return new List<Ads>();
            }
        }
    }
}
