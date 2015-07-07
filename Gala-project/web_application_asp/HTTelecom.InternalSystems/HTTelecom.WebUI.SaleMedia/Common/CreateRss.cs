using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.SaleMedia.Common
{
    public class RSS
    {
        public string CardId{ get; set; }// AdId
        public string Status{ get; set; } // IsActive
        public string OnlineDate{ get; set; } // StartDate
        public string OnlinePeriod { get; set; } // ???
        public string ChannelId{ get; set; }  //


        public string Media{ get; set; }  //
        public string Content{ get; set; }//Descript Ads
        public string Header { get; set; }//Logo and name
        public string Title { get; set; } // AdTItle
        public string Logo{ get; set; }//Logo
        public string link { get; set; } 
    }
}