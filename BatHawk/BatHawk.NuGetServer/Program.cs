// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 
using System;
using Topshelf;

namespace BatHawk.NuGetServer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var rc = HostFactory.Run(c =>
            {
                // Pass it to Topshelf
                //c.UseAutofacContainer(container);

                c.Service<NugetService>(s =>
                {
                    // Let Topshelf use it
                    //s.ConstructUsingAutofacContainer();
                    s.ConstructUsing(name => new NugetService());
                    s.WhenStarted((service, control) => service.Start(control));
                    s.WhenStopped((service, control) => service.Stop(control));
                });

                c.RunAsLocalSystem();                                      

                c.SetDescription("NugetService Topshelf Host");                
                c.SetDisplayName("Stuff");                                
                c.SetServiceName("Stuff");                                 
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;
        }
    }


}
