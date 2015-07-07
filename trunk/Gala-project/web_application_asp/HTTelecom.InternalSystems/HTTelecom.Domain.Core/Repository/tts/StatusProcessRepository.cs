using HTTelecom.Domain.Core.DataContext.tts;
using HTTelecom.Domain.Core.IRepository.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**********************************************************************************************************
 * webpage name: StatusProcessRepository.cs
 * originator: THIEN TRAN
 * date: 17/11/2014
 * adapt from: 
 * description: StatusProcess implement from IStatusProcessRepository.

 * update history:
 * - 21/11/2014: Add implement methods.

 **********************************************************************************************************/

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class StatusProcessRepository : IStatusProcessRepository
    {

        public IList<StatusProcess> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusProcesses.ToList();
                }
                catch
                {
                    return new List<StatusProcess>();
                }
            }
        }

        public IList<StatusProcess> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusProcesses.Where(a => a.IsDeleted == isDeleted).ToList();
                }
                catch
                {
                    return new List<StatusProcess>();
                }
            }
        }

        public IList<StatusProcess> GetList_IsActived(bool isActived)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusProcesses.Where(a => a.IsActive == isActived).ToList();
                }
                catch
                {
                    return new List<StatusProcess>();
                }
            }
        }

        public StatusProcess GetById(long statusProcessId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusProcesses.Find(statusProcessId);
                }
                catch
                {
                    return null;
                }
            }
        }
        public StatusProcess GetByCode(string statusProcessCode)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try { 
                return entities.StatusProcesses.Where(a => a.StatusProcessCode == statusProcessCode).FirstOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }

        public long Insert(StatusProcess statusProcess)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.StatusProcesses.Add(statusProcess);
                    entities.SaveChanges();
                    return statusProcess.StatusProcessId;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public bool Update(StatusProcess statusProcess)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var statusProcessUpdate = this.GetById(statusProcess.StatusProcessId);
                    if (statusProcessUpdate != null)
                    {
                        statusProcessUpdate = statusProcess;
                        entities.SaveChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Remove(long statusProcessId)
        {
            throw new NotImplementedException();
        }
    }
}