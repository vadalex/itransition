using System.Web.Optimization;

namespace TodoWebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css").Include(
                        "~/Content/Site.css"));
            
            bundles.Add(new StyleBundle("~/content/todoliststyle").Include(
                        "~/Content/TodoList.css"));

            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                        "~/Scripts/angular/angular.js").Include(
                        "~/Scripts/angular/angular-resource.min.js").Include(
                        "~/Scripts/angular/TodoController.js"));

            bundles.Add(new ScriptBundle("~/scripts/backbone").Include(
                        "~/Scripts/backbone/underscore.js").Include(
                        "~/Scripts/backbone/backbone.js").Include(
                        "~/Scripts/backbone/todoApp.js"));

            bundles.Add(new ScriptBundle("~/scripts/react").Include(
                       "~/Scripts/react/react.js").Include(
                       "~/Scripts/react/JSXTransformer.js"));

            bundles.Add(new ScriptBundle("~/scripts/flux").Include(
                       "~/Scripts/flux/microevent.js"));


        }
    }
}