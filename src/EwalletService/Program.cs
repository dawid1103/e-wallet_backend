using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace EwalletService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                // sets propert working directory
                //var rootPath = Environment.GetEnvironmentVariable("ASPNETCORE_ROOTPATH");
                //Directory.SetCurrentDirectory(rootPath);
            }

            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddJsonFile($"hosting.{envName}.json", optional: true)
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
