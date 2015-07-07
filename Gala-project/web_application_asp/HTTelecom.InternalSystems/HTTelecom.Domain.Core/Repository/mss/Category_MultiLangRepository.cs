using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class Category_MultiLangRepository
    {
        public IList<Category_MultiLang> GetList_CategoryCategoryMultiLangAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Category_MultiLang.ToList();
                }
                catch
                {
                    return new List<Category_MultiLang>();
                }
            }
        }


        public Category_MultiLang Get_CategoryByIdandLanguageCode(long _CategoryId,string LanguageCode)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    List<Category_MultiLang> temp = _data.Category_MultiLang.Where(x => x.CategoryId == _CategoryId & x.LanguageCode == LanguageCode).ToList();
                    if (temp.Count == 0)
                        return null;
                    return temp[0];
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(Category_MultiLang Category_MultiLangId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Category_MultiLangId.Alias = ExClass.Generates.generateAlias(Category_MultiLangId.CategoryName);
                    _data.Category_MultiLang.Add(Category_MultiLangId);
                    _data.SaveChanges();
                    return Category_MultiLangId.Category_MultiLangId;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public bool Update(Category_MultiLang _Category_MultiLang)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    Category_MultiLang Category_MultiLangToUpdate;
                    Category_MultiLangToUpdate = entities.Category_MultiLang.Where(x => x.Category_MultiLangId == _Category_MultiLang.Category_MultiLangId).FirstOrDefault();
           
                    Category_MultiLangToUpdate.IsActive = _Category_MultiLang.IsActive ?? Category_MultiLangToUpdate.IsActive;
                    Category_MultiLangToUpdate.IsDeleted = _Category_MultiLang.IsDeleted ?? Category_MultiLangToUpdate.IsDeleted;
                    Category_MultiLangToUpdate.Description = _Category_MultiLang.Description ?? Category_MultiLangToUpdate.Description;
                    Category_MultiLangToUpdate.MetaDescription = _Category_MultiLang.MetaDescription ?? Category_MultiLangToUpdate.MetaDescription;
                    Category_MultiLangToUpdate.MetaKeywords = _Category_MultiLang.MetaKeywords ?? Category_MultiLangToUpdate.MetaKeywords;
                    Category_MultiLangToUpdate.MetaTitle = _Category_MultiLang.MetaTitle ?? Category_MultiLangToUpdate.MetaTitle;
                    Category_MultiLangToUpdate.CategoryName = _Category_MultiLang.CategoryName ?? Category_MultiLangToUpdate.CategoryName;
                    Category_MultiLangToUpdate.Alias = ExClass.Generates.generateAlias(Category_MultiLangToUpdate.CategoryName);
                    entities.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Update(Category _Category,string LanguageCode)
        {
            if (_Category.CategoryId == 0 || _Category.CategoryId == null || LanguageCode == "")
            {
                return false;
            }
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    int temp = entities.Category_MultiLang.Where(m => m.CategoryId == _Category.CategoryId && m.LanguageCode == LanguageCode).ToList().Count;
                    Category_MultiLang temp_CateMutilang = entities.Category_MultiLang.Where(m => m.CategoryId == _Category.CategoryId && m.LanguageCode == LanguageCode).FirstOrDefault();
                    if (temp != 0)
                    {
                        Category_MultiLang Category_MultiLangToUpdate;
                        Category_MultiLangToUpdate = entities.Category_MultiLang.Find(temp_CateMutilang.Category_MultiLangId);

                        Category_MultiLangToUpdate.IsActive = _Category.IsActive ?? Category_MultiLangToUpdate.IsActive;
                        Category_MultiLangToUpdate.IsDeleted = _Category.IsDeleted ?? Category_MultiLangToUpdate.IsDeleted;
                        Category_MultiLangToUpdate.Description = _Category.Description ?? Category_MultiLangToUpdate.Description;
                        Category_MultiLangToUpdate.MetaDescription = _Category.MetaDescription ?? Category_MultiLangToUpdate.MetaDescription;
                        Category_MultiLangToUpdate.MetaKeywords = _Category.MetaKeywords ?? Category_MultiLangToUpdate.MetaKeywords;
                        Category_MultiLangToUpdate.MetaTitle = _Category.MetaTitle ?? Category_MultiLangToUpdate.MetaTitle;
                        Category_MultiLangToUpdate.CategoryName = _Category.CategoryName ?? Category_MultiLangToUpdate.CategoryName;
                        Category_MultiLangToUpdate.Alias = _Category.Alias ?? Category_MultiLangToUpdate.Alias;
                        Category_MultiLangToUpdate.Alias = ExClass.Generates.generateAlias(Category_MultiLangToUpdate.CategoryName);
                        entities.SaveChanges();
                        return true;
                    }
                    else
                    { 
                        Category_MultiLang temp_insert = new Category_MultiLang();
                        temp_insert.LanguageCode = LanguageCode;
                        temp_insert.CategoryId = _Category.CategoryId;
                        temp_insert.IsActive = _Category.IsActive;
                        temp_insert.IsDeleted = _Category.IsDeleted;
                        temp_insert.Description = _Category.Description ;
                        temp_insert.MetaDescription = _Category.MetaDescription;
                        temp_insert.MetaKeywords = _Category.MetaKeywords;
                        temp_insert.MetaTitle = _Category.MetaTitle;
                        temp_insert.CategoryName = _Category.CategoryName;
                        temp_insert.Alias = ExClass.Generates.generateAlias(temp_insert.CategoryName);
                        if (this.Insert(temp_insert) != -1)
                        {
                            return true;
                        }
                        else
                        {
                            return false; 
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
