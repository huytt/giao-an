using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTelecom.Domain.Core.DataContext.cis;
namespace HTTelecom.Domain.Core.Repository.cis
{
    public class BankRepository
    {
        private CIS_DBEntities _data;
        public BankRepository()
        {
            _data = new CIS_DBEntities();
        }

        public List<Bank> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                return _data.Banks.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<Bank>();
            }
        }
        public Bank GetById(long id)
        {
            try
            {
                return _data.Banks.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
