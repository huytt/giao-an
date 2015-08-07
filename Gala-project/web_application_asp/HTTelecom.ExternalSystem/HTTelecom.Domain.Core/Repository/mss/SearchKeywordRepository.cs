using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SearchKeywordRepository
    {
        public bool IsExist(string q)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var rs = _data.SearchKeyword.Where(n => n.Keyword.ToUpper() == q.ToUpper()).ToList();
                if (rs.Count > 0)
                    return true;
                else return false;
            }
        }

        public void EditHitCount(string q)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                var rs = _data.SearchKeyword.Where(n => n.Keyword.ToUpper() == q.ToUpper()).FirstOrDefault();
                if (rs != null)
                {
                    rs.HitCount++;
                    _data.SaveChanges();
                }
            }
        }

        public long Create(SearchKeyword sK)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                _data.SearchKeyword.Add(sK);
                _data.SaveChanges();
                return sK.SearchKeywordId;
            }
        }

        public List<SearchKeyword> GetTop(int p)
        {
            using (MSS_DBEntities _data = new MSS_DBEntities())
            {
                return _data.SearchKeyword.OrderByDescending(n => n.HitCount).Take(5).ToList();
            }
        }
    }
}
