using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ArticleTypeRepository
    {
        public List<ArticleType> GetAll(bool IsDeleted)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.ArticleType.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<ArticleType>();
            }
        }
        public List<ArticleType> GetAll(bool IsDeleted, string lang)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    _data.Configuration.ProxyCreationEnabled = false;
                    _data.Configuration.LazyLoadingEnabled = false;
                    return _data.ArticleType.Where(n => n.IsDeleted == IsDeleted && n.LanguageCode == lang).ToList();
                }
            }
            catch
            {
                return new List<ArticleType>();
            }
        }
    }
}
