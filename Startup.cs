using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.OpenApi.Models;
using ParcialComputo3.Data;

namespace ParcialComputo3
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
            //Inicia Contexto de la BD
            services.AddDbContext<DatabaseContext>(options=>
            options.UseNpgsql(Configuration.GetConnectionString("DatabaseContext")));
            //Termina Contexto DB

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PARCIAL III", Version = "v1" });
                
            // Localizando el archivo XML generado por APP.NET
                var xmlFile =$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Le decimos a Swagger que muestre los comentarios del archivo XML
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments:true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                //Iniciando a Editando el Titulo de la Pagina.
                app.UseSwaggerUI(c =>{
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PARCIAL III v1");
                    c.DocumentTitle = "PARCIAL III";
                });
                // Finalizando.
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
