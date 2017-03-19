using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;

namespace EwalletServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!Debugger.IsAttached && !args.Contains("--debug"))
            {
                // sets propert working directory
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            }

            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var hostingConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddJsonFile($"hosting.{envName}.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseConfiguration(hostingConfiguration)
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
