using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SizeGlobalRepository
    {
        public List<SizeGlobal> GetList_SizeGlobalAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.SizeGlobal.ToList();
                }
                catch
                {
                    return new List<SizeGlobal>();
                }
            }
        }

        public List<SizeGlobal> GetList_SizeGlobalAll_IsDeleted(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.SizeGlobal.Where(a => a.IsDelete == IsDeleted).ToList();
                }
                catch
                {
                    return new List<SizeGlobal>();
                }
            }
        }
        public SizeGlobal Get_SizeGlobal_SizeGlobalId(long SizeGlobalId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.SizeGlobal.Where(a => a.SizeGlobalId == SizeGlobalId).FirstOrDefault();
                }
                catch
                {
                    return new SizeGlobal();
                }
            }
        }
    }
}
