using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HTTelecom.WebUI.eCommerce
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            //AreaRegistration.RegisterAllAreas();

            ////WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterAuth();
        }
        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();
        //    Response.Clear();

        //    HttpException httpException = exception as HttpException;

        //    if (httpException != null)
        //    {
        //        string action;

        //        switch (httpException.GetHttpCode())
        //        {
        //            case 404:
        //                // page not found
        //                action = "HttpError404";
        //                break;
        //            case 500:
        //                // server error
        //                action = "HttpError500";
        //                break;
        //            default:
        //                action = "General";
        //                break;
        //        }

        //        // clear error on server
        //        //Khi error sẽ vào đây
        //        Server.ClearError();
        //        //byte[] param_all = Request.BinaryRead(Request.ContentLength);
        //        //string allRequest = System.Text.Encoding.ASCII.GetString(param_all);
        //        //HTTelecom.Domain.Core.Repository.ops.LogRepository _LogRepository = new HTTelecom.Domain.Core.Repository.ops.LogRepository();
        //        //HTTelecom.Domain.Core.DataContext.ops.Log lgs = new HTTelecom.Domain.Core.DataContext.ops.Log();
        //        //lgs.OrderCode = Request["order_id"];
        //        //lgs.Status = Request.Params["transaction_status"];
        //        //lgs.DateCreated = DateTime.Now;
        //        //lgs.Description = "00000000: " + Request.HttpMethod + " - " + allRequest + "------Errrrrrrrrrrrr: - " + exception.Message;
        //        //_LogRepository.Create(lgs);
        //        //Response.Redirect(String.Format("Error/Error?action=" + action + "&message=" + System.Uri.EscapeDataString(exception.Message)));
        //        Response.Redirect(Request.Url.Scheme + "://" + Request.Url.Host + ":" + Request.Url.Port + String.Format("/Error/Error?action=" + action + "&message=" + System.Uri.EscapeDataString(exception.Message)));
        //    }
        //}
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    //if (Request.IsSecureConnection)
        //    //{
        //    //    Response.AddHeader("Strict-Transport-Security", "max-age=31536000");
        //    //}

        //}
    }
}