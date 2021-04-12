using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using Newtonsoft.Json.Serialization;

namespace CommandAPI
{
    public class Startup
    {
        public IConfiguration Configuration {get;}
        public Startup(IConfiguration configuration)
          {
            Configuration = configuration;
          }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString =
                   Configuration.GetConnectionString("PostgreSqlConnection");
                   builder.Username = Configuration["UserID"];
                   builder.Password = Configuration["Password"];
            //Add service
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<CommandContext>(opt => opt.UseNpgsql
                (builder.ConnectionString));
            services.AddControllers();
            services.AddScoped<ICommandAPIRepo,SqlCommandAPIRepo>();
            services.AddControllers().AddNewtonsoftJson(s =>
                   {
                       s.SerializerSettings.ContractResolver = new
                       CamelCasePropertyNamesContractResolver();
                     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //map endpoints to COntrollers
                    endpoints.MapControllers();
                    
                // endpoints.MapGet("/", async context =>
                // {
                    
                //     await context.Response.WriteAsync("Hello World!");
                // });
            });
        }
    }
}
