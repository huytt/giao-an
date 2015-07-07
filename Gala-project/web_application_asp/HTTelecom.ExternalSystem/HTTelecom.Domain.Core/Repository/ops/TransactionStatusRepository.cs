using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ops
{
    public class TransactionStatusRepository
    {
        public List<TransactionStatus> GetAll(bool IsDeleted)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.TransactionStatus.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<TransactionStatus>();
            }
        }

        public TransactionStatus GetByCode(string TransactionStatusCode)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.TransactionStatus.Where(n => n.TransactionStatusCode == TransactionStatusCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
