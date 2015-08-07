using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SizeRepository
    {
        public List<Size> GetAll(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Size.Where(n => n.IsDeleted == IsDeleted).Include(n => n.SizeGlobal).ToList();
            }
        }
        public Size GetById(long SizeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Size.Include(n => n.SizeGlobal).Where(n => n.SizeId == SizeId).FirstOrDefault();
            }
        }
        public List<Size> GetByListId(string[] lstSize)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                var lst = new List<Size>();
                foreach (var item in lstSize)
                {
                    if (item.Length > 0)
                    {
                        var id = Convert.ToInt32(item);
                        var rs = _data.Size.Where(n => n.SizeId == id).Include(n => n.SizeGlobal).FirstOrDefault();
                        lst.Add(rs);
                    }
                }
                return lst;
            }
        }
    }
}
