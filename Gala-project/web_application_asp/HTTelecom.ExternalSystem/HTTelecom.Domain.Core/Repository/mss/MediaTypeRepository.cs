using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class MediaTypeRepository
    {
        public MediaType GetById(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
                return _data.MediaType.Find(id);
        }

        public MediaType GetByTypeCode(string _TypeCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var tmp = _data.MediaType.Where(a => a.MediaTypeCode == _TypeCode).ToList();
                if (tmp.Count > 0)
                {
                    return tmp[0];
                }
                return null;
            }
        }
    }
}
