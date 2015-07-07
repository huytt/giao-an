using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class BankRepository
    {
        public List<Bank> GetAll(bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Bank.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<Bank>();
            }
        }
    }
}
