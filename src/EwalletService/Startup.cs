using EwalletService.BackgroundServices;
using EwalletService.DataAccessLayer;
using EwalletService.Logic;
using EwalletService.Repository;
using EwalletService.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System;

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
            IConfigurationSection scheduledTransactionServiceSettings = Configuration.GetBackgroundServiceSettings("ScheduledTransaction");

            // Add general
            services.AddSingleton(new DatabaseConfig(sqlConnectionString, sqlDatabaseName));
            services.AddScoped<IDatabaseSession, DatabaseSession>();

            // Add logic
            services.AddScoped<IPasswordLogic, PasswordLogic>();

            // Add db repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IScheduledTransactionRepository, ScheduledTransactionRepository>();

            // Add background tasks
            // Dirty hack to make scoped services injected into singleton ;/
            IServiceProvider provider = services.BuildServiceProvider();
            services.AddSingleton<IHostedService>(new ScheduledTransactionService(provider, scheduledTransactionServiceSettings));

            // Add framework
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseSession db)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
            db.InitDatabase();
        }
    }
}
