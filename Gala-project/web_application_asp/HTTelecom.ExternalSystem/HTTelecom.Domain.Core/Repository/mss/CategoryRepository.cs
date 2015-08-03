using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.mss;
namespace HTTelecom.Domain.Core.Repository.mss
{
    public class CategoryRepository
    {
        public List<Category> GetAll(bool IsActive, bool IsDeleted)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Category.Where(n => n.IsActive == IsActive && n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Category>();
            }
        }

        public List<Tuple<Category, int>> GetAllAndProductCount(bool IsActive, bool IsDeleted)
        {
            try
            {
                ProductRepository _ProductRepository = new ProductRepository();
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = _data.Category.Where(n => n.IsActive == IsActive && n.IsDeleted == IsDeleted).ToList();
                List<Tuple<Category, int>> lstCategory = new List<Tuple<Category, int>>();
                foreach (var item in lst)
                {
                    int LstProduct = _ProductRepository.GetListByCategory(item.CategoryId).GroupBy(n => n.GroupProductId).Select(g => g.First()).ToList().Count;
                    var lstCategoryChildren = GetListChildrenCategoryByCategoryId(item.CategoryId);
                    foreach (var itemCate in lstCategoryChildren)
                    {
                        int cout_temp = _ProductRepository.GetListByCategory(itemCate.CategoryId).GroupBy(n => n.GroupProductId).Select(g => g.First()).ToList().Count;
                        LstProduct += cout_temp;
                    }
                    lstCategory.Add(new Tuple<Category, int>(item, LstProduct));
                }
                return lstCategory;
            }
            catch
            {
                return new List<Tuple<Category, int>>();
            }
        }

        public List<Category> GetListChildrenCategoryByCategoryId(long CategoryId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                Category cate = _data.Category.Find(CategoryId);
                var lst = new List<Category>();
                if (cate.CateLevel == 0)
                {
                    var lstCateLv1 = _data.Category.Where(n => n.ParentCateId == cate.CategoryId && n.IsActive == true && n.IsDeleted == false).ToList();
                    foreach (var itemLv1 in lstCateLv1)
                    {
                        lst.Add(itemLv1);
                        var lstLv2 = _data.Category.Where(n => n.ParentCateId == itemLv1.CategoryId && n.IsActive == true && n.IsDeleted == false).ToList();
                        if (lstLv2.Count > 0)
                            lst.AddRange(lstLv2);
                    }
                }
                if (cate.CateLevel == 1)
                {
                    var lstLv2 = _data.Category.Where(n => n.ParentCateId == cate.CategoryId && n.IsActive == true && n.IsDeleted == false).ToList();
                    if (lstLv2.Count > 0)
                        lst.AddRange(lstLv2);
                }
                return lst;
            }
            catch
            {
                return new List<Category>();
            }
        }


        public Category GetById(long CategoryId)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Category.Find(CategoryId);
            }
            catch
            {
                return null;
            }
        }

        public List<Category> GetCateLevel(int number)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.Category.Where(n => n.CateLevel == number && n.IsActive == true && n.IsDeleted == false).ToList();
            }
            catch
            {
                return new List<Category>();
            }
        }

        public Tuple<Category, Category> GetListParentCategory(Category categoryId, string lang)
        {
            var _Category_MultiLangRepository = new Category_MultiLangRepository();
            if (categoryId.CateLevel > 0 && categoryId.ParentCateId != null && categoryId.ParentCateId != 0)
            {
                var cate1 = GetById(Convert.ToInt64(categoryId.ParentCateId));
                if (cate1 != null)
                {
                    var cate1_mutil = _Category_MultiLangRepository.GetByLanguage(cate1.CategoryId, lang);
                    cate1.CategoryName = cate1_mutil == null ? cate1.CategoryName : cate1_mutil.CategoryName;
                    cate1.Alias = cate1_mutil == null ? cate1.Alias : cate1_mutil.Alias;
                }
                Category cate0 = null;
                if (cate1 != null && cate1.CateLevel > 0)
                    cate0 = GetById(Convert.ToInt64(cate1.ParentCateId));
                if (cate0 != null)
                {
                    var cate0_mutil = _Category_MultiLangRepository.GetByLanguage(cate0.CategoryId, lang);
                    cate0.CategoryName = cate0_mutil == null ? cate0.CategoryName : cate0_mutil.CategoryName;
                    cate0.Alias = cate0_mutil == null ? cate0.Alias : cate0_mutil.Alias;
                }
                return new Tuple<Category, Category>(cate0, cate1);
            }
            return new Tuple<Category, Category>(null, null);
        }

        public List<Tuple<Category, int>> GetAllAndProductCount(long categoryId, string lang)
        {
            try
            {
                ProductRepository _ProductRepository = new ProductRepository();
                MSS_DBEntities _data = new MSS_DBEntities();
                var lst = GetListChildrenCategoryByCategoryId(categoryId);
                lst.Add(GetById(categoryId));
                List<Tuple<Category, int>> lstCategory = new List<Tuple<Category, int>>();
                Category_MultiLangRepository _Category_MultiLangRepository = new Category_MultiLangRepository();
                foreach (var item in lst)
                {
                    int LstProduct = _ProductRepository.GetListByCategory(item.CategoryId).GroupBy(n => n.GroupProductId).Select(g => g.First()).ToList().Count;
                    var lstCategoryChildren = GetListChildrenCategoryByCategoryId(item.CategoryId);
                    foreach (var itemCate in lstCategoryChildren)
                    {
                        int cout_temp = _ProductRepository.GetListByCategory(itemCate.CategoryId).GroupBy(n => n.GroupProductId).Select(g => g.First()).ToList().Count;
                        LstProduct += cout_temp;
                    }
                    var itemLang = _Category_MultiLangRepository.GetByLanguage(item.CategoryId, lang);
                    if (itemLang != null)
                    {
                        item.CategoryName = itemLang.CategoryName;
                        item.Alias = itemLang.Alias;
                    }
                    
                    lstCategory.Add(new Tuple<Category, int>(item, LstProduct));
                }
                return lstCategory;
            }
            catch
            {
                return new List<Tuple<Category, int>>();
            }
        }
    }
}
