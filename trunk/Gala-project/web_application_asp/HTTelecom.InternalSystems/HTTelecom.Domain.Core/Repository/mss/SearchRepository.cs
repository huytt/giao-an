using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.IRepository.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class SearchRepository : ISearchRepository
    {
        public IList<Product> SearchProduct(string keyword, int count, bool column1, bool column2, bool column3, bool column4, bool column5, bool column6)
        {
           
            IList<Product> rs = new List<Product>();
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                //if (count == -1)
                //{
                //    rs = _data.SearchProduct(keyword, column1, column2, column3, column4, column5, column6).Distinct().ToList();
                //}
                //else rs = _data.SearchProduct(keyword, column1, column2, column3, column4, column5, column6).Distinct().Take(count).ToList();
                return rs;
            }
            catch
            {
                return null;
            }
        }
        public IList<Product> SearchProductSimple(string keyword, int count)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            IList<Product> rs = new List<Product>();
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                if (count == -1)
                {
                    rs = _data.SearchProductSimple(keyword).ToList();
                }
                else rs = _data.SearchProductSimple(keyword).Distinct().Take(count).ToList();
                return rs;
            }
            catch
            {
                return null;
            }
        }
        public IList<Store> SearchStoreSimple(string keyword, int count)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            IList<Store> rs = new List<Store>();
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                if (count == -1)
                {
                    rs = _data.SearchStoreSimple(keyword).ToList();
                }
                else rs = _data.SearchStoreSimple(keyword).Distinct().Take(count).ToList();
                return rs;
            }
            catch
            {
                return null;
            }
        }
        public IList<Store> SearchStore(string keyword, int count, bool column1, bool column2, bool column3, bool column4, bool column5, bool column6)
        {
            return null;
        }
    }
}
