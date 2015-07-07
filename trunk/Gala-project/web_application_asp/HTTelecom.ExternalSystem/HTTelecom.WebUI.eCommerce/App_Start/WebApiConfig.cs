using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HTTelecom.WebUI.eCommerce
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.MessageHandlers.Add(new EnforceHttpsHandler());

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
    public class EnforceHttpsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // if request is local, just serve it without https
            object httpContextBaseObject;
            if (request.Properties.TryGetValue("MS_HttpContext", out httpContextBaseObject))
            {
                var httpContextBase = httpContextBaseObject as HttpContextBase;

                if (httpContextBase != null && httpContextBase.Request.IsLocal)
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }

            // if request is remote, enforce https
            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(
                    () =>
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent("HTTPS Required")
                        };

                        return response;
                    });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }

}