using HTTelecom.Domain.Core.DataContext.sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HTTelecom.Domain.Core.Repository.sms
{
    public class StoreStatisticRepository
    {
        public StoreStatistic Get_StoreStatisticById(long _StoreStatisticID)
        {
            using (SMS_DBEntities _data = new SMS_DBEntities())
            {
                try
                {
                    return _data.StoreStatistic.Find(_StoreStatisticID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public List<StoreStatistic> ListStoreIdWithDate(DateTime date_from, DateTime date_to)
        {
            using (SMS_DBEntities _data = new SMS_DBEntities())
            {
                try
                {
                    return _data.StoreStatistic.Where(_=>_.Date >=date_from &&_.Date<=date_to).OrderByDescending(g=>g.StoreId).ToList();
                }
                catch
                {
                    return new List<StoreStatistic>();
                }
            }
        }
        public IList<StoreStatistic> GetList_StoreStatisticAll()
        {
            using (SMS_DBEntities _data = new SMS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.StoreStatistic.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<StoreStatistic>();
                }
            }
        }
        public long InsertStoreStatistic(StoreStatistic _StoreStatistic)
        {
            using (SMS_DBEntities _data = new SMS_DBEntities())
            {
                try
                {
                    _data.StoreStatistic.Add(_StoreStatistic);                    
                    _data.SaveChanges();
                    return _StoreStatistic.StoreStatisticId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateStoreStatistic(StoreStatistic _StoreStatistic)
        {
            using (SMS_DBEntities entities = new SMS_DBEntities())
            {
                try
                {
                    StoreStatistic StoreStatisticToUpdate;
                    StoreStatisticToUpdate = entities.StoreStatistic.Where(x => x.StoreStatisticId == _StoreStatistic.StoreStatisticId).FirstOrDefault();

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

    }


}
