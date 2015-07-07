using HTTelecom.Domain.Core.DataContext.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.mss
{
    public class AlertMessageRepository
    {
        public List<AlertMessage> GetAll(bool IsDeleted)
        {
            try
            {
                MSS_DBEntities _data = new MSS_DBEntities();
                return _data.AlertMessage.Where(n => n.IsDeleted == IsDeleted).ToList();
            }
            catch
            {
                return new List<AlertMessage>();
            }
        }
    }
}
