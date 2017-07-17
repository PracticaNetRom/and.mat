using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public WebApiApplication()
        {
            this.BeginRequest += WebApiApplication_BeginRequest;
        }

        private void WebApiApplication_BeginRequest(object sender, EventArgs e)
        {
            if (this.Context.Request.HttpMethod.CompareTo("OPTIONS") == 0)
            {
                this.Context.Response.StatusCode = 200;
                this.CompleteRequest();
            }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
