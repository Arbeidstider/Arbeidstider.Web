using System.Configuration;
using System.Web.Mvc;
using Arbeidstider.AppHost.App_Start;
using Arbeidstider.AppHost.AuthProviders;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataObjects.DAO;
using Arbeidstider.Repository;
using Arbeidstider.ServiceModels;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.Redis;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppHost), "Start")]

//IMPORTANT: Add the line below to RouteConfig.RegisterRoutes(RouteCollection) in the Global.asax:
//routes.IgnoreRoute("api/{*pathInfo}"); 
//More info on how to integrate with MVC: https://github.com/ServiceStack/ServiceStack/wiki/Mvc-integration

/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace Arbeidstider.AppHost.App_Start
{
    //A customizeable typed UserSession that can be extended with your own properties
    //To access ServiceStack's Session, Cache, etc from MVC Controllers inherit from ControllerBase<CustomUserSession>

    public class AppHost
        : AppHostBase
    {
        public AppHost() //Tell ServiceStack the name and where to find your web services
            : base("Arbeidstider API", typeof(Arbeidstider.ServiceInterfaces.EmployeeService).Assembly) { }

        private void EnableCors()
        {
            this.GlobalRequestFilters.Add((httpReq, httpRes, requestDto) =>
                {
                    //Handles Request and closes Responses after emitting global HTTP Headers
                    //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
                    if (httpReq.Verb == "OPTIONS")
                    {
                        httpRes.AddHeader("Access-Control-Allow-Origin", "*");
                        httpRes.AddHeader("Access-Control-Allow-Methods",
                                          "POST, GET, OPTIONS");
                        httpRes.AddHeader("Access-Control-Allow-Headers",
                                          "X-Requested-With, Content-Type, Session-Id, Authorization");
                        httpRes.AddHeader("Access-Control-Max-Age", "1000");
                        httpRes.EndRequest(); //   extension method
                    }
                });
        }

        public override void Configure(Funq.Container container)
        {
            //ConfigureCors();
            ConfigureErrorHandling();
            //Set JSON web services to return idiomatic JSON camelCase properties
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = false;

            //Configure User Defined REST Paths
            //Uncomment to change the default ServiceStack configuration
            SetConfig(new HostConfig
            {
                DebugMode = true,
                HandlerFactoryPath = "/api",
                EnableFeatures = Feature.All.Remove(Feature.Html),
                DefaultContentType = "application/json"
            });
            SuppressFormsAuthenticationRedirectModule.PathToSupress = "/api";

            //EnableCors();
            //Enable Authentication
            ConfigureAuth(container);
            //ConfigureIoC();


            //container.RegisterVj(typeof (IEmployeeRepository));
            //container.RegisterAutoWiredType(typeof (IScheduleRepository));
            //container.RegisterAutoWiredType(typeof (ITimesheetRepository));

            container.RegisterAutoWiredType(typeof(EmployeeRepository), typeof(IRepository<EmployeeDAO>));
            container.RegisterAutoWiredType(typeof(TimesheetRepository), typeof(IRepository<TimesheetDAO>));
            container.RegisterAutoWiredType(typeof(ScheduleRepository), typeof(IRepository<ScheduleDAO>));

            //Register all your dependencies
            container.Register<IRedisClientsManager>(
               new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUrl"]));
            container.Register<ICacheClient>(c =>
                                             (ICacheClient)c.Resolve<IRedisClientsManager>()
                                                             .GetCacheClient());

            //Requires ConnectionString configured in Web.Config
            var connectionString = ConfigurationManager.ConnectionStrings["Auth-Debug"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>())).ReusedWithin(ReuseScope.Container);

            container.Register<IUserAuthRepository>(c =>
                new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));


            //Set MVC to use the same Funq IOC as ServiceStack
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
        }

        private void ConfigureErrorHandling()
        {
            this.UncaughtExceptionHandlers.Add((req, res, operationName, ex) =>
              {
                  res.StatusCode = 400;
                  res.Write(string.Format("Exception {0}", ex.GetType().Name));
                  res.EndRequest(skipHeaders: true);
              });
            //this.CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/notfound");
            //this.CustomErrorHttpHandlers[HttpStatusCode.Unauthorized] = new RazorHandler("/login");
        }

        /* Uncomment to enable ServiceStack Authentication and CustomUserSession */
        private void ConfigureAuth(Funq.Container container)
        {
            var appSettings = new AppSettings();
            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new EmployeeSession(), 
                new IAuthProvider[] {
					new BrowserAuthProvider(), 
                    //new MobileAuthProvider()
            }) { HtmlRedirect = 
                null });
            //Default route: /register
            //Plugins.Add(new RegistrationFeature()); 
            Plugins.Add(new RequestInfoFeature());
        }

        public static void Start()
        {
            //LogManager.LogFactory = new EventLogFactory("ServiceStack.Logging.Tests", "Application");
            LogManager.LogFactory = new NLogFactory(); //Also runs log4net.Config.XmlConfigurator.Configure()
            new AppHost().Init();
        }
    }
}
