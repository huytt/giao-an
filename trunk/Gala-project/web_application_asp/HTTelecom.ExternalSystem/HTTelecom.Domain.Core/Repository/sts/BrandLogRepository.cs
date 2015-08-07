using HTTelecom.Domain.Core.DataContext.sts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sts
{
    public class BrandLogRepository
    {
        public BrandLog Get_BrandLogById(long _BrandLogID)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    return _data.BrandLog.Find(_BrandLogID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<BrandLog> GetList_BrandLogAll()
        {
            using (STSEntities _data = new STSEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.BrandLog.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<BrandLog>();
                }
            }
        }
        public long InsertBrandLog(BrandLog _BrandLog, bool member)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    _BrandLog.Counter = 0;
                    _BrandLog.CounterMember = 0;
                    if (member)
                        _BrandLog.CounterMember = 1;
                    else
                        _BrandLog.Counter = 1;
                    _BrandLog.Time = DateTime.Now;
                    _data.BrandLog.Add(_BrandLog);
                    _data.SaveChanges();
                    return _BrandLog.BrandLogId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateBrandLog(BrandLog _BrandLog)
        {
            using (STSEntities entities = new STSEntities())
            {
                try
                {
                    BrandLog BrandLogToUpdate;
                    BrandLogToUpdate = entities.BrandLog.Where(x => x.BrandLogId == _BrandLog.BrandLogId).FirstOrDefault();

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
        public long BrandInsertStatistics(BrandLog _BrandLog, bool member)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    var tmp = _data.BrandLog.Where(_ => _.BrandId == _BrandLog.BrandId).ToList();
                    if (tmp.Count > 0)// cập nhât Brand log
                    {
                        BrandLog BrandLogToUpdate = tmp[0];
                        BrandLogToUpdate.Time = DateTime.Now;

                        if (member)
                            BrandLogToUpdate.CounterMember += 1;
                        else
                            BrandLogToUpdate.Counter += 1;
                        _data.SaveChanges();
                        return 0;
                    }
                    // ngược lại thêm mới Brand log

                    return this.InsertBrandLog(_BrandLog, member);
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
