using DotNetCoreApiDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiDemo
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
            //----------------- Dependency Injection---------------------------//
            

            ///--------------------- TYPE 1 - constructor Way-----------------//
            //--------1. Singleton
            //services.Add(new ServiceDescriptor(typeof(IMyLogger), new MyLogger()));//Singleton
            //services.Add(new ServiceDescriptor(typeof(IMyLogger), typeof(MyLogger),ServiceLifetime.Singleton));// Thiss is also Singleton
            //services.AddSingleton(typeof(IMyLogger), typeof(MyLogger));//this is also singleton
            services.AddSingleton<IMyLogger, MyLogger>();//This is also SingleTon

            //--------2. Scoped
            //services.Add(new ServiceDescriptor(typeof(IMyLogger), typeof(MyLogger), ServiceLifetime.Scoped));
            //services.AddScoped(typeof(IMyLogger), typeof(MyLogger));
            //services.AddScoped<IMyLogger, MyLogger>();

            //--------3. Transient
            //services.Add(new ServiceDescriptor(typeof(IMyLogger), typeof(MyLogger), ServiceLifetime.Transient));
            //services.AddScoped(typeof(IMyLogger), typeof(MyLogger));
            //services.AddScoped<IMyLogger, MyLogger>();



            services.AddDbContext<EmpEntities>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseCors("AllowAnyOrigins");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
