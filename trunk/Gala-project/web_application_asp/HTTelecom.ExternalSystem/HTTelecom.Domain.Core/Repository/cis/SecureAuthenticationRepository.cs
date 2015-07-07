using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class SecureAuthenticationRepository
    {
        public List<SecureAuthentication> GetAll(bool IsDeleted)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.SecureAuthentication.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<SecureAuthentication>();
            }
        }

        public SecureAuthentication getById(long id)
        {
            try
            {
                CIS_DBEntities _data = new CIS_DBEntities();
                return _data.SecureAuthentication.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
