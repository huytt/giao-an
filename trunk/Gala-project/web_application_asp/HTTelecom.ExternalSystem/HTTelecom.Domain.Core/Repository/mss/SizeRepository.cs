using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SizeRepository
    {
        public List<Size> GetAll(bool IsDeleted)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Size.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Size>();
            }
        }
        public Size GetById(long SizeId)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    return _data.Size.Find(SizeId);
                }
            }
            catch
            {
                return null;
            }
        }
        public List<Size> GetByListId(string[] lstSize)
        {
            try
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
                            var rs = _data.Size.Where(n => n.SizeId == id).FirstOrDefault();
                            lst.Add(rs);
                        }
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                return new List<Size>();
            }
        }
    }
}
