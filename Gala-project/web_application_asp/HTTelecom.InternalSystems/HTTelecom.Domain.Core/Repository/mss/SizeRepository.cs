using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SizeRepository
    {
        public List<Size> GetList_SizeAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Size.Where(n => n.IsDeleted == false).ToList();
                }
                catch
                {
                    return new List<Size>();
                }
            }
        }

        public Size Get_Size_SizeId(long SizeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Size.Where(n => n.SizeId == SizeId).FirstOrDefault();
                }
                catch
                {
                    return new Size();
                }
            }
        }
    }
}
