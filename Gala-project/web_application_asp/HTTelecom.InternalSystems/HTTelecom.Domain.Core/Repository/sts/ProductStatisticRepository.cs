using HTTelecom.Domain.Core.DataContext.sts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sts
{
    public class ProductStatisticRepository
    {
        public ProductStatistic Get_ProductStatisticById(long _ProductStatisticID)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    return _data.ProductStatistic.Find(_ProductStatisticID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<ProductStatistic> GetList_ProductStatisticAll()
        {
            using (STSEntities _data = new STSEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ProductStatistic.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<ProductStatistic>();
                }
            }
        }
        public long InsertProductStatistic(ProductStatistic _ProductStatistic)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    _data.ProductStatistic.Add(_ProductStatistic);
                    _data.SaveChanges();
                    return _ProductStatistic.ProductStatisticId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateProductStatistic(ProductStatistic _ProductStatistic)
        {
            using (STSEntities entities = new STSEntities())
            {
                try
                {
                    ProductStatistic ProductStatisticToUpdate;
                    ProductStatisticToUpdate = entities.ProductStatistic.Where(x => x.ProductStatisticId == _ProductStatistic.ProductStatisticId).FirstOrDefault();

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
        public long Create(ProductStatistic _ProductStatistic)
        {
            try
            {
                STSEntities _STSDb = new STSEntities();
                _STSDb.ProductStatistic.Add(_ProductStatistic);
                _STSDb.SaveChanges();
                return _ProductStatistic.ProductStatisticId;
            }
            catch
            {
                return -1;
            }
        }
        public bool EditTime(long ProductId,string content,int CounterNew,DateTime Date)
        {
            try
            {
                Date.Add(new TimeSpan(0, 0, 0));
                STSEntities _STSDb = new STSEntities();
                var rs = _STSDb.ProductStatistic.Where(n => n.ProductId == ProductId && n.Date.Value == Date).FirstOrDefault();
                if (rs != null)
                {
                    rs.Content = content;
                    rs.Counter = CounterNew;
                    _STSDb.SaveChanges();
                    return true;
                }
                else return false;
                
            }
            catch
            {
                return false;
            }
        }
    }
}
