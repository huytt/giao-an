using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HTTelecom.WebUI.SaleMedia
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            var manifest = Revalee.Client.RecurringTasks.RecurringTaskModule.GetManifest();

            manifest.Activated += RecurringTasks_Activated;
            manifest.Deactivated += RecurringTasks_Deactivated;
            manifest.ActivationFailed += RecurringTasks_ActivationFailed;
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
        protected void RecurringTasks_Activated(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ACTIVATED");
        }

        protected void RecurringTasks_Deactivated(object sender, Revalee.Client.RecurringTasks.DeactivationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DEACTIVATED: " + e.Exception.Message);
        }

        protected void RecurringTasks_ActivationFailed(object sender, Revalee.Client.RecurringTasks.ActivationFailureEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ACTIVATION FAILURE: attempt #" + e.FailureCount.ToString());
        }
    }
}