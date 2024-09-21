﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SmartTech
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            /*BundleConfig.RegisterBundles(BundleTable.Bundles);*/
        }

    }
}
