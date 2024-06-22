using System.Web.Optimization;

public class BundleConfig
{
    public static void RegisterBundles(BundleCollection bundles)
    {
        // Bundle and include CSS files without minification
        bundles.Add(new StyleBundle("~/bundles/css").Include(
                  "~/Content/frontend/css/plugins/animation.css",
                  "~/Content/frontend/css/plugins/bootstrap.min.css",
                  "~/Content/frontend/css/plugins/flaticon.css",
                  "~/Content/frontend/css/plugins/font-awesome.css",
                  "~/Content/frontend/css/plugins/iconfont.css",
                  "~/Content/frontend/css/plugins/ion.rangeSlider.min.css",
                  "~/Content/frontend/css/plugins/light-box.css",
                  "~/Content/frontend/css/plugins/line-icons.css",
                  "~/Content/frontend/css/plugins/slick-theme.css",
                  "~/Content/frontend/css/plugins/slick.css",
                  "~/Content/frontend/css/plugins/snackbar.min.css",
                  "~/Content/frontend/css/plugins/themify.css",
                  "~/Content/frontend/css/styles.css").Include("~/Content/frontend/css", new CssRewriteUrlTransform()));

        // JavaScript bundles without minification
        bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Content/frontend/js/jquery.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                    "~/Content/frontend/js/popper.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Content/frontend/js/bootstrap.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/ionrangeslider").Include(
                    "~/Content/frontend/js/ion.rangeSlider.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/slick").Include(
                    "~/Content/frontend/js/slick.js"));

        bundles.Add(new ScriptBundle("~/bundles/sliderbg").Include(
                    "~/Content/frontend/js/slider-bg.js"));

        bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                    "~/Content/frontend/js/lightbox.js"));

        bundles.Add(new ScriptBundle("~/bundles/smoothproducts").Include(
                    "~/Content/frontend/js/smoothproducts.js"));

        bundles.Add(new ScriptBundle("~/bundles/snackbar").Include(
                    "~/Content/frontend/js/snackbar.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/styleswitcher").Include(
                    "~/Content/frontend/js/jQuery.style.switcher.js"));

        bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                    "~/Content/frontend/js/custom.js"));

        // Disable optimizations to avoid minification
        BundleTable.EnableOptimizations = false;
    }
}
