using HTTelecom.Domain.Core.DataContext.sts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sts
{
    public class ProductBuyStatisticRepository
    {
        public long Create(ProductBuyStatistic _ProductBuyStatistic)
        {
            try
            {
                STSEntities _STSDb = new STSEntities();
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
