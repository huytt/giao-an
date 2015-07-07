using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.AdminPanel.Common
{
    public class GlobalVariables
    {
        public static string SystemCode
        {
            get { return "AP";}
        }
        public static int levelRole = 0;
        public static int levelRoleAdmin = 0;
        public static string HostNamePrivate
        {
            get { return "http://galagala.vn:9999/"; }
        }
        public static string HostNamePublic
        {
            get { return "http://localhost:3849"; }
        }
    }
}