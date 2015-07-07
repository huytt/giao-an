using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class Category_MultiLangRepository
    {
        public List<Category_MultiLang> GetAll(bool IsDeleted, bool IsActive)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Category_MultiLang.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
                }
                catch
                {
                    return new List<Category_MultiLang>();
                }
            }
        }
        public Category_MultiLang GetByLanguage(long CategoryId, string lang)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                try
                {
                    return _data.Category_MultiLang.Where(n => n.IsDeleted == false && n.IsActive == true && n.LanguageCode == lang && n.CategoryId == CategoryId).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
