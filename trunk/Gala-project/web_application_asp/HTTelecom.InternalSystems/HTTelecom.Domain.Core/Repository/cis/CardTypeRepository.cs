using HTTelecom.Domain.Core.DataContext.cis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.cis
{
    public class CardTypeRepository
    {
        private CIS_DBEntities _data;
        public CardTypeRepository()
        {
            _data = new CIS_DBEntities();
        }

        public List<CardType> GetAll(bool IsDeleted, bool IsActive)
        {
            try
            {
                return _data.CardTypes.Where(n => n.IsDeleted == IsDeleted && n.IsActive == IsActive).ToList();
            }
            catch
            {
                return new List<CardType>();
            }
        }
        public CardType GetById(long id)
        {
            try
            {
                return _data.CardTypes.Find(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
