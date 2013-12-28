using System.Configuration;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;

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
		    ConfigureIoC(container);
		    ConfigureServiceRoutes();
		    ConfigureAuth(container);
		    EnableCors();
    	}

	    private static void ConfigureIoC(Funq.Container container)
	    {
		    container.Register<ICacheClient>(new MemoryCacheClient());

			var connectionString = ConfigurationManager.ConnectionStrings["Auth"].ConnectionString;

			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
	    }

	    private void EnableCors()
	    {
             //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
            base.SetConfig(new HostConfig
            {
                DefaultContentType = "application/json",
                GlobalResponseHeaders = {
                    { "Access-Control-Allow-Origin", "*" },
                    { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                    { "Access-Control-Allow-Headers", "Content-Type" },
                },
            });

		    Plugins.Add(new CorsFeature(
                    allowedOrigins:"*", 
                    allowedMethods:"GET, POST, PUT, DELETE, OPTIONS", 
                    allowedHeaders:"Content-Type", 
                    allowCredentials:true
                ));
	    }

	    private void ConfigureServiceRoutes()
	    {
			//Configure User Defined REST Paths
	        Routes
	            .Add<Timesheets>("/timesheets", "GET")
	            .Add<CreateTimesheet>("/timesheet/create", "POST")
	            .Add<UpdateTimesheet>("/timesheet/update", "POST")
	            .Add<SessionRequest>("/getsession", "GET");
	    }

	    private void ConfigureAuth(Funq.Container container)
	    {
			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new AuthUserSession(),
				new IAuthProvider[] {
					new BasicAuthProvider(), 
                    new CredentialsAuthProvider(), 
				})); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature());
	    }

	    public static void Start()
		{
			new AppHost().Init();
		}
	}
}