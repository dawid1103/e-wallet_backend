using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace EwalletServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            if (Debugger.IsAttached || args.Contains("--debug"))
            {
                host.Run();
            }
            else
            {
                //to register the service run as admin:
                //sc create EwalletService binPath = "Full\Path\To\The\EwalletServices.exe"
                host.RunAsService();
            }
        }
    }
}
