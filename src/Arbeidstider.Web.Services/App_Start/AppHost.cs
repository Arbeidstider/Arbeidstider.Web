using System.Configuration;
using Arbeidstider.Web.Services.App_Start;
using Arbeidstider.Web.Services.ServiceModels;
using Arbeidstider.Web.Services2.App_Start;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
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
	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("Arbeidstider Web Services", typeof(AppHost).Assembly) { }

		public override void Configure(Funq.Container container)
		{
            container.Register<ICacheClient>(new MemoryCacheClient());
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
		
			//Configure User Defined REST Paths
		    Routes
		        .Add<Timesheets>("/timesheets", "GET")
    		    .Add<CreateTimesheet>("/timesheet/create", "POST")
    		    .Add<UpdateTimesheet>("/timesheet/update", "POST");
		}

		/* Example ServiceStack Authentication and CustomUserSession */
		private void ConfigureAuth(Funq.Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new CustomUserSession(),
				new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
					new FacebookAuthProvider(appSettings), 
					new TwitterAuthProvider(appSettings), 
					new BasicAuthProvider(appSettings), 
				})); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature()); 

			//Requires ConnectionString configured in Web.Config
			var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            container.Resolve<IUserAuthRepository>().InitSchema();
		}

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}
