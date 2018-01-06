using System.Web;
using System.Web.Optimization;

namespace MvcApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/content/css").Include("~/Content/Site.css").Include("~/Content/MyCss.css").Include("~/Content/bootstrap.css"));


            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js").Include("~/Scripts/jquery.realTimeCommentsList.js"));
        }
    }
}