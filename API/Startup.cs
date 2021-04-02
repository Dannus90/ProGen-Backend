using System.Text;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Configurations;
using Core.Context;
using Core.Mapping;
using Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private const string _allowedSpecificOrigins = "_allowedSpecificOrigins";
        private readonly string _connectionString;
        private readonly DependencyInjection _dependencyInjection;
        private readonly IConfigurationSection _proGenConfig;
        private readonly IConfigurationSection _tokenConfig;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Properties/appsettings.json", false, true)
                .AddJsonFile($"Properties/appsettings.{env.EnvironmentName}.json", false,
                    true).AddEnvironmentVariables();

            _configuration = configurationBuilder.Build();
            _proGenConfig = _configuration.GetSection("ProGenConfig");
            _tokenConfig = _configuration.GetSection("TokenConfig");
            _connectionString = _proGenConfig.Get<ProGenConfig>().DbConnectionString;
            _dependencyInjection = new DependencyInjection();
        }

        private IConfiguration _configuration { get; }

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
                    _allowedSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000",
                                "https://localhost:3000",
                                "http://localhost:4000",
                                "https://localhost:4000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                );
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _tokenConfig.Get<TokenConfig>().Issuer,
                        ValidAudience = _tokenConfig.Get<TokenConfig>().Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig
                                .Get<TokenConfig>()
                                .SecretKey))
                    };
                });

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new SimpleMappers()); });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper(GetType().Assembly);
            services.AddControllers(opt => opt.Filters.Add(new ExceptionFilter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo {Title = "API", Version = "v1"});
            });

            // Dependency injection.
            services.Configure<ProGenConfig>(_proGenConfig);
            services.Configure<TokenConfig>(_tokenConfig);
            _dependencyInjection.AddDependencyInjectionHandlers(services);
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}