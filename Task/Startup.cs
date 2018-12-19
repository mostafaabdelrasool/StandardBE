using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistance.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Task
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Core2.1
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #endregion
            #region CrossOriging
            services.AddCors();
            #endregion
            #region Swagger
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MO API", Version = "v1" });
            });
            #endregion
            #region Database 
            services.AddDbContext<TaskContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            #endregion            
            #region Dev
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    #region DB migration
                    // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                    var context = serviceScope.ServiceProvider.GetService<TaskContext>();
                    context.Database.Migrate();
                    #endregion
                }
                #region Swagger
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger(c => { });
                // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
                #endregion
            }
            #endregion
            #region Production
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseCookiePolicy();
                app.UseExceptionHandler("/Home/Error");
                try
                {
                    // Migrate DB here
                    // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                }
                catch { }
            }
            #endregion
            #region CrossOriging
            app.UseCors(builder =>
                       builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader().AllowAnyMethod());
            #endregion

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
