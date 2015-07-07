using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**********************************************************************************************************
 * webpage name: PriorityRepository.cs
 * originator: THIEN TRAN
 * date: 17/11/2014
 * adapt from: 
 * description: Priority implement from IPriorityRepository.

 * update history:
 * - 21/11/2014: Add implement methods.

 **********************************************************************************************************/

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class PriorityRepository : IPriorityRepository
    {

        public IList<Priority> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    return entities.Priorities.ToList();
                }
                catch
                {
                    return new List<Priority>();
                }
            }
        }

        public IList<Priority> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    return entities.Priorities.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<Priority>();
                }
            }
        }

        public IList<Priority> GetList_IsActived(bool isActived)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    return entities.Priorities.Where(a => a.IsActive == isActived).ToList();
                }
                catch
                {
                    return new List<Priority>();
                }
            }
        }

        public Priority GetById(long priorityId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.Priorities.Find(priorityId);
            }
        }

        public long Insert(Priority priority)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.Priorities.Add(priority);
                    entities.SaveChanges();
                    return priority.PriorityId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }

        public bool Update(Priority priority)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var priorityUpdate = this.GetById(priority.PriorityId);
                    if (priorityUpdate != null)
                    {
                        priorityUpdate = priority;
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

        public bool Remove(long priorityId)
        {
            throw new NotImplementedException();
        }
    }
}