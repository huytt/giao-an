using HTTelecom.Domain.Core.DataContext.sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sms
{
    public class StoreLogRepository
    {
        public long Create(StoreLog _StoreLog)
        {
            try
            {
                using (SMS_DBEntities _STSDb = new SMS_DBEntities())
                {
                    _STSDb.StoreLog.Add(_StoreLog);
                    _STSDb.SaveChanges();
                    return _StoreLog.StoreLogId;
                }

            }
            catch
            {
                return -1;
            }
        }
        public StoreLog GetByStore(long StoreId)
        {
            try
            {
                using (SMS_DBEntities _STSDb = new SMS_DBEntities())
                {
                    var rs = _STSDb.StoreLog.Where(n => n.StoreId == StoreId).FirstOrDefault();
                    return rs;
                }

            }
            catch
            {
                return null;
            }
        }
        public bool RemoveByStore(long StoreId)
        {
            try
            {
                using (SMS_DBEntities _STSDb = new SMS_DBEntities())
                {
                    var rs = _STSDb.StoreLog.Where(n => n.StoreId == StoreId).FirstOrDefault();
                    if (rs != null)
                    {
                        _STSDb.StoreLog.Remove(rs);
                        _STSDb.SaveChanges();
                    }
                    else return false;
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }
        public bool Remove(long Id)
        {
            try
            {
                using (SMS_DBEntities _STSDb = new SMS_DBEntities())
                {
                    var rs = _STSDb.StoreLog.Find(Id);
                    if (rs != null)
                    {
                        _STSDb.StoreLog.Remove(rs);
                        _STSDb.SaveChanges();
                    }
                    else return false;
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }
    }
}
