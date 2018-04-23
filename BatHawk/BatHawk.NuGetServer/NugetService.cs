using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.Timers;
using Topshelf;

namespace BatHawk.NuGetServer
{
    public class NugetService : ServiceControl
    {
        private readonly string baseAddress = ConfigurationManager.AppSettings["BASEADDRESS"];
        private readonly string watchTimer = ConfigurationManager.AppSettings["WATCHTIMER"];
        private IDisposable _app;


        readonly Timer _timer;


        public NugetService()
        {
            _timer = new Timer(Int32.Parse(watchTimer)) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);

        }


        public bool Start(HostControl hostControl)
        {
            _timer.Start();
            Console.WriteLine("NugetService Started.");


            _app = WebApp.Start<Startup>(baseAddress);
            return _app != null;
        }


        public bool Stop(HostControl hostControl)
        {
            _timer.Stop();
            return _app != null;
        }
    }
}
