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
                    int LstProduct = _ProductRepository.GetListByCategory(item.CategoryId).Count;
                    var lstCategoryChildren = GetListChildrenCategoryByCategoryId(item.CategoryId);
                    foreach (var itemCate in lstCategoryChildren)
                    {
                        LstProduct += _ProductRepository.GetListByCategory(itemCate.CategoryId).Count;
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
                    var lstCateLv1 = _data.Category.Where(n => n.ParentCateId == cate.CategoryId).ToList();
                    foreach (var itemLv1 in lstCateLv1)
                    {
                        lst.Add(itemLv1);
                        var lstLv2 = _data.Category.Where(n => n.ParentCateId == itemLv1.CategoryId).ToList();
                        if (lstLv2.Count > 0)
                            lst.AddRange(lstLv2);
                    }
                }
                if (cate.CateLevel == 1)
                {
                    var lstLv2 = _data.Category.Where(n => n.ParentCateId == cate.CategoryId).ToList();
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
    }
}
