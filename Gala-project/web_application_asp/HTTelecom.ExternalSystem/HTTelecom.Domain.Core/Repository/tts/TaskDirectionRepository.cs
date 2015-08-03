using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class TaskDirectionRepository
    {
        public TaskDirection GetBy(string TaskFormCode, bool? IsValid, int? OrderQueue)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var itemLst = _data.TaskDirection.Where(n => n.TaskFormCode.Contains(TaskFormCode) == true && n.OrderQueue == OrderQueue).ToList();
                var item = itemLst.Where(n => n.IsValid == IsValid).FirstOrDefault();
                return item;
            }
            catch
            {
                return null;
            }
        }

        public TaskDirection GetById(long TaskDirectionId)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.TaskDirection.Find(TaskDirectionId);
            }
            catch
            {
                return null;
            }
        }
    }
}
