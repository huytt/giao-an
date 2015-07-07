using System.Web;
using System.Web.Optimization;

namespace HTTelecom.WebUI.eCommerce
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region remove
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            #endregion

            bundles.Add(new StyleBundle("~/www.galagala.vn/css/vs-mb").Include("~/Content/_all.css"));
            bundles.Add(new StyleBundle("~/www.galagala.vn/css/font-awesome").Include("~/Content/font/font-awesome-4.3.0/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/www.galagala.vn/css/vs-mb/all").Include("~/Content/_lib.css", "~/Content/Site.css"));

            #region customer
            bundles.Add(new ScriptBundle("~/www.galagala.vn/jquery").Include("~/Scripts/jquery-1.11.2.js"));
            bundles.Add(new ScriptBundle("~/www.galagala.vn/jqueryajax").Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/www.galagala.vn/vs-mb").Include(
                        "~/Scripts/_all.js", "~/Scripts/Galagala.js", "~/Scripts/_Js.js"));
            bundles.Add(new ScriptBundle("~/www.galagala.vn/swiper").Include(
                        "~/Scripts/swiper.min.js.js"));
            #endregion
        }
    }
}