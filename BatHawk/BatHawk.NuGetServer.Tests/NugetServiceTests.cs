using System;
using Xunit;

namespace BatHawk.NuGetServer.Tests
{
    public class NugetServiceTests
    {
        [Fact]
        public void StartAndStopTest()
        {
            Topshelf.HostControl hostControl = null; 
            NugetService service = new NugetService();
            service.Start(hostControl);
            service.Stop(hostControl);
        }
        
    }
}