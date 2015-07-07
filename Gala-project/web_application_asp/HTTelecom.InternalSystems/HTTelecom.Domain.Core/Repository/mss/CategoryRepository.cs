using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class CategoryRepository
    {
        public IList<Category> GetList_CategoryAll()
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Category.ToList();
                }
                catch
                {
                    return new List<Category>();
                }
            }
        }

        public IList<Category> GetList_Category_IsDeleted(bool isDeleted)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Category.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Category>();
                }
            }
        }

        public IList<Category> GetList_Category_IsActive(bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Category.Where(a => a.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Category>();
                }
            }
        }

        public IList<Category> GetList_Category_IsDeleted_IsActive(bool IsDeleted, bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Category.Where(a => a.IsDeleted == IsDeleted).Where(b => b.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Category>();
                }
            }
        }

        public Category Get_CategoryById(long CategoryId)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.Configuration.ProxyCreationEnabled = false;
                _data.Configuration.LazyLoadingEnabled = false;
                try
                {
                    return _data.Category.Find(CategoryId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public IList<Category> GetList_Category_CateLevel_ParentCateId(int CateLevel, int ParentCateId = -1)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    if (ParentCateId == -1)
                    {
                        var lang = "vi";

                        var lst = _data.Category.Where(a => a.CateLevel == CateLevel).ToList();
                        if (lang != "zh")
                        {
                            foreach (var item in lst)
                            {
                                var itemMuitl = _data.Category_MultiLang.Where(n => n.CategoryId == item.CategoryId && n.LanguageCode == lang).FirstOrDefault();
                                if (itemMuitl != null)
                                    item.CategoryName = itemMuitl.CategoryName;
                            }
                        }
                        return lst;
                        //return _data.Category.Where(a => a.CateLevel == CateLevel).ToList();
                    }
                    else
                    {
                        var lang = "vi";
                        var lst =  _data.Category.Where(n => n.CateLevel == CateLevel).Where(m => m.ParentCateId == ParentCateId).ToList();
                        if (lang != "zh")
                        {
                            foreach (var item in lst)
                            {
                                var itemMuitl = _data.Category_MultiLang.Where(n => n.CategoryId == item.CategoryId&& n.LanguageCode == lang).FirstOrDefault();
                                if (itemMuitl != null)
                                    item.CategoryName = itemMuitl.CategoryName;
                            }
                        }
                        return lst;
                    }
                }
                catch
                {
                    return new List<Category>();
                }
            }
        }

        //public Category Get_CategoryByCode(string CategoryCode)
        //{
        //    using (MSS_DBEntities _data = new MSS_DBEntities())
        //    {
        //        return _data.Category.Where(a => a.CategoryCode == CategoryCode).FirstOrDefault();
        //    }
        //}

        public long Insert(Category Category)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    Category.DateCreated = DateTime.Now;
                    Category.Alias = ExClass.Generates.generateAlias(Category.CategoryName);
                    _data.Category.Add(Category);
                    _data.SaveChanges();

                    return Category.CategoryId;
                }
                catch
                {
                    return -1;
                }

            }
        }

        public bool Update(Category Category)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    Category CategoryToUpdate;
                    CategoryToUpdate = entities.Category.Where(x => x.CategoryId == Category.CategoryId).FirstOrDefault();
                    bool flag = false;
                    if (CategoryToUpdate.IsDeleted != Category.IsDeleted)//xem có cập nhật cái này không
                    {
                        flag = true;
                    }
                    CategoryToUpdate.IsActive = Category.IsActive ?? CategoryToUpdate.IsActive;
                    CategoryToUpdate.IsDeleted = Category.IsDeleted ?? CategoryToUpdate.IsDeleted;
                    CategoryToUpdate.LogoMediaId = Category.LogoMediaId ?? CategoryToUpdate.LogoMediaId;
                    CategoryToUpdate.BannerMediaId = Category.BannerMediaId ?? CategoryToUpdate.BannerMediaId;
                    CategoryToUpdate.MetaDescription = Category.MetaDescription ?? CategoryToUpdate.MetaDescription;
                    CategoryToUpdate.MetaKeywords = Category.MetaKeywords ?? CategoryToUpdate.MetaKeywords;
                    CategoryToUpdate.MetaTitle = Category.MetaTitle ?? CategoryToUpdate.MetaTitle;
                    CategoryToUpdate.OrderNumber = Category.OrderNumber ?? CategoryToUpdate.OrderNumber;
                    CategoryToUpdate.ParentCateId = Category.ParentCateId ?? CategoryToUpdate.ParentCateId;
                    CategoryToUpdate.ProductInCategory = Category.ProductInCategory ?? CategoryToUpdate.ProductInCategory;
                    CategoryToUpdate.VisitCount = Category.VisitCount ?? CategoryToUpdate.VisitCount;
                    CategoryToUpdate.CategoryName = Category.CategoryName ?? CategoryToUpdate.CategoryName;
                    CategoryToUpdate.Description = Category.Description ?? CategoryToUpdate.Description;
                    CategoryToUpdate.Alias = Category.Alias ?? CategoryToUpdate.Alias;
                    CategoryToUpdate.CateLevel = Category.CateLevel ?? CategoryToUpdate.CateLevel;

                    entities.SaveChanges();

                    if (flag == true)
                    {
                        ProductInCategoryRepository pic = new ProductInCategoryRepository();
                        if (CategoryToUpdate.IsDeleted == true)
                        {
                            pic.IsDeleteCategory(Category.CategoryId, true);
                        }
                        else
                        {
                            pic.IsDeleteCategory(Category.CategoryId, false);
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateCategoryOrderNumber(long _categoryId, int _orderNumber)
        {
            using (MSS_DBEntities entities = new MSS_DBEntities())
            {
                try
                {
                    Category CategoryToUpdate;
                    CategoryToUpdate = entities.Category.Where(x => x.CategoryId == _categoryId).FirstOrDefault();

                    CategoryToUpdate.OrderNumber = _orderNumber;
                    entities.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void FixOrderNumberCategory(long _ParentId)
        {
            try
            {
                List<Category> temp = this.GetList_CategoryAll().Where(c => c.ParentCateId == _ParentId).ToList().OrderBy(x => x.OrderNumber).ToList();
                int i = 1;
                foreach (Category item in temp)
                {
                    this.UpdateCategoryOrderNumber(item.CategoryId, i);
                    i++;
                }
            }
            catch
            {
            }
        }

        public List<Category> GetByLang(int CateLevel, int ParentCateId, string lang)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    if (ParentCateId == -1)
                    {
                        lang = lang == null ? "vi" : lang;

                        var lst = _data.Category.Where(a => a.CateLevel == CateLevel).ToList();
                        if (lang != "zh")
                        {
                            foreach (var item in lst)
                            {
                                var itemMuitl = _data.Category_MultiLang.Where(n => n.CategoryId == ParentCateId && n.LanguageCode == lang).FirstOrDefault();
                                if (itemMuitl != null)
                                    item.CategoryName = itemMuitl.CategoryName;
                            }
                        }
                        return lst;
                    }
                    else
                    {
                        var lst = _data.Category
                            .Where(n => n.CateLevel == CateLevel
                                && n.ParentCateId == ParentCateId)

                                .ToList();
                        foreach (var item in lst)
                        {
                            var itemMuitl = _data.Category_MultiLang.Where(n => n.CategoryId == ParentCateId && n.LanguageCode == lang).FirstOrDefault();
                            if (itemMuitl != null)
                                item.CategoryName = itemMuitl.CategoryName;
                        }
                        return lst;
                    }
                }
                catch
                {
                    return new List<Category>();
                }
            }
        }
    }
}
