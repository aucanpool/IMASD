using System.Web;
using System.Web.Optimization;

namespace Nomina2018.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));
        }

    }
}