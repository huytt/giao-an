using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.tts
{
    public interface IStatusDirectionRepository
    {
        IList<StatusDirection> GetAll();
        IList<StatusDirection> GetList_IsDeleted(bool isDeleted);
        IList<StatusDirection> GetList_IsActived(bool isActived);
        StatusDirection GetById(long statusDirectionId);
        long Insert(StatusDirection statusDirection);
        bool Update(StatusDirection statusDirection);
        bool Remove(long statusDirectionId);
        StatusDirection GetByCode(string statusDirectionCode);

        List<StatusDirection> GetByListCode(List<string> list);
    }
}
