using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class ContractRepository
    {
        public List<Contract> GetAll(bool IsActive, bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Contract.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<Contract>();
            }
        }
        public Contract GetById(long id)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.Contract.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
