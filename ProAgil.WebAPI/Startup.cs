using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProAgil.Repository;


namespace ProAgil.WebAPI
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
            services.AddControllers();
            services.AddDbContext<ProAgilContext>(x =>
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                );
            services.AddScoped<IProAgilRepository, ProAgilRepository>();
            services.AddMvc();
            services.AddCors();
            services.AddSwaggerGen(c =>
                                       {
                                           c.SwaggerDoc("v1", new OpenApiInfo
                                           {
                                               Title = "Curso Udemy - Dev FullStack with Angular",
                                               Version = "v1"
                                           });

                                           var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                                           var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                                           c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                                       });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseCors(access => access.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "v1");
            });

        }
    }
}
