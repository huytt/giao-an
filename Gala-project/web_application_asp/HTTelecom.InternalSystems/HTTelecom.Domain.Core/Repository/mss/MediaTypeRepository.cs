using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class MediaTypeRepository
    {
        public IList<MediaType> GetList_MediaTypeAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.MediaType.ToList();
                }
                catch
                {
                    return new List<MediaType>();
                }
            }
        }

        public IList<MediaType> GetList_MediaTypeAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.MediaType.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<MediaType>();
                }
            }
        }

        public IList<MediaType> GetList_MediaTypeAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                return _data.MediaType.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<MediaType>();
                }
            }
        }

        public IList<MediaType> GetList_MediaTypeAll_IsDeleted_IsActive(bool IsDeleted, bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.MediaType.Where(a => a.IsDeleted == IsDeleted)
                                      .Where(b => b.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<MediaType>();
                }
            }
        }


        public MediaType Get_MediaTypeById(long MediaTypeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.MediaType.Find(MediaTypeId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public MediaType Get_MediaTypeByCode(string MediaTypeCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                return _data.MediaType.Where(a => a.MediaTypeCode == MediaTypeCode).FirstOrDefault();
                    }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(MediaType MediaType)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.MediaType.Add(MediaType);
                    _data.SaveChanges();

                    return MediaType.MediaTypeId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(MediaType MediaType)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    MediaType MediaTypeToUpdate;
                    MediaTypeToUpdate = entities.MediaType.Where(x => x.MediaTypeId == MediaType.MediaTypeId).FirstOrDefault();
                    MediaTypeToUpdate = MediaType;
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
