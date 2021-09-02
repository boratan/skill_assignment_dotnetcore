using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Wolfpack.Data;
using Wolfpack.DomainServices;

namespace Wolfpack.API
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
            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("WolfDB");
                if (!string.IsNullOrEmpty(connectionString))
                    options.UseSqlServer(connectionString);
            }, ServiceLifetime.Singleton);
            services.AddScoped<IWolfRepository, WolfRepository>();
            services.AddScoped<IPackRepository, PackRepository>();
            services.AddControllers();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "WolfPack API", 
                    Version = "v1",
                    Description ="Simple .Net Core API example.",
                    Contact = new OpenApiContact
                    {
                        Name = "Borislav Atanasov",
                        Email = "b.atanasov@student.fontys.nl",
                        Url = new Uri("https://coderjony.com/"),
                    },
                });
            });;
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
            
            #region --------------------------------------------- Swagger -------------------------------------------------------

            app.UseSwagger( );

            app.UseSwaggerUI( options =>
            {
                options.SwaggerEndpoint( "/swagger/v1/swagger.json", "API V1" );
                options.RoutePrefix = "docs";
            } );

            #endregion
        }
    }
}
