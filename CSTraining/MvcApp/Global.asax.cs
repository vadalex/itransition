using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using Contracts;
using BusinessLogic;
using BusinessLogic.Search;
using System.Threading;

namespace MvcApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterContainer(Application, true);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();            
            if (exception != null)
            {
                Contracts.ILogger logger = DependencyResolver.Current.GetService<Contracts.ILogger>();                    
                logger.LogException(exception.Message, exception);
                /*
                Exception innerEx = exception.InnerException;
                while (innerEx != null) 
                {
                    logger.Error(innerEx.Message, innerEx);
                    innerEx = innerEx.InnerException;
                };
                */
                HandleErrorInfo handleError = new HandleErrorInfo(exception, "Home", "Index");
                Session["LastError"] = handleError;                
                Server.ClearError();
                Response.Redirect(String.Format("~/Home/Error/"));
            }            
        }        
    }
}