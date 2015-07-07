using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.tts
{
    public interface IStatusProcessRepository
    {
        IList<StatusProcess> GetAll();
        IList<StatusProcess> GetList_IsDeleted(bool isDeleted);
        IList<StatusProcess> GetList_IsActived(bool isActived);
        StatusProcess GetById(long statusProcessId);
        long Insert(StatusProcess statusProcess);
        bool Update(StatusProcess statusProcess);
        bool Remove(long statusProcessId);
    }
}
