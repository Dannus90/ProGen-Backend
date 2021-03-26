using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Configurations;
using Core.Context;
using Core.Mapping;
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
                opt.UseNpgsql(_connectionString, b => 
                    b.MigrationsAssembly("API")));
            
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

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SimpleMappers());
            });
            
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper(GetType().Assembly);
            services.AddControllers(opt => opt.Filters.Add(new ExceptionFilter()));
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1",
                new OpenApiInfo {Title = "API", Version = "v1"}); });
            
            // Dependency injection.
            services.Configure<ProGenConfig>(_proGenConfig);
            _dependencyInjection.AddDependencyInjectionServices(services);
            _dependencyInjection.AddDependencyInjectionRepositories(services, _connectionString);
        }

        // This method gets called by the runtime. We use this method to configure the pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(_allowedSpecificOrigins);
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}