using Fcc.Aeat.Core.Data.Connection;
using Fcc.Aeat.Factura.Contracts.Contracts;
using Fcc.Aeat.Factura.Contracts.Repositories;
using Fcc.Aeat.Factura.Handlers;
using Fcc.Aeat.Factura.Managers;
using Fcc.Aeat.Factura.Queries.Contracts;
using Fcc.Aeat.Factura.Queries.Impl;
using Fcc.Aeat.Factura.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fcc.Aeat.Api
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
            ConfigureManagers(services);
            ConfigureHandlers(services);
            ConfigureServiceQueries(services);
            ConfigureDapperConnectionStrings(services);
            ConfigureServiceRepositories(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fcc.Aeat.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fcc.Aeat.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /*
         Transient: Se crean cada vez que se solicitan desde el contenedor de servicios. 
        Esta vida útil funciona mejor para servicios ligeros y sin estado.

         Scoped: Se crean una vez por solicitud del cliente(conexión). 
        Se utiliza cuando queremos servir la misma instancia dentro del mismo 
        contexto de una petición HTTP, pero diferente entre distintos contextos HTTP.

         Singleton: Se crean la primera vez que se solicitan o cuando Startup.ConfigureServices se 
        ejecuta y se especifica una instancia con el registro del servicio. Cada solicitud
        posterior utiliza la misma instancia.
         */
        private void ConfigureServiceQueries(IServiceCollection services)
        {
            services.AddScoped<IFacturaQueries, FacturaQueries>();
        }

        private void ConfigureDapperConnectionStrings(IServiceCollection services)
        {
            string FccConnectionstring = Configuration.GetConnectionString("FccConnectionString");
            var connectionString = new ConnectionString(FccConnectionstring);
            services.AddSingleton(connectionString);
        }

        private void ConfigureHandlers(IServiceCollection services)
        {
            services.AddMediatR(
                typeof(FacturaAddCommandHandler).Assembly);
        }

        private void ConfigureManagers(IServiceCollection services)
        {
            services.AddScoped<IAddFacturaManager, AddFacturaManager>();
        }

        private void ConfigureServiceRepositories(IServiceCollection services)
        {
            services.AddScoped<IFacturaRepository, FacturaRepository>();
        }

    }
}
