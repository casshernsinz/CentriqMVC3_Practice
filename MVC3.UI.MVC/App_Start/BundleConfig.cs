using System.Web.Optimization;

namespace MVC3.UI.MVC
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

			//REMOVED
			//bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
			//          "~/Scripts/bootstrap.js",
			//          "~/Scripts/respond.js"));


			//This new bundle replaces previous ones
			bundles.Add(new ScriptBundle("~/bundles/template").Include(
				"~/Scripts/js/jquery.min.js",
				"~/Scripts/js/bootstrap.min.js",
				"~/Scripts/js/main.js"
				));
			;
			//REMOVED to replace with template CSS -see below
			//bundles.Add(new StyleBundle("~/Content/css").Include(
			//          "~/Content/bootstrap.css",
			//          "~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
			 "~/Content/css/bootstrap.min.css",
			 "~/Content/css/font-awesome.min.css",
             "~/Content/PagedList.css",//Added after is was brought in with NuGet
			 "~/Content/css/style.css"));
		}
    }
}
