using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentWebApp.ConfigModel;
using TalentWebApp.DataAccess;
using TalentWebApp.Interfaces;
using TalentWebApp.Services;

namespace TalentWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        // Build DI Container : 
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Delay :" + Configuration["talent:delay:productsWaitTime"]);
            services.Configure<DelayConfigModel>(Configuration.GetSection("talent:delay"));


            services.AddDbContext<MyAppDataContext>(
                    options => options.UseSqlServer("name=ConnectionStrings:TalentConnection"));

            services.AddScoped<IAppLogger, ShimonLogger>();

            //services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<IProductService, MockProductsService>();



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TalentWebApp", Version = "v1" });
            });
        }

        // Build middlewares (Pipeline) : 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TalentWebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //app.UseStaticFiles();


            //app.Use(async (context, next) => { 
            //    await context.Response.WriteAsync("Use1 (A) "); // 1
            //    await next();
            //    await context.Response.WriteAsync("Use1 (B) "); // 5
            //});

            //app.Use(async (context, next) => {
            //    await context.Response.WriteAsync("Use2 (A) "); // 2
            //    await next();
            //    await context.Response.WriteAsync("Use2 (B) "); // 4
            //});

            //app.Run((context) => {
            //    return context.Response.WriteAsync("My APP "); // 3
            //});





        }
    }
}


