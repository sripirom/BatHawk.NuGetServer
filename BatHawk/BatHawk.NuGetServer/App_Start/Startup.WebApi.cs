using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using NuGet.Server.V2;

namespace BatHawk.NuGetServer
{
    public partial class Startup
    {
        public static void ConfigureWebApi(IAppBuilder appBuilder, HttpConfiguration config)
        {

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Simple authenticator that authorizes all users that supply a username and password. Only meant for demonstration purposes.
            appBuilder.Use(typeof(BasicAuthentication));


            //Feed that allows  read/download access for authenticated users, delete/upload is disabled (configured in controller's constructor).
            //User authentication is done by hosting environment, typical Owin pipeline or IIS (configured by attribute on controller).
            //NuGetV2WebApiEnabler.UseNuGetV2WebApiFeed(config,
            //    routeName: "NuGetAdmin",
            //    routeUrlRoot: "NuGet/admin",
            //    oDatacontrollerName: "NuGetPrivateOData");            //NuGetPrivateODataController.cs, located in Controllers\ folder

            //Feed that allows unauthenticated read/download access, delete/upload requires ApiKey (configured in controller's constructor).
            NuGetV2WebApiEnabler.UseNuGetV2WebApiFeed(config,
                routeName: "NuGetPublic",
                routeUrlRoot: "NuGet/public",
                oDatacontrollerName: "NuGetPublicOData");            //NuGetPublicODataController.cs, located in Controllers\ folder


            //Feed that allows unauthenticated read/download/delete/upload (configured in controller's constructor).
            //NuGetV2WebApiEnabler.UseNuGetV2WebApiFeed(config,
            //    routeName: "NuGetVeryPublic",
            //    routeUrlRoot: "NuGet/verypublic",
            //    oDatacontrollerName: "NuGetVeryPublicOData");        //NuGetVeryPublicODataController.cs, located in Controllers\ folder


        }
    }
}