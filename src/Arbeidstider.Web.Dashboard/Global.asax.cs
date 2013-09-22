using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Arbeidstider.Business.Domain;
using Arbeidstider.Business.Interfaces.Repository;
using Arbeidstider.Business.Interfaces.Services;
using Arbeidstider.Business.Repository;
using Arbeidstider.Business.Services;
using Arbeidstider.Web.App_Start;
using Autofac;
using Autofac.Integration.Mvc;
using log4net;

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

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Repositories
            builder.RegisterType<EmployerRepository>().As<IRepository<Employer>>().SingleInstance();
            // Services
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.Register(x => LogManager.GetLogger("FileLogger")).As<ILog>().SingleInstance();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}