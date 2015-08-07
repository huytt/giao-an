using HTTelecom.Domain.Core.DataContext.sts;
using HTTelecom.Domain.Core.Repository.sts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sts
{
    public class ProductLogRepository
    {
        public ProductLog Get_ProductLogById(long _ProductLogID)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    return _data.ProductLog.Find(_ProductLogID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<ProductLog> GetList_ProductLogAll()
        {
            using (STSEntities _data = new STSEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductLog.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductLog>();
                }
            }
        }
        public long InsertProductLog(ProductLog _ProductLog, int counter)            
        {
            /*
                int counter              = 1
                int counterMember        = 2
                int counterBuyPage       = 3
                int counterAddToCart	 = 4
                int counterAddToWishList = 5
             */
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    _ProductLog.Counter = 0;
                    _ProductLog.CounterMember = 0;
                    _ProductLog.CounterBuyPage = 0;
                    _ProductLog.CounterAddToCart = 0;
                    _ProductLog.CounterAddToWishList = 0;
                    switch (counter)
                    {
                        case 1:
                            _ProductLog.Counter = 1;
                            break;
                        case 2:
                            _ProductLog.CounterMember = 1;
                            break;
                        case 3:
                            _ProductLog.CounterBuyPage = 1;
                            break;
                        case 4:
                            _ProductLog.CounterAddToCart = 1;
                            break;
                        case 5:
                            _ProductLog.CounterAddToWishList = 1;
                            break;
                    }
                 
                    _ProductLog.Time = DateTime.Now;
                    _data.ProductLog.Add(_ProductLog);
                    _data.SaveChanges();
                    return _ProductLog.ProductLogId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateProductLog(ProductLog _ProductLog)
        {
            using (STSEntities entities = new STSEntities())
            {
                try
                {
                    ProductLog ProductLogToUpdate;
                    ProductLogToUpdate = entities.ProductLog.Where(x => x.ProductLogId == _ProductLog.ProductLogId).FirstOrDefault();

                    entities.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return false;
                }
            }
        }
        public long ProductInsertStatistics(ProductLog _ProductLog, int counter)
        {
            /*
                int counter              = 1
                int counterMember        = 2
                int counterBuyPage       = 3
                int counterAddToCart	 = 4
                int counterAddToWishList = 5
             */
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    var tmp = _data.ProductLog.Where(_ => _.ProductId == _ProductLog.ProductId).ToList();
                    if (tmp.Count > 0)// cập nhât Product log
                    {
                        ProductLog ProductLogToUpdate = tmp[0];
                        ProductLogToUpdate.Time = DateTime.Now;

                        switch (counter)
                        {
                            case 1:
                                ProductLogToUpdate.Counter += 1;
                                break;
                            case 2:
                                ProductLogToUpdate.CounterMember += 1;
                                break;
                            case 3:
                                ProductLogToUpdate.CounterBuyPage += 1;
                                break;
                            case 4:
                                ProductLogToUpdate.CounterAddToCart += 1;
                                break;
                            case 5:
                                ProductLogToUpdate.CounterAddToWishList += 1;
                                break;
                        }
                   
                        _data.SaveChanges();
                        return 0;
                    }
                    // ngược lại thêm mới Product log

                    return this.InsertProductLog(_ProductLog, counter);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
    }
}
