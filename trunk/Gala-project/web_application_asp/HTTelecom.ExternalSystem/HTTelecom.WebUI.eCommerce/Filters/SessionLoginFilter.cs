using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HTTelecom.WebUI.eCommerce.Filters
{
    public class SessionLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["sessionGala"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Customer" },
                                          { "action", "Login" }
                                         });
            }
            base.OnActionExecuting(filterContext);
        }

    }
    public class SessionVendorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["sessionVendorGala"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Customer" },
                                          { "action", "Login" }
                                         });
            }
            base.OnActionExecuting(filterContext);
        }

    }
    public class WhitespaceFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //var request = filterContext.HttpContext.Request;
            //var response = filterContext.HttpContext.Response;
            //if (response.Filter == null) return;
            //response.Filter = new WhiteSpaceFilter(response.Filter, s =>
            //{
            //    //s = Regex.Replace(s, @"\s+", " ");
            //    s = Regex.Replace(s, @"\s*\n\s*", "\n");
            //    s = Regex.Replace(s, @"\s*\>\s*\<\s*", "><");
            //    //s = Regex.Replace(s, @"<!--(.*?)-->", "");
            //    //s = Regex.Replace(s, @"(?s)<!--((?:(?!</?pre\b).)*?)-->(?!(?:(?!</?pre\b).)*</pre>)", "");
            //    //var firstEndBracketPosition = s.IndexOf(">");
            //    //if (firstEndBracketPosition >= 0)
            //    //{
            //    //    s = s.Remove(firstEndBracketPosition, 1);
            //    //    s = s.Insert(firstEndBracketPosition, ">");
            //    //}
            //    return s;
            //});

        }
        public class WhiteSpaceFilter : Stream
        {
            private Stream _shrink;
            private Func<string, string> _filter;
            public WhiteSpaceFilter(Stream shrink, Func<string, string> filter)
            {
                _shrink = shrink;
                _filter = filter;
            }
            public override bool CanRead { get { return true; } }
            public override bool CanSeek { get { return true; } }
            public override bool CanWrite { get { return true; } }
            public override void Flush() { _shrink.Flush(); }
            public override long Length { get { return 0; } }
            public override long Position { get; set; }
            public override int Read(byte[] buffer, int offset, int count)
            {
                return _shrink.Read(buffer, offset, count);
            }
            public override long Seek(long offset, SeekOrigin origin)
            {
                return _shrink.Seek(offset, origin);
            }
            public override void SetLength(long value)
            {
                _shrink.SetLength(value);
            }
            public override void Close()
            {
                _shrink.Close();
            }
            public override void Write(byte[] buffer, int offset, int count)
            {
                byte[] data = new byte[count];
                Buffer.BlockCopy(buffer, offset, data, 0, count);
                string s = Encoding.Default.GetString(buffer);
                s = _filter(s);
                byte[] outdata = Encoding.Default.GetBytes(s);
                _shrink.Write(outdata, 0, outdata.GetLength(0));
            }
        }

    }
}