using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.Repository.ops
{
    public class TransactionStatusRepository : ITransactionStatusRepository
    {
        private OPS_DBEntities _OPSDb;
        public TransactionStatusRepository()
        {
            _OPSDb = new OPS_DBEntities();
        }
    }
}
