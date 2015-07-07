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
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Article.Where(n => n.IsDeleted == IsDeleted && n.IsPublish == true && n.IsVerified == true).ToList();
            }
            catch
            {
                return new List<Article>();
            }
        }
        public List<Article> GetAll(bool IsDeleted, string lang)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    _data.Configuration.ProxyCreationEnabled = false;
                    _data.Configuration.LazyLoadingEnabled = false;
                    return _data.Article.Where(n => n.IsDeleted == IsDeleted && n.IsPublish == true && n.IsVerified == true && n.LanguageCode == lang).ToList();
                }
            }
            catch
            {
                return new List<Article>();
            }
        }
        public Article GetById(long id)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Article.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
