using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace BatHawk.NuGetServer
{
    public partial class Startup
    {
        public static void ConfigureFileServer(IAppBuilder app)
        {
            string rootPath = System.Configuration.ConfigurationManager.AppSettings["WWW_PATH"];
            // Make ./www the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem(rootPath),
                EnableDirectoryBrowsing = false
            });
        }
    }
}