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
        private IConfiguration Configuration { get; }
        private const string _allowedSpecificOrigins = "_allowedSpecificOrigins";
        private readonly string _connectionString;
        private readonly DependencyInjection _dependencyInjection;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Properties/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"Properties/appsettings.{env.EnvironmentName}.json", optional: false,
                    reloadOnChange: true).AddEnvironmentVariables();
            
            Configuration = configuration;
            _dependencyInjection = new DependencyInjection();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register database.
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString(_connectionString)));
            
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
            
            _dependencyInjection.AddDependencyInjectionServices(services);
            _dependencyInjection.AddDependencyInjectionRepositories(services);
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1",
                new OpenApiInfo {Title = "API", Version = "v1"}); });
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