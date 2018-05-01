using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using blocknetcore.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace blocknetcore
{
    public class Program
    {
        private static readonly Lazy<Logic> logic = new Lazy<Logic>(() => new Logic());
        public static Logic BlockLogic { get { return logic.Value; } }

        public static void Main(string[] args)
        {

            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
