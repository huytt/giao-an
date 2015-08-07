using HTTelecom.Domain.Core.DataContext.sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sms
{
    public class ProductBuyStatisticRepository
    {
        public long Create(ProductBuyStatistic _ProductBuyStatistic)
        {
            try
            {
                SMS_DBEntities _STSDb = new SMS_DBEntities();
                _STSDb.ProductBuyStatistic.Add(_ProductBuyStatistic);
                _STSDb.SaveChanges();
                return _ProductBuyStatistic.ProductBuyStatisticId;
            }
            catch
            {
                return -1;
            }
        }
    }
}
