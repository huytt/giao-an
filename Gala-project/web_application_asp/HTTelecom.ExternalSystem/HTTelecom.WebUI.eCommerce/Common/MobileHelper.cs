using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTelecom.WebUI.eCommerce.Common
{
    public class MobileHelper
    {
        private static string[] mobileDevices = new string[] {"iphone","ppc",
                                                      "windows ce","blackberry",
                                                      "opera mini","mobile","palm",
                                                      "portable","opera mobi" };
        public static bool IsMobileDevice(string userAgent)
        {
            userAgent = userAgent.ToLower();
            return mobileDevices.Any(x => userAgent.Contains(x));
        }
    }
}