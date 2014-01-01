using System.Configuration;
using Arbeidstider.Web.Framework.AuthProvider;
using Arbeidstider.Web.Framework.Session;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Redis;

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
	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Arbeidstider Web Services", typeof(AppHost).Assembly) { }

		public override void Configure(Funq.Container container)
		{
		    EnableCors();
		    ConfigureIoC(container);
		    ConfigureServiceRoutes();
		    ConfigureAuth(container);
    	}

	    private static void ConfigureIoC(Funq.Container container)
	    {
		    container.Register<IRedisClientsManager>(new PooledRedisClientManager(ConfigurationManager.AppSettings["RedisUrl"]));
		    container.Register<ICacheClient>(c =>
                (ICacheClient)c.Resolve<IRedisClientsManager>()
                    .GetCacheClient())
                    .ReusedWithin(Funq.ReuseScope.None);

			var connectionString = ConfigurationManager.ConnectionStrings["Auth"].ConnectionString;

			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
	    }

	    private void EnableCors()
	    {
          base.SetConfig(new HostConfig
            {
                /*
                GlobalResponseHeaders = {
                    { "Access-Control-Allow-Origin", "*" },
                    { "Access-Control-Allow-Headers", "Content-Type, Session-Id, Authorization" },
                    { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" } 
                },
                AllowJsonpRequests = true
                 */
            });

            this.GlobalRequestFilters.Add((httpReq, httpRes, requestDto) =>
            {
                //Handles Request and closes Responses after emitting global HTTP Headers
                 //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
                if (httpReq.Verb == "OPTIONS")
                    httpRes.EndRequest();   //   extension method
            });

	        //Plugins.Add(new CorsFeature(allowedHeaders:"Content-Type, Session-Id, Authorization"));
	    }

	    private void ConfigureServiceRoutes()
	    {
			//Configure User Defined REST Paths
	        Routes
	            .Add<Timesheets>("/timesheets", "GET")
	            .Add<CreateTimesheet>("/timesheet/create", "POST")
	            .Add<UpdateTimesheet>("/timesheet/update", "POST")
	            .Add<SessionRequest>("/getsession", "GET, OPTIONS");
	    }

	    private void ConfigureAuth(Funq.Container container)
	    {
			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new EmployeeSession(), 
				new IAuthProvider[] {
					new EmployeeAuthProvider(), 
				}){HtmlRedirect = null}); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature());
	    }

	    public static void Start()
		{
			new AppHost().Init();
		}
	}
}