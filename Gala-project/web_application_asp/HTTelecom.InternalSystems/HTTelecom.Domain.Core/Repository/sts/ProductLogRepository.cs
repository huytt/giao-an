using HTTelecom.Domain.Core.DataContext.sts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.sts
{
    public class ProductLogRepository
    {
        public long Create(ProductLog _ProductLog)
        {
            try
            {
                using (STSEntities _STSDb = new STSEntities())
                {
                    _STSDb.ProductLog.Add(_ProductLog);
                    _STSDb.SaveChanges();
                    return _ProductLog.ProductLogId;
                }

            }
            catch
            {
                return -1;
            }
        }
        public ProductLog GetByProduct(long ProductId)
        {
            try
            {
                using (STSEntities _STSDb = new STSEntities())
                {
                    var rs = _STSDb.ProductLog.Where(n => n.ProductId == ProductId).FirstOrDefault();
                    return rs;
                }

            }
            catch
            {
                return null;
            }
        }
        public bool RemoveByProduct(long ProductId)
        {
            try
            {
                using (STSEntities _STSDb = new STSEntities())
                {
                    var rs = _STSDb.ProductLog.Where(n => n.ProductId == ProductId).FirstOrDefault();
                    if (rs != null)
                    {
                        _STSDb.ProductLog.Remove(rs);
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
                using (STSEntities _STSDb = new STSEntities())
                {
                    var rs = _STSDb.ProductLog.Find(Id);
                    if (rs != null)
                    {
                        _STSDb.ProductLog.Remove(rs);
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
