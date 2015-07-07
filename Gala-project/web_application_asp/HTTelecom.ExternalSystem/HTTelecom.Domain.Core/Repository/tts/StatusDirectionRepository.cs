using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class StatusDirectionRepository
    {
        public StatusDirection GetByCode(string StatusDirectionCode)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.StatusDirection.Where(n => n.StatusDirectionCode == StatusDirectionCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
