using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.ExClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class ArticleRepository
    {

        public IList<Article> GetList_ArticleIndex()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    List<Article> result = new List<Article>();
                    foreach (var item in _data.Article)
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
                    return new List<Article>();
                }
            }
        }

        public IList<Article> GetList_ArticleAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {

                    return _data.Article.OrderByDescending(a => a.DateCreated).ToList();
                }
                catch
                {
                    return new List<Article>();
                }
            }
        }

        public IList<Article> GetList_ArticleAll(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Article.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return new List<Article>();
                }
            }
        }

        public Article Get_ArticleById(long articleId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Article.Find(articleId);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return null;
                }
            }
        }

        public Article Get_ArticleByCodeandLanguageCode(long code, string LanguageCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    List<Article> temp = _data.Article.Where(a=>a.Code == code & a.LanguageCode ==LanguageCode).ToList();
                    if(temp.Count ==0)
                    {
                        return null;
                    }else{
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

        public long Insert(Article article)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    _data.Article.Add(article);
                    article.DateCreated = DateTime.Now;
                    _data.SaveChanges();
                    article.Code =  article.ArticleId;
                    _data.SaveChanges();
                    return article.ArticleId;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("##### System Error: " + ex.InnerException.Message.ToString());
                    return -1;
                }
            }
        }

        public bool Update(Article article)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    if (entities.Article.Where(x => x.Code == article.Code & x.LanguageCode == article.LanguageCode).ToList().Count > 0)
                    {
                        foreach (Article item in entities.Article.Where(x => x.Code == article.Code).ToList())
                        {
                            Article ArticleToUpdate;
                            ArticleToUpdate = entities.Article.Where(x => x.Code == item.Code & x.LanguageCode == item.LanguageCode).FirstOrDefault();
                            if (item.Code == article.Code & item.LanguageCode == article.LanguageCode)
                            {
                               
                                ArticleToUpdate.Title = article.Title ?? ArticleToUpdate.Title;
                                ArticleToUpdate.Introduction = article.Introduction ?? ArticleToUpdate.Introduction;
                                ArticleToUpdate.ArticleContent = article.ArticleContent ?? ArticleToUpdate.ArticleContent;
                                ArticleToUpdate.Version = article.Version ?? ArticleToUpdate.Version;
                                ArticleToUpdate.MetaTitle = article.MetaTitle ?? ArticleToUpdate.MetaTitle;
                                ArticleToUpdate.MetaKeywords = article.MetaKeywords ?? ArticleToUpdate.MetaKeywords;
                                ArticleToUpdate.MetaDescription = article.MetaDescription ?? ArticleToUpdate.MetaDescription;
                            }

                            ArticleToUpdate.ArticleTypeId = article.ArticleTypeId ?? ArticleToUpdate.ArticleTypeId;
                            // Update Date
                            ArticleToUpdate.DateModified = DateTime.Now; //Not update DateCreated
                            if (article.IsPublish == true)
                            {
                                ArticleToUpdate.DatePublish = DateTime.Now;
                            }
                            if (article.IsVerified == true)
                            {
                                ArticleToUpdate.DateVerified = DateTime.Now;
                            }

                            ArticleToUpdate.ModifiedBy = article.ModifiedBy ?? ArticleToUpdate.ModifiedBy; //Not update CreatedBy                    
                            ArticleToUpdate.IsPublish = article.IsPublish ?? ArticleToUpdate.IsPublish;
                            ArticleToUpdate.IsVerified = article.IsVerified ?? ArticleToUpdate.IsVerified;
                            ArticleToUpdate.IsDeleted = article.IsDeleted ?? ArticleToUpdate.IsDeleted;

                            entities.SaveChanges();
                        }
                       
                        return true;
                    }
                    else
                    {
                        article.ArticleId = 0;
                        entities.Article.Add(article);
                        article.DateCreated = DateTime.Now;
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

        public bool UpdateVerified(long articleId, bool isVerified)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Article article = _data.Article.Where(x => x.ArticleId == articleId).FirstOrDefault();

                    if (isVerified == true)
                    {
                        article.DateVerified = DateTime.Now;
                    }
                    article.IsVerified = isVerified;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public bool UpdatePublished(long articleId, bool isPublished)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Article article = _data.Article.Where(x => x.ArticleId == articleId).FirstOrDefault();

                    if (isPublished == true)
                    {
                        article.DatePublish = DateTime.Now;
                    }
                    article.IsPublish = isPublished;
                    _data.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
        }

        public IList<Article> SearchArticle(string keywords)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    string wordASCII = Generates.ConvertUnicodeToASCII(keywords).ToLower();
                    var querylst_Article = _data.Article.ToList();
                    IList<Article> lst_Article = querylst_Article.FindAll(
                        delegate(Article article)
                        {
                            if (Generates.ConvertUnicodeToASCII(article.Title.ToLower()).Contains(wordASCII))
                                return true;
                            else
                                return false;
                        }
                    );
                    return lst_Article;
                }
                catch { return new List<Article>(); }
            }
        }
    }
}
