using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.IRepository.tts
{
    public interface ITaskFormRepository
    {
        IList<TaskForm> GetAll();
        IList<TaskForm> GetList_IsDeleted(bool isDeleted);
        IList<TaskForm> GetList_IsActived(bool isActived);
        TaskForm GetById(long taskFormId);
        long Insert(TaskForm taskForm);
        bool Update(TaskForm taskForm);
        bool Remove(long taskFormId);

        TaskForm GetByCode(string Code);
    }
}
