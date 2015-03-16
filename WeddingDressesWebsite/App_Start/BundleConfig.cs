﻿using System.Web;
using System.Web.Optimization;

namespace WeddingDressesWebsite
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

			bundles.Add(new ScriptBundle("~/bundles/gallery/scripts").Include(
			"~/Scripts/photoswipe.min.js",
			"~/Scripts/photoswipe-ui-default.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/bundles/css").Include(
					  "~/Content/css/bootstrap-rtl.css",
					  "~/Content/css/Site.css",
					  "~/Content/css/bootstrap.customize.css"));

			bundles.Add(new StyleBundle("~/bundles/gallery/css").Include(
					  "~/Content/css/photoswipe.css",
					  "~/Content/css/default-skin/default-skin.css"));

        }
	}
}
