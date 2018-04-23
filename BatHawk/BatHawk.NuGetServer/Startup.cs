using NuGet.Server.V2;
using Owin;
using System;
using System.Web.Http;

namespace BatHawk.NuGetServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //logger.InfoFormat("I: Run appBilder");
            try
            {
                HttpConfiguration config = new HttpConfiguration();


                ConfigureWebApi(appBuilder, config);
                //logger.InfoFormat("ConfigureWebApi Done");

                ConfigureFileServer(appBuilder);
                //logger.InfoFormat("ConfigureFileServer Done");

                ConfigureContainer(appBuilder, config);
                //logger.InfoFormat("ConfigureContainer Done");

                // Configure Web API for self-host. 
                appBuilder.UseWebApi(config);

                //appBuilder.UseStageMarker(PipelineStage.MapHandler);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "AppStart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //logger.Error(ex.Message, ex);
                throw;
            }






        }
    }
}
