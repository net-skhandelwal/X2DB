using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace X2DB
{
    internal static class ServiceConfiguration
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<ServiceThread>(service =>
                {
                    service.ConstructUsing(s => new ServiceThread());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                    service.WhenShutdown(s => s.Stop());
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("X2DB");
                configure.SetDisplayName("X2DB");
                configure.SetDescription("X2DB");
            });
        }
    }
}
