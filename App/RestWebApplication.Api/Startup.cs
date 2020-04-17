using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestWebApplication.Data;
using RestWebApplication.Data.Common.Repositories;
using RestWebApplication.Data.Repositories;
using RestWebApplication.Services.Data;
using RestWebApplication.Services.Data.Contracts;
using RestWebApplication.Services.Mapping;
using RestWebApplication.Services.Models;

namespace RestWebApplication.Api
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

            services.AddDbContext<RestWebAppDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<ITeachersService, TeachersService>();
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapperConfig.RegisterMappings(typeof(TeacherDto).GetTypeInfo().Assembly);
            using (var scope = app.ApplicationServices.CreateScope() )
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<RestWebAppDbContext>();


                //context.Database.EnsureDeleted();
                
                context.Database.EnsureCreated();

            }
            
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers();});
        }
    }
}