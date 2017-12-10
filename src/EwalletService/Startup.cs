using EwalletService.BackgroundServices;
using EwalletService.DataAccessLayer;
using EwalletService.Logic;
using EwalletService.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            string sqlDatabaseName = Configuration.GetConnectionString("DatabaseName");

            // Add general
            services.AddSingleton(new DatabaseConfig(sqlConnectionString, sqlDatabaseName));
            services.AddScoped<IDatabaseSession, DatabaseSession>();

            // Add logic
            services.AddScoped<IPasswordLogic, PasswordLogic>();

            // Add db repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Add background tasks
            services.AddSingleton<IHostedService, ScheduledTransactionService>();

            // Add framework
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseSession db)
        {
            app.UseMvc();
            db.InitDatabase();
        }
    }
}
