using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class ContractRepository
    {
         private CIS_DBEntities _data;
         public ContractRepository()
        {
            _data = new CIS_DBEntities();
        }

         public long Create(Contract contract)
         {
             try
             {
                 _data.Contracts.Add(contract);
                 _data.SaveChanges();
                 return contract.ContractId;
             }
             catch
             {
                 return -1;
             }
         }

         public bool CheckCode(string Code)
         {
             try
             {
                 var item = _data.Contracts.Where(n=>n.ContractCode.ToUpper().IndexOf(Code.ToUpper())==0).FirstOrDefault();
                 if (item != null)
                     return false;
                 else return true;
             }
             catch
             {
                 return false;
             }
         }

         public Contract GetById(long ContractId)
         {
             try
             {
                 return _data.Contracts.Find(ContractId);
             }
             catch
             {
                 return null;
             }
         }

         public List<Contract> GetAllContract()
         {
             try
             {
                 return _data.Contracts.ToList();
             }
             catch
             {
                 return null;
             }
         }
    }
}
