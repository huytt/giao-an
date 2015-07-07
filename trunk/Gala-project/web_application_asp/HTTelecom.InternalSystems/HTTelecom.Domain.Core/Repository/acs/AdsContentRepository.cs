using HTTelecom.Domain.Core.DataContext.acs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.acs
{
    public class AdsContentRepository
    {
        private ACS_DBEntities _data;
        public AdsContentRepository()
        {
            _data = new ACS_DBEntities();
        }
        public long Create(AdsContent adsContent)
        {
            try
            {
                _data.AdsContent.Add(adsContent);
                _data.SaveChanges();
                return adsContent.AdsContentId;
            }
            catch
            {
                return -1;
            }
        }

        public AdsContent GetById(long AdsContentId)
        {
            try
            {
                return _data.AdsContent.Find(AdsContentId);
            }
            catch
            {
                return null;
            }
        }
       

        public IList<AdsContent> GetList_AdsContentAll()
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {

                try
                {
                    return _data.AdsContent.OrderBy(a => a.AdsContentId).ToList();
                }
                catch
                {
                    return new List<AdsContent>();
                }
            }
        }

        public bool Update(AdsContent _adsContent)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    AdsContent AdsContentToUpdate;
                    AdsContentToUpdate = _data.AdsContent.Where(c =>c.AdsContentId == _adsContent.AdsContentId).FirstOrDefault();
                    AdsContentToUpdate.AdsHeader = _adsContent.AdsHeader ?? AdsContentToUpdate.AdsHeader;
                    AdsContentToUpdate.ImageFilePath = _adsContent.ImageFilePath ?? AdsContentToUpdate.ImageFilePath;
                    AdsContentToUpdate.VideoFilePath = _adsContent.VideoFilePath ?? AdsContentToUpdate.VideoFilePath;
                    AdsContentToUpdate.LogoFilePath = _adsContent.LogoFilePath ?? AdsContentToUpdate.LogoFilePath;
                    AdsContentToUpdate.AdsTitle = _adsContent.AdsTitle ?? AdsContentToUpdate.AdsTitle;
                    AdsContentToUpdate.Description = _adsContent.Description ?? AdsContentToUpdate.Description;
                    AdsContentToUpdate.LinkSite = _adsContent.LinkSite ?? AdsContentToUpdate.LinkSite;

                    AdsContentToUpdate.DateCreated = _adsContent.DateCreated ?? AdsContentToUpdate.DateCreated;
                    AdsContentToUpdate.DateModified = _adsContent.DateModified ?? AdsContentToUpdate.DateModified;
                    AdsContentToUpdate.ModifiedBy = _adsContent.ModifiedBy ?? AdsContentToUpdate.ModifiedBy;
                    _data.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool Update(long? adsContentId, string imagePath)
        {
            using (ACS_DBEntities _data = new ACS_DBEntities())
            {
                try
                {
                    AdsContent AdsContentToUpdate;
                    AdsContentToUpdate = _data.AdsContent.Where(c => c.AdsContentId == adsContentId).FirstOrDefault();
                    AdsContentToUpdate.ImageFilePath = imagePath ?? AdsContentToUpdate.ImageFilePath;
                    _data.SaveChanges();

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
