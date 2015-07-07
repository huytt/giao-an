using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.tts
{
   public  class SubRecordRepository
    {
       public long Create(SubRecord subRecord)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                _data.SubRecord.Add(subRecord);
                _data.SaveChanges();
                return subRecord.SubRecordId;
            }
            catch
            {
                return -1;
            }
        }
    }
}
