using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Cars_Dotnet.Models;
using Api_Cars_Dotnet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api_Cars_Dotnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CarStoreDatabaseSettings>(Configuration.GetSection(nameof(CarStoreDatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CarStoreDatabaseSettings>>().Value);

            services.AddSingleton<CarService>();

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var dbParams = sp.GetRequiredService<IOptions<CarStoreDatabaseSettings>>().Value;
                var client = new MongoClient(dbParams.ConnectionString);
                return client.GetDatabase(dbParams.DatabaseName);
            });

            services.AddSingleton<IRepository<Car>, MongoDBRepository<Car>>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
