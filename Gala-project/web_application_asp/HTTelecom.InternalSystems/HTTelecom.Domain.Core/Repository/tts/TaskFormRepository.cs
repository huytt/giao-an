using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class TaskFormRepository : ITaskFormRepository
    {
        public IList<TaskForm> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskForms.ToList();
            }
        }

        public IList<TaskForm> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskForms.Where(a => a.IsDeleted == isDeleted).ToList();
            }
        }

        public IList<TaskForm> GetList_IsActived(bool isActived)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskForms.Where(a => a.IsActive == isActived).ToList();
            }
        }

        public TaskForm GetById(long taskFormId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskForms.Find(taskFormId);
            }
        }
        public TaskForm GetByCode(string taskFormCode)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskForms.Where(a => a.TaskFormCode == taskFormCode).FirstOrDefault();
            }
        }

        public long Insert(TaskForm taskForm)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.TaskForms.Add(taskForm);
                    entities.SaveChanges();
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
                return taskForm.TaskFormId;
            }
        }

        public bool Update(TaskForm taskForm)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var TaskFormOriginal = this.GetById(taskForm.TaskFormId);
                    if (TaskFormOriginal != null)
                    {
                        TaskFormOriginal = taskForm;
                        entities.SaveChanges();
                    }

                    return true;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }

        public bool Remove(long TaskFormId)
        {
            throw new NotImplementedException();
        }
    }
}
