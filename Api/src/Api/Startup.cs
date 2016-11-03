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

            app.UseMvc();
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

            foreach(var implementationType in typesWithMatchingInterfaceNames)
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
