﻿using System.Web;
using System.Web.Optimization;

namespace WebСourseProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            // "~/Scripts/jquery.validate*"));

          //  bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
           //             "~/Scripts/jquery-{version}.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

               bundles.Add(new StyleBundle("~/Content/css").Include(
                           "~/Content/bootstrap.css",
                           "~/Content/site.css",
                           "~/Content/simple-sidebar.css",
                           "~/Content/stylesheet.css"));
             }
           /*   bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/simple-sidebar.css"));
        }*/
    }
}
