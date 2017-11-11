using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using EwalletService.DataAccessLayer;
using EwalletService.Repository;
using EwalletService.Logic;
using System.IO;

namespace EwalletService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine("Logs", "log-{Date}.txt")).CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            // Add general
            services.AddSingleton(new DatabaseConfig(sqlConnectionString));
            services.AddScoped<IDatabaseSession, DatabaseSession>();

            // Add logic
            services.AddScoped<IPasswordLogic, PasswordLogic>();

            // Add db repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Add framework
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Log to console only on dev
            if (env.EnvironmentName != EnvironmentName.Production || env.EnvironmentName != EnvironmentName.Staging)
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            }

            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            app.UseMvc();
        }
    }
}
