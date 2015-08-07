using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SizeGlobalRepository
    {
        public List<SizeGlobal> GetAll(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.SizeGlobal.Where(n => n.IsDelete == IsDeleted).ToList();
            }
             
        }

        public List<SizeGlobal> GetByListId(string[] lstSizeGlobal)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                var lst = new List<SizeGlobal>();
                foreach (var item in lstSizeGlobal)
                {
                    if (item.Length > 0)
                    {
                        var id = Convert.ToInt32(item);
                        var rs = _data.SizeGlobal.Where(n => n.SizeGlobalId == id).FirstOrDefault();
                        lst.Add(rs);
                    }
                }
                return lst;
            }
        }
    }
}
