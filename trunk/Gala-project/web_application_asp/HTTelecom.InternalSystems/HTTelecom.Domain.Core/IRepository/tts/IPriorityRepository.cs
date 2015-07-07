/**********************************************************************************************************
 * webpage name: IPriorityRepository.cs
 * originator: THIEN TRAN
 * date: 23/03/2014
 * adapt from: 
 * description: Priority interface.

 * update history:
 * - 17/11/2014: Add methods.

 **********************************************************************************************************/
using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.tts
{
    public interface IPriorityRepository
    {
        IList<Priority> GetAll();
        IList<Priority> GetList_IsDeleted(bool isDeleted);
        IList<Priority> GetList_IsActived(bool isActived);
        Priority GetById(long priorityId);
        long Insert(Priority priority);
        bool Update(Priority priority);
        bool Remove(long priorityId);
    }
}
