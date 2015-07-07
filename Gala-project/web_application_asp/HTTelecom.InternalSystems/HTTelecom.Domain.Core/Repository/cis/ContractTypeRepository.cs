using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class ContractTypeRepository
    {
        private CIS_DBEntities _data;
        public ContractTypeRepository()
        {
            _data = new CIS_DBEntities();
        }
    }
}
