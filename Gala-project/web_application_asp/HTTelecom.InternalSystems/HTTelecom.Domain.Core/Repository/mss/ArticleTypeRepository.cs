using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ArticleTypeRepository
    {
        public IList<ArticleType> GetList_ArticleTypeAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ArticleType.ToList();
                }
                catch
                {
                    return new List<ArticleType>();
                }
            }
        }

        public IList<ArticleType> GetList_ArticleTypeAll(bool isActive, bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.ArticleType.Where(a => a.IsActive == isActive && a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<ArticleType>();
                }
            }
        }

        public ArticleType Get_ArticleTypeById(long articleTypeId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.ArticleType.Find(articleTypeId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(ArticleType articleType)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.ArticleType.Add(articleType);
                    articleType.DateCreated = DateTime.Now;
                    _data.SaveChanges();
                    articleType.Code = articleType.ArticleTypeId;
                    _data.SaveChanges();
                    return articleType.ArticleTypeId;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public bool Update(ArticleType articleType)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    if (entities.ArticleType.Where(x => x.Code == articleType.Code & x.LanguageCode == articleType.LanguageCode).ToList().Count > 0)
                    {
                        foreach (ArticleType item in entities.ArticleType.Where(x => x.Code == articleType.Code).ToList())
                        {
                            ArticleType ArticleTypeToUpdate;
                            ArticleTypeToUpdate = entities.ArticleType.Where(x => x.Code == item.Code & x.LanguageCode == item.LanguageCode).FirstOrDefault();
                            if (item.Code == articleType.Code & item.LanguageCode == articleType.LanguageCode)
                            {

                                ArticleTypeToUpdate.ArticleTypeName = articleType.ArticleTypeName ?? ArticleTypeToUpdate.ArticleTypeName;
                            }

                            // Update Date
                            ArticleTypeToUpdate.DateModified = DateTime.Now; //Not update DateCreated
                            ArticleTypeToUpdate.IsActive = articleType.IsActive ?? ArticleTypeToUpdate.IsActive;
                            ArticleTypeToUpdate.IsDeleted = articleType.IsDeleted ?? ArticleTypeToUpdate.IsDeleted;

                            entities.SaveChanges();
                        }

                        return true;
                    }
                    else
                    {
                        articleType.ArticleTypeId = 0;
                        entities.ArticleType.Add(articleType);
                        articleType.DateCreated = DateTime.Now;
                        entities.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public ArticleType Get_ArticleTypeByCodeandLanguageCode(long id, string lang)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    List<ArticleType> temp = _data.ArticleType.Where(a => a.Code == id & a.LanguageCode == lang).ToList();
                    if (temp.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return temp[0];
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public IList<ArticleType> GetList_ArticleTypeIndex()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    List<ArticleType> result = new List<ArticleType>();
                    foreach (var item in _data.ArticleType)
                    {
                        if (result.Where(x => x.Code == item.Code).ToList().Count == 0)
                        {
                            result.Add(item);
                        }
                    }

                    return result;
                }
                catch
                {
                    return new List<ArticleType>();
                }
            }
        }
    }
}
