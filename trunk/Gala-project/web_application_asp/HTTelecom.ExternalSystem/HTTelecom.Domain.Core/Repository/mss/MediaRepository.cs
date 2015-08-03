using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class MediaRepository
    {
        public List<Media> GetAll()
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Media.ToList();
            }
            catch
            {
                return new List<Media>();
            }
        }

        public Media GetById(long MediaId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Media.Where(n => n.MediaId == MediaId && n.IsDeleted == false && n.IsActive == true).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Media> GetByHome()
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                if (_data.Database.Exists())
                {

                }
                {
                    return _data.Media.Where(n => n.IsActive == true && n.IsDeleted == false && n.MediaType.MediaTypeCode == "MALL-1").ToList();
                }

            }
            catch
            {
                return new List<Media>();
            }
        }
    }
}
