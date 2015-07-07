using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
   public class AdsCustomerCardRepository
    {
        private CIS_DBEntities _data;
        public AdsCustomerCardRepository()
        {
            _data = new CIS_DBEntities();
        }

        public long Create(AdsCustomerCard adsCustomerCard)
        {
            try
            {
                _data.AdsCustomerCards.Add(adsCustomerCard);
                _data.SaveChanges();
                return adsCustomerCard.AdsCustomerCardId;
            }
            catch
            {
                return -1;
            }
        }

        public AdsCustomerCard GetByAdsCustomer(long adsCustomerId)
        {
            try
            {
                return _data.AdsCustomerCards.Where(n => n.AdsCustomerId == adsCustomerId).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
