using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ops
{
    public class LogRepository
    {
        public List<Log> GetAll(bool IsDeleted)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    return _data.Log.ToList();
                }
                catch
                {
                    return new List<Log>();
                }
            }
        }
        public long Create(Log l)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    _data.Log.Add(l);
                    _data.SaveChanges();
                    return l.LogId;
                }
                catch
                {
                    return -1;
                }
            }
        }
    }
}
