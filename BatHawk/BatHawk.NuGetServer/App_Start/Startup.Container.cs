using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.WebApi;
using CommonServiceLocator;
using NuGet.Server.Core.Infrastructure;
using NuGet.Server.Core.Logging;
using NuGet.Server.V2;
using Owin;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace BatHawk.NuGetServer
{
    public partial class Startup
    {

        public static void ConfigureContainer(IAppBuilder app, HttpConfiguration config)
        {
            IContainer container = CreateContainer();

            app.UseAutofacMiddleware(container);

            // Set the WebApi dependency resolver to be Autofac.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Set the ServiceLocator provider to be Autofac.
            var serviceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            app.UseAutofacMiddleware(container);

        }

        private static IContainer CreateContainer()
        {
            string apiKey = ConfigurationManager.AppSettings["APIKEY"];
            string publishPackageRepository = ConfigurationManager.AppSettings["REPOSITORY"];
            string nuGetPublic = "NuGetPublic";

            // Set up a common settingsProvider to be used by all repositories. 
            // If a setting is not present in dictionary default value will be used.
            var settings = new Dictionary<string, object>();
            settings.Add("enableDelisting", false);                         //default=false
            settings.Add("enableFrameworkFiltering", false);                //default=false
            settings.Add("ignoreSymbolsPackages", true);                    //default=false
            settings.Add("allowOverrideExistingPackageOnPush", true);       //default=true
            var settingsProvider = new DictionarySettingsProvider(settings);

            var logger = new ConsoleLogger();


            ContainerBuilder builder = new ContainerBuilder();

            Assembly assembly = Assembly.GetExecutingAssembly();

            // Enable Interceptor Logger you can use Class Attribute in  Controller such as [Intercept(typeof(InterceptLogger))]
            builder.RegisterApiControllers(assembly);

            //Sets up three repositories with seperate packages in each feed. These repositories are used by our controllers.
            //In a real world application the repositories will probably be inserted through DI framework, or created in the controllers constructor.
            //builder.Register(c => NuGetV2WebApiEnabler.CreatePackageRepository(PrivatePackageRepository, settingsProvider, logger))
            //    .Keyed<IServerPackageRepository>(NugetPrivate);
            builder.Register(c => NuGetV2WebApiEnabler.CreatePackageRepository(publishPackageRepository, settingsProvider, logger))
                .Keyed<IServerPackageRepository>(nuGetPublic);
            //builder.Register(c => NuGetV2WebApiEnabler.CreatePackageRepository(VeryPublishPackageRepository, settingsProvider, logger))
            //   .Keyed<IServerPackageRepository>(NuGetVeryPublic);
    
            builder.Register(c => new ApiKeyPackageAuthenticationService(true, apiKey)).As<IPackageAuthenticationService>()
                .InstancePerDependency();

            builder.RegisterType<NugetService>();


            // You can register controllers all at once using assembly scanning...
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();


            return container;
        }



    }
}