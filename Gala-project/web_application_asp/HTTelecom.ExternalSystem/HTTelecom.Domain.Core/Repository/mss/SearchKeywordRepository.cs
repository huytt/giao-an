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
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    var rs = _data.SearchKeyword.Where(n => n.Keyword.ToUpper() == q.ToUpper()).ToList();
                    if (rs.Count > 0)
                        return true;
                    else return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void EditHitCount(string q)
        {
            try
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
            catch
            {
            }
        }

        public long Create(SearchKeyword sK)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    _data.SearchKeyword.Add(sK);
                    _data.SaveChanges();
                    return sK.SearchKeywordId;
                }
            }
            catch
            {
                return -1;
            }
        }

        public List<SearchKeyword> GetTop(int p)
        {
            try
            {
                using (MSS_DBEntities _data = new MSS_DBEntities())
                {
                    return _data.SearchKeyword.OrderByDescending(n => n.HitCount).Take(5).ToList();
                }
            }
            catch
            {
                return new List<SearchKeyword>();
            }
        }
    }
}
