using System.Web;
using System.Web.Optimization;

namespace Softvision.QA.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Layout Styles
            bundles.Add(new StyleBundle("~/bundles/layout_styles").Include(
              "~/Content/Bootstrap/css/bootstrap.min.css",
              "~/Content/pace/pace.min.css",
              "~/Content/main.css"));

            // Layout Scripts
            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                "~/Content/js/jquery-3.1.1.min.js",
                "~/Content/js/jquery-migrate-1.4.1.min.js",
                "~/Content/Bootstrap/js/bootstrap.min.js",
                "~/Content/js/ie10-viewport-bug-workaround.js",
                "~/Content/pace/pace.min.js",
                "~/Content/js/sf-main.js"));

            // Editor
            bundles.Add(new ScriptBundle("~/bundles/sk").Include(
                "~/Content/Markdown/markdown/resizer.js",
                "~/Content/Markdown/markdown/Markdown.Converter.js",
                "~/Content/Markdown/markdown/Markdown.Editor.js",
                "~/Content/Markdown/markdown/Markdown.Sanitizer.js",
                "~/Content/Markdown/markdown/Markdown.js"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
