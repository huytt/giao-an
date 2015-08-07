using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ArticleRepository
    {
        public List<Article> GetAll(bool IsDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Article.Where(n => n.IsDeleted == IsDeleted && n.IsPublish == true && n.IsVerified == true).ToList();
            }
        }
        public List<Article> GetAll(bool IsDeleted, string lang)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                return _data.Article.Where(n => n.IsDeleted == IsDeleted && n.IsPublish == true && n.IsVerified == true && n.LanguageCode == lang).ToList();
            }
        }
        public Article GetById(long id)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.Article.Find(id);
            }
        }
        public Article GetById(long id, string lang)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var item = _data.Article.Find(id);
                var rs = _data.Article.Where(n => n.Code == item.Code && n.LanguageCode == lang).FirstOrDefault();
                return rs != null ? rs : item;
            }
        }
    }
}
