using System;
using Core.Configurations;
using Core.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        private const string _allowedSpecificOrigins = "_allowedSpecificOrigins";
        private readonly string _connectionString;
        private readonly IConfigurationSection _proGenConfig;
        private readonly DependencyInjection _dependencyInjection;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Properties/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Properties/appsettings.{env.EnvironmentName}.json", optional: false,
                    reloadOnChange: true).AddEnvironmentVariables();
            
            _configuration = configurationBuilder.Build();
            _proGenConfig = _configuration.GetSection("ProGenConfig");
            _connectionString = _proGenConfig.Get<ProGenConfig>().DbConnectionString;
            _dependencyInjection = new DependencyInjection();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register database.
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(_configuration.GetConnectionString(_connectionString)));
            
            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    name: _allowedSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000",
                            "https://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                );
            });

            Console.WriteLine("Testing commit connection again");
            
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1",
                new OpenApiInfo {Title = "API", Version = "v1"}); });
            
            // Dependency injection.
            services.Configure<ProGenConfig>(_proGenConfig);
            _dependencyInjection.AddDependencyInjectionServices(services);
            _dependencyInjection.AddDependencyInjectionRepositories(services);
        }

        // This method gets called by the runtime. We use this method to configure the pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(_allowedSpecificOrigins);
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}