using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using Arbeidstider.Web.Framework.AuthProviders;
using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceInterfaces;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Host.Handlers;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Web;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppHost), "Start")]


/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace Arbeidstider.Web.Services.App_Start
{
    /*
    public class EmployeeSession : AuthUserSession
    {
        
    }
     */

    public class OptionsRequestHandler : IHttpHandler, IServiceStackHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            context.Response.Headers["Access-Control-Allow-Methods"] = "POST,GET,OPTIONS";
            context.Response.Headers["Access-Control-Allow-Headers"] =
                "X-Requested-With, Content-Type, SessionId, Authorization";
            context.Response.Headers["Access-Control-Max-Age"] = "1000";
            context.Response.End();
        }

        public Task ProcessRequestAsync(IRequest httpReq, IResponse httpRes, string operationName)
        {
            AddHeadersAndEnd(httpRes);
            return new Task(new Action(process));
        }

        public void ProcessRequest(IRequest httpReq, IResponse httpRes, string operationName)
        {
            AddHeadersAndEnd(httpRes);
        }

        private void AddHeadersAndEnd(IResponse httpRes)
        {
            httpRes.AddHeader("Access-Control-Allow-Origin", "*");
            httpRes.AddHeader("Access-Control-Max-Age", "1000");
            httpRes.AddHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
            httpRes.AddHeader("Access-Control-Allow-Headers", "X-Requested-With, Content-Type, Session-Id, Authorization");
            httpRes.EndRequest();
        }

        private void process()
        {

        }
    }


    public class AppHost : AppHostBase
    {
        public AppHost() //Tell ServiceStack the name and where to find your web services
            : base("Arbeidstider Web Services", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Funq.Container container)
        {
            EnableCors();
            ConfigureIoC(container);
            ConfigureServiceRoutes();
            ConfigureAuth(container);
        }

        private static void ConfigureIoC(Funq.Container container)
        {
            container.Register<IRedisClientsManager>(
                new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUrl"]));
            container.Register<ICacheClient>(c =>
                                             (ICacheClient)c.Resolve<IRedisClientsManager>()
                                                             .GetCacheClient())
                     .ReusedWithin(Funq.ReuseScope.None);

            var connectionString = ConfigurationManager.ConnectionStrings["Auth"].ConnectionString;

            container.Register<IDbConnectionFactory>(c =>
                                                     new OrmLiteConnectionFactory(connectionString,
                                                                                  SqlServerDialect.Provider));

            container.Register<IUserAuthRepository>(c =>
                                                    new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
        }

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
            this.CatchAllHandlers.Add((httpMethod, pathInfo, filePath) =>
                                          {
                                              if ("OPTIONS".Equals(httpMethod,
                                                                   System.StringComparison.InvariantCultureIgnoreCase))
                                                  return new OptionsRequestHandler();
                                              else return null;
                                          });
        }

        private void ConfigureServiceRoutes()
        {
            //Configure User Defined REST Paths
            Routes
                .Add<Timesheets>("/timesheets")
                .Add<Dashboard>("/getdashboard/{employeeId}")
                .Add<CreateTimesheet>("/timesheet/create", "POST")
                .Add<UpdateTimesheet>("/timesheet/update", "POST")
                .Add<RegisterEmployee>("/employee/register", "POST, OPTIONS")
                .Add<SessionRequest>("/getsession", "GET, OPTIONS")
                .Add<SessionRequest>("/verifysession", "POST");
        }

        private void ConfigureAuth(Funq.Container container)
        {
            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new EmployeeSession(),
                                        new IAuthProvider[]
                                            {
                                                new EmployeeAuthProvider()
                                            }) { HtmlRedirect = null });

            //Default route: /register
            Plugins.Add(new RegistrationFeature());
            using (var db = container.Resolve<IDbConnectionFactory>().Open())
                db.CreateTableIfNotExists();
        }

        public static void Start()
        {
            new AppHost().Init();
        }
    }
}