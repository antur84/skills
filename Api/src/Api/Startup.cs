using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Cinode.Skills.Api.Mappers;
using Cinode.Skills.Api.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Cinode.Api.Models;
using Cinode.Skills.Api;
using Microsoft.Extensions.Primitives;

namespace Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterAllServicesByConvention(services);
            RegisterMappers(services);
            RegisterRepositories(services);
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureErrorHandling(app);

            ConfigureCors(app);

            app.UseMvc();
        }

        private void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(GetAllowedOrigins())
                    .Build();
            });
        }

        private string[] GetAllowedOrigins()
        {
            return AllowedOrigins.Whitelist.ToArray();
        }

        private void ConfigureErrorHandling(IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder  =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    string origin;
                    if (IsOriginWhitelisted(context, out origin))
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", origin);
                    }

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            ApiResponseViewModel<object>
                        {
                            Code = 500,
                            Message = ex.Message
                        }));
                    }
                });
            });
        }

        private bool IsOriginWhitelisted(HttpContext context, out string origin)
        {
            StringValues temp;
            origin = string.Empty;
            if (context.Request.Headers.TryGetValue("Origin", out temp)) {
                var o = temp.First();
                origin = o;
                return GetAllowedOrigins().Any(x => x == o);
            }
            return false;
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            var type = typeof(IRepository<>);
            var repositories = GetAllTypesWhichImplementsInterface(type);

            foreach (var repo in repositories)
            {
                var serviceType = repo.GetTypeInfo().GetInterface(type.GetTypeInfo().Name);
                services.AddSingleton(serviceType, repo);
            }
        }

        private void RegisterAllServicesByConvention(IServiceCollection services)
        {
            var typesWithMatchingInterfaceNames = GetType().GetTypeInfo().Assembly.ExportedTypes.Where(x =>
                x.GetTypeInfo().GetInterfaces().FirstOrDefault(i => i.Name == "I" + x.Name) != null);

            foreach (var implementationType in typesWithMatchingInterfaceNames)
            {
                var serviceType = implementationType.GetTypeInfo().GetInterface("I" + implementationType.Name);
                services.AddScoped(serviceType, implementationType);
            }
        }

        private void RegisterMappers(IServiceCollection services)
        {
            var type = typeof(IMapper<,>);
            var mappers = GetAllTypesWhichImplementsInterface(type);

            foreach (var mapper in mappers)
            {
                var serviceType = mapper.GetTypeInfo().GetInterface(type.GetTypeInfo().Name);
                services.AddScoped(serviceType, mapper);
            }
        }

        private IEnumerable<Type> GetAllTypesWhichImplementsInterface(Type type)
        {
            return GetType().GetTypeInfo().Assembly.ExportedTypes.Where(x =>
                x.GetTypeInfo().GetInterfaces().Any(i => i.Name == type.GetTypeInfo().Name));
        }
    }
}
