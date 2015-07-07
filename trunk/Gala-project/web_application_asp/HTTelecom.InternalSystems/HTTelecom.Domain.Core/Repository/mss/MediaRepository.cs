using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class MediaRepository
    {
        public IList<Media> GetList_MediaAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Media.ToList();
                }
                catch
                {
                    return new List<Media>();
                }
            }
        }

        public IList<Media> GetList_MediaAll_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Media.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Media>();
                }
            }
        }

        public IList<Media> GetList_MediaAll_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Media.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Media>();
                }
            }
        }

        public Media Get_MediaById(long MediaId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Media.Find(MediaId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IList<Media> GetList_MediaAll_MediaTypeId(long MediaTypeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Media.Where(a => a.MediaTypeId == MediaTypeId).ToList();
                }
                catch
                {
                    return new List<Media>();
                }
            }
        }

        public long Insert(Media Media)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Media.DateCreated = DateTime.Now;
                    Media.IsActive = true;
                    Media.IsDeleted = false;

                    _data.Media.Add(Media);
                    _data.SaveChanges();

                    return Media.MediaId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(Media Media)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    Media MediaToUpdate;
                    MediaToUpdate = entities.Media.Where(x => x.MediaId == Media.MediaId).FirstOrDefault();

                    MediaToUpdate.MediaName = Media.MediaName ?? MediaToUpdate.MediaName;
                    //MediaToUpdate.MediaType = Media.MediaType ?? MediaToUpdate.MediaType;
                    MediaToUpdate.MediaTypeId = Media.MediaTypeId ?? MediaToUpdate.MediaTypeId;
                    //MediaToUpdate.ProductInMedia = Media.ProductInMedia ?? MediaToUpdate.ProductInMedia;
                    //MediaToUpdate.StoreInMedia = Media.StoreInMedia ?? MediaToUpdate.StoreInMedia;
                    MediaToUpdate.Url = Media.Url ?? MediaToUpdate.Url;

                    MediaToUpdate.DateModified = DateTime.Now;
                    MediaToUpdate.IsActive = Media.IsActive ?? MediaToUpdate.IsActive;
                    MediaToUpdate.IsDeleted = Media.IsDeleted ?? MediaToUpdate.IsDeleted;
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteAll(long _mediaTypeId)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    List<Media> lst_MediaToUpdate;
                    lst_MediaToUpdate = this.GetList_MediaAll().Where(x => x.MediaTypeId == _mediaTypeId).ToList();
                    foreach (Media item in lst_MediaToUpdate)
                    {
                        Media MediaToUpdate;
                        MediaToUpdate = entities.Media.Where(x => x.MediaId == item.MediaId).FirstOrDefault();
                        MediaToUpdate.IsDeleted = true;
                        entities.SaveChanges();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateActive(long mediaId, bool isActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Media media = _data.Media.Where(x => x.MediaId == mediaId).FirstOrDefault();

                    media.IsActive = isActive;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }
    }
}
