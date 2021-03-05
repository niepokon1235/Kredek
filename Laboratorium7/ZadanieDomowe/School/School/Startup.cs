using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using School.Controllers;
using School.Controllers.Interfaces;
using School.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School
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
            // Po��czenie z baz� danych
            services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("SchoolConnection")));

            services.AddControllers();

            // Dodanie automappera
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Wstrzykiwanie zale�no�ci Repozytori�w do interfejs�w
            services.AddScoped<IStudentRepository, SqlSchoolRepository>();
            services.AddScoped<ITeacherRepository, SqlSchoolRepository>();
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
