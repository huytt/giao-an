using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.MediaSupport.Common
{
    public class GlobalVariables
    {
        public static string SystemCode
        {
            get { return "MS"; }
        }
        public static int levelRole = 0;
        public static int levelRoleAdmin = 0;
        public static string HostNamePrivate
        {
            get { return "http://galagala.vn:9999/"; }
        }
        public static string HostNamePublic
        {
            //get { return "http://galagala.vn:8888/"; }
            get
            {
                //var scheme = Request.Url.Scheme; // will get http, https, etc.
                //var host = Request.Url.Host; // will get www.mywebsite.com
                //var port = Request.Url.Port; // will get the port
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            }
        }
    }
}