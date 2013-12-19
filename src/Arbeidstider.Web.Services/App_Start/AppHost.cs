using System.Configuration;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Host.Handlers;
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
	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Arbeidstider Web Services", typeof(AppHost).Assembly) { }

		public override void Configure(Funq.Container container)
		{
            //Handles Request and closes Response after emitting global HTTP Headers
            var emitGlobalHeadersHandler = new CustomActionHandler(
            (httpReq, httpRes) => httpRes.EndRequest());

            this.RawHttpHandlers.Add(httpReq =>
                httpReq.HttpMethod == HttpMethods.Options
                ? emitGlobalHeadersHandler
                : null); 

            this.PreRequestFilters.Add((httpReq, httpRes) => {
                //Handles Request and closes Responses after emitting global HTTP Headers
                if (httpReq.Verb == "OPTIONS") 
                    httpRes.EndRequest();
            });

		    container.Register<ICacheClient>(new MemoryCacheClient());

		    ConfigureServiceRoutes();
		    ConfigureAuth(container);
		    Plugins.Add(new CorsFeature());
    	}

	    private void ConfigureServiceRoutes()
	    {
			//Configure User Defined REST Paths
		    Routes
		        .Add<Timesheets>("/timesheets", "GET")
    		    .Add<CreateTimesheet>("/timesheet/create", "POST")
    		    .Add<UpdateTimesheet>("/timesheet/update", "POST");
	    }

	    private void ConfigureAuth(Funq.Container container)
	    {
			//Requires ConnectionString configured in Web.Config
			var connectionString = ConfigurationManager.ConnectionStrings["Auth"].ConnectionString;

			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

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