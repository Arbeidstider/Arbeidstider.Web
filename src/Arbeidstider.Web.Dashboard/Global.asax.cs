using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Web.Dashboard.App_Start;
using Arbeidstider.Web.Dashboard.Controllers;
using Arbeidstider.Web.Framework;
using Arbeidstider.Web.Framework.Scaffolding;
using Autofac.Integration.Mvc;

namespace Arbeidstider.Web.Dashboard
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
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            IoC.Initialize();
            Data.Run();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IoC.BaseContainer));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            Server.ClearError();

            int statusCode = 0;

            if (lastError == null) return;

            if (lastError.GetType() == typeof(HttpException))
            {
                statusCode = ((HttpException) lastError).GetHttpCode();
            }
            else
            {
                // Not an HTTP related error so this is a problem in our code, set status to
                // 500 (internal server error)
                statusCode = 500;
            }

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (statusCode == 500)
                routeData.Values.Add("action", "Index");
            else
                routeData.Values.Add("action", "NotFound");
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);

            IController controller = new ErrorController();

            RequestContext requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);

            controller.Execute(requestContext);
            Response.End();
        }
    }
}