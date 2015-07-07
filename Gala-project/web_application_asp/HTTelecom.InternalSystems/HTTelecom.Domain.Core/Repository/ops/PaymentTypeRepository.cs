using HTTelecom.Domain.Core.IRepository.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTTelecom.Domain.Core.DataContext.ops;
namespace HTTelecom.Domain.Core.Repository.ops
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        public PaymentTypeRepository()
        {

        }
        public List<PaymentType> GetAll()
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                return _OPSDb.PaymentType.ToList();
            }
            catch
            {
                return null;
            }
        }

        public PaymentType GetById(long PaymentTypeId)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                return _OPSDb.PaymentType.Find(PaymentTypeId);
            }
            catch
            {
                return null;
            }
        }

        public PaymentType GetByCode(string Code)
        {
            try
            {
                OPS_DBEntities _OPSDb = new OPS_DBEntities();
                return _OPSDb.PaymentType.Where(n=>n.PaymentTypeCode == Code).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
