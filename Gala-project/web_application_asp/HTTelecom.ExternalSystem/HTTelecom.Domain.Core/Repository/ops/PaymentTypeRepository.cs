using HTTelecom.Domain.Core.DataContext.ops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ops
{
    public class PaymentTypeRepository
    {
        public List<PaymentType> GetAll(bool IsDeleted)
        {
            try
            {
                OPS_DBEntities _data = new OPS_DBEntities();
                return _data.PaymentType.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<PaymentType>();
            }
        }

        public PaymentType GetByCode(string PaymentTypeCode)
        {
            using (OPS_DBEntities _data = new OPS_DBEntities())
            {
                try
                {
                    return _data.PaymentType.Where(n => n.PaymentTypeCode == PaymentTypeCode).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
           
        }
    }
}
