using HTTelecom.Domain.Core.DataContext.sts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sts
{
    public class StoreLogRepository
    {
        public StoreLog Get_StoreLogById(long _StoreLogID)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    return _data.StoreLog.Find(_StoreLogID);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }
        public IList<StoreLog> GetList_StoreLogAll()
        {
            using (STSEntities _data = new STSEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.StoreLog.ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<StoreLog>();
                }
            }
        }
        public long InsertStoreLog(StoreLog _StoreLog, bool member)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    _StoreLog.Counter = 0;
                    _StoreLog.CounterMember = 0;
                    if(member)
                        _StoreLog.CounterMember = 1;
                    else
                        _StoreLog.Counter = 1;
                    _StoreLog.Time = DateTime.Now;
                    _data.StoreLog.Add(_StoreLog);
                    _data.SaveChanges();
                    return _StoreLog.StoreLogId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }
        public bool UpdateStoreLog(StoreLog _StoreLog)
        {
            using (STSEntities entities = new STSEntities())
            {
                try
                {
                    StoreLog StoreLogToUpdate;
                    StoreLogToUpdate = entities.StoreLog.Where(x => x.StoreLogId == _StoreLog.StoreLogId).FirstOrDefault();

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
        public long StoreInsertStatistics(StoreLog _StoreLog,bool member)
        {
            using (STSEntities _data = new STSEntities())
            {
                try
                {
                    var tmp = _data.StoreLog.Where(_=>_.StoreId == _StoreLog.StoreId).ToList();
                    if(tmp.Count > 0)// cập nhât store log
                    {
                        StoreLog StoreLogToUpdate = tmp[0];
                        StoreLogToUpdate.Time = DateTime.Now;

                        if (member)
                            StoreLogToUpdate.CounterMember += 1;
                        else
                            StoreLogToUpdate.Counter += 1;
                        _data.SaveChanges();
                        return 0;
                    }
                 // ngược lại thêm mới store log

                    return this.InsertStoreLog(_StoreLog, member);
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
