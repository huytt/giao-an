using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.mss
{
    public interface ISearchRepository
    {
        IList<Product> SearchProduct(string keyword, int count, bool column1, bool column2, bool column3, bool column4, bool column5, bool column6);
        IList<Product> SearchProductSimple(string keyword, int count);
        IList<Store> SearchStoreSimple(string keyword, int count);
        IList<Store> SearchStore(string keyword, int count, bool column1, bool column2, bool column3, bool column4, bool column5, bool column6);
    }
}
