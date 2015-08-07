using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
namespace HTTelecom.Domain.Core.Repository.mss
{
    public class MediaRepository
    {
        public List<Media> GetAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Media.ToList();
            }
        }

        public Media GetById(long MediaId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Media.Where(n => n.MediaId == MediaId && n.IsDeleted == false && n.IsActive == true).Include(n=>n.MediaType).FirstOrDefault();
            }
        }


        #region

        public Media GetByIdApp(long MediaId)
        {
            MSS_DBEntities _data = new MSS_DBEntities();
            {
                return _data.Media.Where(n => n.MediaId == MediaId && n.IsDeleted == false && n.IsActive == true).Include(n => n.MediaType).FirstOrDefault();
            }
        }
        #endregion
        public List<Media> GetByHome()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Media.Where(n => n.IsActive == true && n.IsDeleted == false && n.MediaType.MediaTypeCode == "MALL-1").Include(n=>n.MediaType).ToList();
            }

        }

        #region 
        public List<Media> GetByHomeApp()
        {
            MSS_DBEntities _data = new MSS_DBEntities();
            {
                return _data.Media.Where(n => n.IsActive == true && n.IsDeleted == false && n.MediaType.MediaTypeCode == "MALL-1").Include(n => n.MediaType).ToList();
            }

        }
        #endregion
    }
}
